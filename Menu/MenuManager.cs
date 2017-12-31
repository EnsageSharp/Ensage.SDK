// <copyright file="MenuManager.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
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
    using Ensage.SDK.Menu.Config;
    using Ensage.SDK.Menu.Entries;
    using Ensage.SDK.Menu.Items;
    using Ensage.SDK.Menu.Styles;
    using Ensage.SDK.Service;

    using log4net;

    using Newtonsoft.Json.Converters;
    using Newtonsoft.Json.Linq;

    using PlaySharp.Toolkit.Helper.Annotations;
    using PlaySharp.Toolkit.Logging;

    using SharpDX;

    [Export]
    [PublicAPI]
    public class MenuManager : ControllableService
    {
        private static readonly ILog Log = AssemblyLogs.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly IServiceContext context;

        private readonly List<MenuEntry> rootMenus = new List<MenuEntry>();

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

        [ImportingConstructor]
        public MenuManager([Import] IServiceContext context, [Import] ViewRepository viewRepository, [Import] StyleRepository styleRepository)
        {
            // TODO extract interface
            this.context = context;
            this.viewRepository = viewRepository;
            this.styleRepository = styleRepository;
        }

        public bool IsVisible { get; private set; } = true;

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
            if (this.positionDirty)
            {
                // recalculate positions
                var pos = this.MenuPosition;
                foreach (var menuEntry in this.rootMenus)
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

                // recalculate positions by rendersize
                var pos = this.MenuPosition;
                foreach (var menuEntry in this.rootMenus)
                {
                    pos = this.UpdateMenuEntryRenderPosition(menuEntry, pos);
                }

                this.sizeDirty = false;
            }

            if (!this.IsVisible)
            {
                return;
            }

            var activeStyle = this.MenuConfig.GeneralConfig.ActiveStyle.Value;
            var titleBar = activeStyle.StyleConfig.TitleBar;
            this.context.Renderer.DrawTexture(activeStyle.TitleBar, new RectangleF(this.Position.X, this.Position.Y, this.TitleBarSize.X, this.TitleBarSize.Y));
            this.context.Renderer.DrawText(
                this.Position + new Vector2(titleBar.Border.Thickness[0], titleBar.Border.Thickness[1]),
                "Menu",
                titleBar.Font.Color,
                titleBar.Font.Size,
                titleBar.Font.Family);

            foreach (var menuEntry in this.rootMenus)
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

            this.context.Container.BuildUpWithImportCheck(menu);

            var menuName = sdkAttribute.Name;
            if (string.IsNullOrEmpty(menuName))
            {
                menuName = dataType.Name;
            }

            var view = this.viewRepository.GetMenuView();
            var textureAttribute = dataType.GetCustomAttribute<TexureAttribute>();
            var menuEntry = new MenuEntry(menuName, textureAttribute?.TextureKey, view, this.context.Renderer, this.MenuConfig, menu, null);
            this.VisitInstance(menuEntry, menu);

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
            Messenger<DynamicMenuAddedMessage>.Subscribe(this.DynamicMenuAddedMessage);
            Messenger<DynamicMenuRemovedMessage>.Subscribe(this.DynamicMenuRemovedMessage);

            this.menuSerializer = new MenuSerializer(new StringEnumConverter(), new MenuStyleConverter(this.styleRepository));

            this.MenuConfig = new MenuConfig();
            this.MenuConfig.GeneralConfig.ActiveStyle = new Selection<IMenuStyle>(this.styleRepository.Styles.ToArray());
            this.MenuConfig.GeneralConfig.ActiveStyle.Value = this.styleRepository.DefaultMenuStyle;

            this.Position = this.MenuConfig.MenuPosition;

            var titleBar = this.MenuConfig.GeneralConfig.ActiveStyle.Value.StyleConfig.TitleBar;
            this.TitleBarSize = this.context.Renderer.MessureText("Menu", titleBar.Font.Size, titleBar.Font.Family)
                                + new Vector2(titleBar.Border.Thickness[0] + titleBar.Border.Thickness[2], titleBar.Border.Thickness[1] + titleBar.Border.Thickness[3]);
            this.context.Renderer.TextureManager.LoadFromResource("menuStyle/logo", @"MenuStyle.logo.png");

            this.RegisterMenu(this.MenuConfig);

            this.context.Renderer.Draw += this.OnDraw;
            this.context.Input.MouseMove += this.OnMouseMove;
            this.context.Input.MouseClick += this.OnMouseClick;
        }

        protected override void OnDeactivate()
        {
            Messenger<AddDynamicMenuMessage>.Unsubscribe(this.AddDynamicMenu);
            Messenger<DynamicMenuAddedMessage>.Unsubscribe(this.DynamicMenuAddedMessage);
            Messenger<DynamicMenuRemovedMessage>.Unsubscribe(this.DynamicMenuRemovedMessage);

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
                var type = args.Instance.GetType();
                var propertyInfo = type.GetProperties(BindingFlags.Instance | BindingFlags.Public).First(); // todo need to get actual property info -> so can't add items directly only classes?
                var view = this.viewRepository.GetView(propertyInfo.PropertyType);
                var menuItemEntry = new MenuItemEntry(args.Name, args.TextureKey, view, this.context.Renderer, this.MenuConfig, args.Instance, propertyInfo);
                args.DynamicMenu.AddMenuItem(menuItemEntry);
            }
            else
            {
                var type = args.Instance.GetType();
                var propertyInfo = type.GetProperties(BindingFlags.Instance | BindingFlags.Public).First();
                var menuItemEntry = new MenuEntry(
                    args.Name,
                    args.TextureKey,
                    this.viewRepository.MenuView.Value,
                    this.context.Renderer,
                    this.MenuConfig,
                    args.Instance,
                    propertyInfo);
                args.DynamicMenu.AddMenu(menuItemEntry);
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

        private Vector2 CalculateMenuTotalSize(IEnumerable<MenuBase> entries)
        {
            var entryList = entries.ToList();

            var first = entryList.FirstOrDefault();
            if (first == null)
            {
                return Vector2.Zero;
            }

            var firstSize = first.RenderSize;
            var totalSize = new Vector2(firstSize.X, entryList.Count * firstSize.Y);
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

            foreach (var menu in entry.Children.OfType<MenuEntry>())
            {
                this.DrawMenuEntry(menu);
            }

            foreach (var item in entry.Children.OfType<MenuItemEntry>())
            {
                item.Draw();
            }
        }

        private void DynamicMenuAddedMessage(DynamicMenuAddedMessage obj)
        {
            this.positionDirty = this.sizeDirty = true;
        }

        private void DynamicMenuRemovedMessage(DynamicMenuRemovedMessage obj)
        {
            foreach (var menuEntry in this.rootMenus)
            {
                var found = this.FindParentMenu(menuEntry, obj.MenuBase);
                if (found != null)
                {
                    found.RemoveChild(obj.MenuBase);
                    this.positionDirty = this.sizeDirty = true;
                    return;
                }
            }
        }

        private void LoadLayer(MenuEntry menu, JToken token)
        {
            foreach (var child in menu.Children.OfType<MenuItemEntry>())
            {
                var entry = token[child.PropertyInfo.Name];
                if (entry != null)
                {
                    if (child.Value is ILoadable loadable)
                    {
                        var loaded = this.menuSerializer.ToObject(entry, child.PropertyInfo.PropertyType);
                        loadable.Load(loaded);
                    }
                    else
                    {
                        child.Value = this.menuSerializer.ToObject(entry, child.PropertyInfo.PropertyType);
                    }

                    child.AssignDefaultValue();
                }
                else
                {
                    // set default value by attribute
                    var defaultValue = child.PropertyInfo.GetCustomAttribute<DefaultValueAttribute>();
                    if (defaultValue != null)
                    {
                        child.Value = defaultValue.Value;
                        child.AssignDefaultValue();
                    }
                }
            }

            foreach (var child in menu.Children.OfType<MenuEntry>())
            {
                var subToken = token[child.PropertyInfo.Name];
                if (subToken != null)
                {
                    this.LoadLayer(child, subToken);
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

            if (!this.IsVisible || !this.IsInsideMenu(e.Position))
            {
                return;
            }

            if (this.titleBarHovered && (e.Buttons & MouseButtons.LeftDown) == MouseButtons.LeftDown)
            {
                Log.Debug($"start title bar dragging");
                this.dragMouseDiff = e.Position - this.Position;
                this.titleBarDragged = true;
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
                this.Position = e.Position - this.dragMouseDiff;
                return;
            }

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

            // check for titlebar mouseover
            var titleBar = new RectangleF(this.Position.X, this.Position.Y, this.TitleBarSize.X, this.TitleBarSize.Y);
            if (titleBar.Contains(e.Position))
            {
                this.titleBarHovered = true;
            }
            else
            {
                this.titleBarHovered = false;

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

        private void VisitInstance(MenuEntry parent, object instance)
        {
            this.VisitMenu(parent, instance);
            this.VisitItem(parent, instance);
        }

        private void VisitItem(MenuEntry parent, object instance)
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

                this.context.Container.BuildUpWithImportCheck(propertyValue);

                var textureAttribute = propertyInfo.GetCustomAttribute<TexureAttribute>();

                var view = this.viewRepository.GetView(propertyInfo.PropertyType);
                var menuItemEntry = new MenuItemEntry(menuItemName, textureAttribute?.TextureKey, view, this.context.Renderer, this.MenuConfig, instance, propertyInfo);

                var tooltip = propertyInfo.GetCustomAttribute<TooltipAttribute>();
                if (tooltip != null)
                {
                    menuItemEntry.Tooltip = tooltip.Text;
                }

                parent.AddChild(menuItemEntry);
            }
        }

        private void VisitMenu(MenuEntry parent, object instance)
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

                this.context.Container.BuildUpWithImportCheck(propertyValue);

                var textureAttribute = propertyInfo.GetCustomAttribute<TexureAttribute>();
                var menuItemEntry = new MenuEntry(
                    menuItemName,
                    textureAttribute?.TextureKey,
                    this.viewRepository.GetMenuView(),
                    this.context.Renderer,
                    this.MenuConfig,
                    propertyValue,
                    propertyInfo);
                this.VisitInstance(menuItemEntry, propertyValue);

                parent.AddChild(menuItemEntry);
            }
        }
    }
}