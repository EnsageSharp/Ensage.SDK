// <copyright file="MenuManager.cs" company="Ensage">
//    Copyright (c) 2018 Ensage.
// </copyright>

namespace Ensage.SDK.Menu
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.Composition;
    using System.Linq;
    using System.Reflection;
    using System.Windows.Input;

    using Ensage.Common.Extensions;
    using Ensage.SDK.Helpers;
    using Ensage.SDK.Input;
    using Ensage.SDK.Menu.Attributes;
    using Ensage.SDK.Menu.Config;
    using Ensage.SDK.Menu.Entries;
    using Ensage.SDK.Menu.Items;
    using Ensage.SDK.Menu.Styles;
    using Ensage.SDK.Menu.Styles.Elements;
    using Ensage.SDK.Service;

    using EnsageSharp.Sandbox;

    using Newtonsoft.Json.Converters;
    using Newtonsoft.Json.Linq;

    using NLog;

    using PlaySharp.Toolkit.Helper.Annotations;

    using SharpDX;

    [Export]
    [PublicAPI]
    public class MenuManager : ControllableService
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        private readonly IServiceContext context;

        private readonly List<MenuEntry> rootMenus = new List<MenuEntry>();

        private readonly List<PermaMenuItemEntry> permaItemEntries = new List<PermaMenuItemEntry>();

        private readonly StyleRepository styleRepository;

        private readonly ViewRepository viewRepository;

        private bool blockedLeftClick;

        private MenuEntry dragMenuEntry;

        private Vector2 dragMouseDiff;

        private Vector2 dragStartPosition;

        private MenuBase lastHoverEntry;

        private MenuSerializer menuSerializer;

        private Vector2 position;

        private bool positionDirty;

        private bool sizeDirty;

        private bool titleBarDragged;

        private bool titleBarHovered;

        private Vector2 permaPosition;

        [ImportingConstructor]
        public MenuManager([Import] IServiceContext context, [Import] ViewRepository viewRepository, [Import] StyleRepository styleRepository)
        {
            // TODO extract interface
            this.context = context;
            this.viewRepository = viewRepository;
            this.styleRepository = styleRepository;
        }

        public bool IsVisible { get; private set; } = false;

        public MenuBase LastHoverEntry
        {
            get
            {
                return this.lastHoverEntry;
            }

            set
            {
                if (this.lastHoverEntry != null)
                {
                    this.lastHoverEntry.IsHovered = false;
                }

                this.lastHoverEntry = value;
                if (this.lastHoverEntry != null)
                {
                    this.lastHoverEntry.IsHovered = true;
                }
            }
        }

        public MenuConfig MenuConfig { get; private set; }

        public Vector2 MenuPosition
        {
            get
            {
                return this.position + new Vector2(0, this.TitleBarSize.Y);
            }
        }

        /// <summary>
        ///     Gets or sets the upper left corner position of the menu.
        /// </summary>
        public Vector2 Position
        {
            get
            {
                return this.position;
            }

            set
            {
                this.position = value;
                this.positionDirty = true;
                this.MenuConfig.MenuPosition = value;
            }
        }

        /// <summary>
        ///     Gets or sets the upper left corner position of the perma menu.
        /// </summary>
        public Vector2 PermaPosition
        {
            get
            {
                return this.permaPosition;
            }

            set
            {
                this.permaPosition = value;
                this.MenuConfig.PermaPosition = value;
            }
        }

        /// <summary>
        ///     Gets the total size of the menu as a rectangle.
        /// </summary>
        public Vector2 Size { get; private set; }

        public Vector2 TitleBarSize { get; private set; }

        public bool DeregisterMenu(object menu, bool saveMenu = true)
        {
            var dataType = menu.GetType();
            var sdkAttribute = dataType.GetCustomAttribute<MenuAttribute>();
            if (sdkAttribute == null)
            {
                throw new Exception($"Missing attribute {nameof(MenuAttribute)}");
            }

            if (saveMenu)
            {
                this.SaveMenu(menu);
            }

            return this.rootMenus.RemoveAll(x => x.DataContext == menu) != 0;
        }

        [CanBeNull]
        public MenuEntry FindDynamicMenuEntry(DynamicMenu menu)
        {
            foreach (var menuEntry in this.rootMenus)
            {
                if (menuEntry.DataContext is DynamicMenu && menuEntry.DataContext == menu)
                {
                    return menuEntry;
                }

                foreach (var entry in menuEntry.Children.OfType<MenuEntry>())
                {
                    if (!(entry.DataContext is DynamicMenu dynamic))
                    {
                        continue;
                    }

                    var found = this.FindDynamicMenuEntry(dynamic);
                    if (found != null)
                    {
                        return found;
                    }
                }
            }

            return null;
        }

        [CanBeNull]
        public MenuEntry FindParentMenu(MenuEntry menu, MenuBase findEntry)
        {
            foreach (var entry in menu.Children)
            {
                if (entry == findEntry)
                {
                    return menu;
                }

                if (entry is MenuEntry childMenu)
                {
                    var found = this.FindParentMenu(childMenu, findEntry);
                    if (found != null)
                    {
                        return childMenu;
                    }
                }
            }

            return null;
        }

        public bool IsInsideMenu(Vector2 screenPosition)
        {
            return this.Position.X <= screenPosition.X
                   && this.Position.Y <= screenPosition.Y
                   && screenPosition.X <= (this.Position.X + this.Size.X)
                   && screenPosition.Y <= (this.Position.Y + this.Size.Y);
        }

        public bool LoadMenu(object menu)
        {
            try
            {
                var token = this.menuSerializer.Deserialize(menu);
                var rootMenu = this.rootMenus.First(x => x.DataContext == menu);

                if (rootMenu.DataContext is DynamicMenu dynamic)
                {
                    dynamic.LoadToken = token;
                }

                this.LoadLayer(rootMenu, token);
                return true;
            }
            catch (Exception e)
            {
                Log.Error(e);
                return false;
            }
        }

        public void OnDraw(object sender, EventArgs e)
        {
            var drawList = this.rootMenus.ToArray();

            if (this.positionDirty)
            {
                // recalculate positions
                var pos = this.MenuPosition;
                foreach (var menuEntry in drawList)
                {
                    pos = this.UpdateMenuEntryPosition(menuEntry, pos);
                }

                this.sizeDirty = true;
                this.positionDirty = false;
            }

            if (this.sizeDirty)
            {
                // recalculate size
                this.CalculateMenuRenderSize(this.rootMenus);
                this.Size = this.CalculateMenuTotalSize(this.rootMenus);

                this.CalculateMenuPermaRenderSize(this.permaItemEntries);

                // recalculate positions by rendersize
                var pos = this.MenuPosition;
                foreach (var menuEntry in drawList)
                {
                    pos = this.UpdateMenuEntryRenderPosition(menuEntry, pos);
                }

                this.sizeDirty = false;
            }

            var activeStyle = this.MenuConfig.GeneralConfig.ActiveStyle.Value;
            if (!this.IsVisible)
            {
                // Permashow
                var permaShow = activeStyle.StyleConfig.TitleBar;
                this.context.Renderer.DrawTexture(activeStyle.TitleBar, new RectangleF(this.PermaPosition.X, this.PermaPosition.Y, this.TitleBarSize.X, this.TitleBarSize.Y));
                this.context.Renderer.DrawText(
                    this.PermaPosition + new Vector2(permaShow.Border.Thickness[0], permaShow.Border.Thickness[1]),
                    "Menu",
                    permaShow.Font.Color,
                    permaShow.Font.Size,
                    permaShow.Font.Family);

                var p = this.PermaPosition;
                p.Y += this.TitleBarSize.Y;
                foreach (var permaItemEntry in this.permaItemEntries.ToArray())
                {
                    p = permaItemEntry.PermaDraw(p);
                }

                return;
            }

            var titleBar = activeStyle.StyleConfig.TitleBar;
            this.context.Renderer.DrawTexture(activeStyle.TitleBar, new RectangleF(this.Position.X, this.Position.Y, this.TitleBarSize.X, this.TitleBarSize.Y));
            this.context.Renderer.DrawText(
                this.Position + new Vector2(titleBar.Border.Thickness[0], titleBar.Border.Thickness[1]),
                "Menu",
                titleBar.Font.Color,
                titleBar.Font.Size,
                titleBar.Font.Family);

            foreach (var menuEntry in drawList)
            {
                this.DrawMenuEntry(menuEntry);
            }
        }

        public MenuEntry RegisterMenu(object menu)
        {
            Log.Debug($"Registering {menu}");
            var dataType = menu.GetType();
            var sdkAttribute = dataType.GetCustomAttribute<MenuAttribute>();
            if (sdkAttribute == null)
            {
                throw new Exception($"Missing attribute {nameof(MenuAttribute)}");
            }

            if (this.rootMenus.Any(x => x.DataContext == menu))
            {
                throw new ArgumentException($"{menu} is already registered");
            }

            this.context.Container.BuildUp(menu);

            var menuName = sdkAttribute.Name;
            if (string.IsNullOrEmpty(menuName))
            {
                menuName = dataType.Name;
            }

            var view = this.viewRepository.GetMenuView();
            var textureAttribute = dataType.GetCustomAttribute<TexureAttribute>();
            var menuEntry = new MenuEntry(menuName, textureAttribute?.TextureKey, view, this.context.Renderer, this.MenuConfig, menu);
            this.VisitInstance(menuEntry, menu, menuEntry);

            this.rootMenus.Add(menuEntry);

            this.LoadMenu(menu);

            this.positionDirty = true;
            this.sizeDirty = true;

            return menuEntry;
        }

        public bool SaveMenu(object menu)
        {
            try
            {
                this.menuSerializer.Serialize(menu);
                return true;
            }
            catch (Exception e)
            {
                Log.Error(e);
                return false;
            }
        }

        protected override void OnActivate()
        {
            Messenger<AddDynamicMenuMessage>.Subscribe(this.AddDynamicMenu);
            Messenger<DynamicMenuRemoveMessage>.Subscribe(this.DynamicMenuRemovedMessage);

            this.menuSerializer = new MenuSerializer(new StringEnumConverter(), new MenuStyleConverter(this.styleRepository));

            this.MenuConfig = new MenuConfig();
            this.MenuConfig.GeneralConfig.ActiveStyle = new Selection<IMenuStyle>(this.styleRepository.Styles.ToArray());
            this.MenuConfig.GeneralConfig.ActiveStyle.Value = this.styleRepository.DefaultMenuStyle;

            this.Position = this.MenuConfig.MenuPosition;
            this.PermaPosition = this.MenuConfig.PermaPosition;

            var titleBar = this.MenuConfig.GeneralConfig.ActiveStyle.Value.StyleConfig.TitleBar;
            this.TitleBarSize = this.context.Renderer.MessureText("Menu", titleBar.Font.Size, titleBar.Font.Family)
                                + new Vector2(titleBar.Border.Thickness[0] + titleBar.Border.Thickness[2], titleBar.Border.Thickness[1] + titleBar.Border.Thickness[3]);
            this.context.Renderer.TextureManager.LoadFromResource("menuStyle/logo", @"MenuStyle.logo.png");

            this.RegisterMenu(this.MenuConfig);

            this.context.Renderer.Draw += this.OnDraw;
            this.context.Input.MouseMove += this.OnMouseMove;
            this.context.Input.MouseClick += this.OnMouseClick;

            // Register hotkeys if set
            if (SandboxConfig.Config.HotKeys.TryGetValue("MenuToggle", out var toggleKey))
            {
                this.context.Input.RegisterHotkey("Ensage.SDK.ToggleKey", (uint)toggleKey, this.ToggleKey);
            }

            this.context.Input.KeyDown += this.MenuKeyDown;
            this.context.Input.KeyUp += this.MenuKeyUp;
        }

        protected override void OnDeactivate()
        {
            this.context.Input.UnregisterHotkey("Ensage.SDK.ToggleKey");
            this.context.Input.KeyDown -= this.MenuKeyDown;
            this.context.Input.KeyUp -= this.MenuKeyUp;

            Messenger<AddDynamicMenuMessage>.Unsubscribe(this.AddDynamicMenu);
            Messenger<DynamicMenuRemoveMessage>.Unsubscribe(this.DynamicMenuRemovedMessage);

            this.MenuConfig.MenuPosition = this.Position;

            this.context.Renderer.Draw -= this.OnDraw;
            this.context.Input.MouseMove -= this.OnMouseMove;
            this.context.Input.MouseClick -= this.OnMouseClick;

            foreach (var menuEntry in this.rootMenus)
            {
                this.SaveMenu(menuEntry.DataContext);
            }

            this.MenuConfig = null;
        }

        private void AddDynamicMenu(AddDynamicMenuMessage args)
        {
            var parent = this.FindDynamicMenuEntry(args.DynamicMenu);
            if (parent == null)
            {
                throw new Exception($"{args} is not a registered dynamic menu.");
            }

            if (args.IsItem)
            {
                var valueDictionaryBinding = new ValueDictionaryBinding(args.Key, args.DynamicMenu.ValueStorage);
                var type = valueDictionaryBinding.ValueType;
                var view = this.viewRepository.GetView(type);
                var menuItemEntry = new MenuItemEntry(args.Name, args.TextureKey, view, this.context.Renderer, this.MenuConfig, valueDictionaryBinding);

                parent.AddChild(menuItemEntry);
                this.positionDirty = this.sizeDirty = true;
            }
            else
            {
                var menuEntry = new MenuEntry(args.Name, args.TextureKey, this.viewRepository.MenuView.Value, this.context.Renderer, this.MenuConfig, args.Instance);

                parent.AddChild(menuEntry);
                this.positionDirty = this.sizeDirty = true;
            }
        }

        private void CalculateMenuRenderSize(IEnumerable<MenuBase> entries)
        {
            var entryList = entries.ToList();

            var renderSize = Vector2.Zero;
            foreach (var child in entryList)
            {
                var menuEntry = child as MenuEntry;
                if (menuEntry != null)
                {
                    this.CalculateMenuRenderSize(menuEntry.Children);
                }

                var menuEntrySize = child.Size;
                renderSize.X = Math.Max(renderSize.X, menuEntrySize.X);
                renderSize.Y = Math.Max(renderSize.Y, menuEntrySize.Y);
            }

            foreach (var child in entryList)
            {
                child.RenderSize = renderSize;
            }
        }

        private void CalculateMenuPermaRenderSize(IEnumerable<PermaMenuItemEntry> entries)
        {
            var entryList = entries.ToList();

            var renderSize = Vector2.Zero;
            foreach (var child in entryList)
            {
                var menuEntrySize = child.PermaSize;
                renderSize.X = Math.Max(renderSize.X, menuEntrySize.X);
                renderSize.Y = Math.Max(renderSize.Y, menuEntrySize.Y);
            }

            foreach (var child in entryList)
            {
                child.PermaRenderSize = renderSize;
            }
        }

        private Vector2 CalculateMenuTotalSize(IEnumerable<MenuBase> entries)
        {
            Vector2 totalSize = this.TitleBarSize;

            var entryList = entries.ToList();
            var first = entryList.FirstOrDefault();
            if (first == null)
            {
                return totalSize;
            }

            var firstSize = first.RenderSize;
            totalSize += new Vector2(firstSize.X, entryList.Count * firstSize.Y);
            for (var i = 0; i < entryList.Count; i++)
            {
                var menuEntry = entryList[i] as MenuEntry;
                if (menuEntry != null)
                {
                    var childSize = this.CalculateMenuTotalSize(menuEntry.Children);
                    menuEntry.TotalSize = childSize + new Vector2(firstSize.X, firstSize.Y * i);

                    totalSize.X = Math.Max(totalSize.X, menuEntry.TotalSize.X);
                    totalSize.Y = Math.Max(totalSize.Y, menuEntry.TotalSize.Y);
                }
            }

            return totalSize;
        }

        private bool CollapseLayer(MenuEntry menuEntry, List<MenuEntry> menuEntries)
        {
            // in current layer
            if (menuEntries.Any(x => x == menuEntry))
            {
                menuEntries.Where(x => x != menuEntry).ForEach(x => x.IsCollapsed = true);
                return true;
            }

            // search next layers
            foreach (var entry in menuEntries)
            {
                if (this.CollapseLayer(menuEntry, entry.Children.OfType<MenuEntry>().ToList()))
                {
                    return true;
                }
            }

            return false;
        }

        private void DrawMenuEntry(MenuEntry entry)
        {
            if (!entry.IsVisible)
            {
                return;
            }

            entry.Draw();
            if (entry.IsCollapsed)
            {
                return;
            }

            var children = entry.Children.ToArray();
            foreach (var menu in children.OfType<MenuEntry>())
            {
                this.DrawMenuEntry(menu);
            }

            foreach (var item in children.OfType<MenuItemEntry>())
            {
                item.Draw();
            }
        }

        private void DynamicMenuRemovedMessage(DynamicMenuRemoveMessage obj)
        {
            // TODO:
        }

        private void LoadLayer(MenuEntry menu, [CanBeNull] JToken token)
        {
            foreach (var child in menu.Children.OfType<MenuItemEntry>())
            {
                // set default value by attribute
                var defaultValue = child.ValueBinding.GetCustomAttribute<DefaultValueAttribute>();
                if (defaultValue != null)
                {
                    child.Value = defaultValue.Value;
                    child.AssignDefaultValue();
                }

                var entry = token?[child.ValueBinding.Name];
                if (entry != null)
                {
                    if (child.Value is ILoadable loadable)
                    {
                        var loaded = this.menuSerializer.ToObject(entry, child.ValueBinding.ValueType);
                        loadable.Load(loaded);
                    }
                    else
                    {
                        child.Value = this.menuSerializer.ToObject(entry, child.ValueBinding.ValueType);
                    }
                }
            }

            foreach (var child in menu.Children.OfType<MenuEntry>())
            {
                var subToken = token[child.DataContext.GetType().Name];
                if (subToken != null)
                {
                    this.LoadLayer(child, subToken);
                }
            }
        }

        private void MenuKeyDown(object sender, KeyEventArgs e)
        {
            if (SandboxConfig.Config.HotKeys.TryGetValue("Menu", out var menuKey))
            {
                var key = KeyInterop.KeyFromVirtualKey(menuKey);
                if (e.Key == key)
                {
                    this.IsVisible = true;
                }
            }
        }

        private void MenuKeyUp(object sender, KeyEventArgs e)
        {
            if (SandboxConfig.Config.HotKeys.TryGetValue("Menu", out var menuKey))
            {
                var key = KeyInterop.KeyFromVirtualKey(menuKey);
                if (e.Key == key)
                {
                    this.IsVisible = false;
                }
            }
        }

        [CanBeNull]
        private MenuBase OnClickCheck(MenuEntry entry, MouseButtons buttons, Vector2 mousePosition)
        {
            if (!entry.IsVisible)
            {
                return null;
            }

            if (!entry.IsInsideMenu(mousePosition))
            {
                return null;
            }

            if (entry.IsInside(mousePosition))
            {
                entry.OnClick(buttons, mousePosition);
                return entry;
            }

            MenuEntry hoveredEntry = null;
            foreach (var menu in entry.Children.OfType<MenuEntry>())
            {
                if (this.OnClickCheck(menu, buttons, mousePosition) != null)
                {
                    hoveredEntry = menu;
                    break;
                }
            }

            if (hoveredEntry != null)
            {
                if (!hoveredEntry.IsCollapsed)
                {
                    entry.Children.OfType<MenuEntry>().Where(x => x != hoveredEntry).ForEach(x => x.IsCollapsed = true);
                }

                return hoveredEntry;
            }

            if (!entry.IsCollapsed)
            {
                foreach (var item in entry.Children.OfType<MenuItemEntry>())
                {
                    if (item.IsInside(mousePosition))
                    {
                        item.OnClick(buttons, mousePosition);
                        return item;
                    }
                }
            }

            return null;
        }

        [CanBeNull]
        private MenuBase OnInsideCheck(MenuEntry entry, Vector2 mousePosition)
        {
            if (!entry.IsVisible)
            {
                return null;
            }

            if (!entry.IsInsideMenu(mousePosition))
            {
                return null;
            }

            if (entry.IsInside(mousePosition))
            {
                return entry;
            }

            foreach (var menu in entry.Children.OfType<MenuEntry>())
            {
                var hoveredEntry = this.OnInsideCheck(menu, mousePosition);
                if (hoveredEntry != null)
                {
                    return hoveredEntry;
                }
            }

            if (!entry.IsCollapsed)
            {
                foreach (var item in entry.Children.OfType<MenuItemEntry>())
                {
                    if (item.IsInside(mousePosition))
                    {
                        return item;
                    }
                }
            }

            return null;
        }

        private void OnMouseClick(object sender, MouseEventArgs e)
        {
            if ((e.Buttons & MouseButtons.Left) == 0)
            {
                return;
            }

            if ((e.Buttons & MouseButtons.LeftUp) == MouseButtons.LeftUp)
            {
                if (this.titleBarDragged)
                {
                    Log.Debug($"Stop title bar dragging");
                    this.titleBarDragged = false;
                    return;
                }

                // handle drag and drop
                if (this.dragMenuEntry != null)
                {
                    // TODO:
                    this.dragMenuEntry = null;
                }

                if (this.blockedLeftClick)
                {
                    e.Process = false;
                    this.blockedLeftClick = false;
                }

                // return;
            }

            if (this.titleBarHovered && (e.Buttons & MouseButtons.LeftDown) == MouseButtons.LeftDown)
            {
                Log.Debug($"start title bar dragging");
                if (this.IsVisible)
                {
                    this.dragMouseDiff = e.Position - this.Position;
                }
                else
                {
                    this.dragMouseDiff = e.Position - this.PermaPosition;
                }

                this.titleBarDragged = true;
                return;
            }

            if (!this.IsVisible || !this.IsInsideMenu(e.Position))
            {
                return;
            }

            // check for click
            if (this.LastHoverEntry != null)
            {
                if (this.context.Input.IsKeyDown(Key.Escape))
                {
                    this.LastHoverEntry.Reset();
                }
                else
                {
                    this.LastHoverEntry.OnClick(e.Buttons, e.Position);
                    if (this.LastHoverEntry is MenuEntry menuEntry)
                    {
                        if (!menuEntry.IsCollapsed)
                        {
                            this.CollapseLayer(menuEntry, this.rootMenus);
                        }
                    }
                }

                e.Process = false;
                this.blockedLeftClick = true;
            }
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (this.titleBarDragged)
            {
                if (this.IsVisible)
                {
                    this.Position = e.Position - this.dragMouseDiff;
                }
                else
                {
                    this.PermaPosition = e.Position - this.dragMouseDiff;
                }

                return;
            }

            // check for titlebar mouseover
            var titleBar = this.IsVisible ? new RectangleF(this.Position.X, this.Position.Y, this.TitleBarSize.X, this.TitleBarSize.Y) : new RectangleF(this.PermaPosition.X, this.PermaPosition.Y, this.TitleBarSize.X, this.TitleBarSize.Y);
            this.titleBarHovered = titleBar.Contains(e.Position);

            if (!this.IsVisible || !this.IsInsideMenu(e.Position))
            {
                if (this.LastHoverEntry != null)
                {
                    Log.Info($"MouseLeave {this.LastHoverEntry}");
                    this.LastHoverEntry = null;
                }

                return;
            }

            if (this.dragMenuEntry != null)
            {
                return;
            }

            if (!this.titleBarHovered)
            {
                // check for mouse hover
                foreach (var menuEntry in this.rootMenus)
                {
                    var hoverItem = this.OnInsideCheck(menuEntry, e.Position);
                    if (hoverItem != null)
                    {
                        if (this.LastHoverEntry != hoverItem)
                        {
                            if (this.LastHoverEntry != null)
                            {
                                Log.Info($"MouseLeave {this.LastHoverEntry}");
                            }

                            Log.Info($"MouseHover {hoverItem}");
                            this.LastHoverEntry = hoverItem;

                            // check for drag and drop of menu entries (possible to swap positions)
                            if ((e.Buttons & MouseButtons.Left) != 0 && this.LastHoverEntry is MenuEntry hoverEntry && this.LastHoverEntry.DataContext != this.MenuConfig)
                            {
                                this.dragStartPosition = e.Position;
                                this.dragMenuEntry = hoverEntry;
                            }
                        }

                        return;
                    }
                }
            }

            if (this.LastHoverEntry != null)
            {
                Log.Info($"MouseLeave {this.LastHoverEntry}");
                this.LastHoverEntry = null;
            }
        }

        private void ToggleKey(KeyEventArgs obj)
        {
            this.IsVisible = !this.IsVisible;
        }

        private Vector2 UpdateMenuEntryPosition(MenuEntry entry, Vector2 pos)
        {
            entry.Position = pos;

            var currentPos = pos + new Vector2(entry.Size.X, 0);
            foreach (var menu in entry.Children.OfType<MenuEntry>())
            {
                currentPos = this.UpdateMenuEntryPosition(menu, currentPos);
            }

            foreach (var item in entry.Children.OfType<MenuItemEntry>())
            {
                item.Position = currentPos;
                currentPos += new Vector2(0, item.Size.Y);
            }

            return entry.Position + new Vector2(0, entry.Size.Y);
        }

        private Vector2 UpdateMenuEntryRenderPosition(MenuEntry entry, Vector2 pos)
        {
            entry.Position = pos;

            var currentPos = pos + new Vector2(entry.RenderSize.X, 0);
            foreach (var menu in entry.Children.OfType<MenuEntry>())
            {
                currentPos = this.UpdateMenuEntryRenderPosition(menu, currentPos);
            }

            foreach (var item in entry.Children.OfType<MenuItemEntry>())
            {
                item.Position = currentPos;
                currentPos += new Vector2(0, item.RenderSize.Y);
            }

            return entry.Position + new Vector2(0, entry.RenderSize.Y);
        }

        private void VisitInstance(MenuEntry parent, object instance, MenuEntry rootMenu)
        {
            this.VisitMenu(parent, instance, rootMenu);
            this.VisitItem(parent, instance, rootMenu);
        }

        private void VisitItem(MenuEntry parent, object instance, MenuEntry rootMenu)
        {
            var type = instance.GetType();
            foreach (var propertyInfo in type.GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                var menuItemAttribute = propertyInfo.GetCustomAttribute<ItemAttribute>();
                if (menuItemAttribute == null)
                {
                    continue;
                }

                var propertyValue = propertyInfo.GetValue(instance);
                if (propertyValue == null)
                {
                    throw new NullReferenceException($"{type.FullName} {propertyInfo.Name}");
                }

                var menuItemName = menuItemAttribute.Name;
                if (string.IsNullOrEmpty(menuItemName))
                {
                    menuItemName = propertyInfo.Name;
                }

                this.context.Container.BuildUp(propertyValue);

                var textureAttribute = propertyInfo.GetCustomAttribute<TexureAttribute>();

                var view = this.viewRepository.GetView(propertyInfo.PropertyType);

                MenuItemEntry menuItemEntry;
                if (propertyInfo.GetCustomAttribute<PermaShowAttribute>() != null)
                {
                    var tmp = new PermaMenuItemEntry(
                        menuItemName,
                        textureAttribute?.TextureKey,
                        view,
                        this.context.Renderer,
                        this.MenuConfig,
                        new ValuePropertyBinding(instance, propertyInfo));

                    tmp.RootMenuName = rootMenu.Name;
                    this.permaItemEntries.Add(tmp);

                    menuItemEntry = tmp;
                }
                else
                {
                    menuItemEntry = new MenuItemEntry(
                        menuItemName,
                        textureAttribute?.TextureKey,
                        view,
                        this.context.Renderer,
                        this.MenuConfig,
                        new ValuePropertyBinding(instance, propertyInfo));
                }

                var tooltip = propertyInfo.GetCustomAttribute<TooltipAttribute>();
                if (tooltip != null)
                {
                    menuItemEntry.Tooltip = tooltip.Text;
                }

                parent.AddChild(menuItemEntry);
            }
        }

        private void VisitMenu(MenuEntry parent, object instance, MenuEntry rootMenu)
        {
            var type = instance.GetType();
            foreach (var propertyInfo in type.GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                var menuAttribute = propertyInfo.GetCustomAttribute<MenuAttribute>();
                if (menuAttribute == null)
                {
                    continue;
                }

                var propertyValue = propertyInfo.GetValue(instance);
                if (propertyValue == null)
                {
                    throw new NullReferenceException($"{type.FullName} {propertyInfo.Name}");
                }

                var menuItemName = menuAttribute.Name;
                if (string.IsNullOrEmpty(menuItemName))
                {
                    menuItemName = type.Name;
                }

                this.context.Container.BuildUp(propertyValue);

                var textureAttribute = propertyInfo.GetCustomAttribute<TexureAttribute>();
                var menuItemEntry = new MenuEntry(
                    menuItemName,
                    textureAttribute?.TextureKey,
                    this.viewRepository.GetMenuView(),
                    this.context.Renderer,
                    this.MenuConfig,
                    propertyValue);
                this.VisitInstance(menuItemEntry, propertyValue, rootMenu);

                parent.AddChild(menuItemEntry);
            }
        }
    }
}