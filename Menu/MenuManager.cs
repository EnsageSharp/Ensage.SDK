// <copyright file="MenuManager.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Menu
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.Composition;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    using Ensage.Common.Extensions;
    using Ensage.SDK.Input;
    using Ensage.SDK.Menu.Config;
    using Ensage.SDK.Menu.Entries;
    using Ensage.SDK.Menu.Items;
    using Ensage.SDK.Menu.Styles;
    using Ensage.SDK.Renderer;
    using Ensage.SDK.Service;

    using log4net;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using Newtonsoft.Json.Linq;

    using PlaySharp.Toolkit.Helper;
    using PlaySharp.Toolkit.Helper.Annotations;
    using PlaySharp.Toolkit.Logging;

    using SharpDX;

    [Export]
    [PublicAPI]
    public class MenuManager : ControllableService
    {
        private static readonly ILog Log = AssemblyLogs.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly string configDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config", "game");

        private readonly IInputManager input;

        private readonly IRendererManager renderer;

        private readonly List<MenuEntry> rootMenus = new List<MenuEntry>();

        private readonly StyleRepository styleRepository;

        private readonly ViewRepository viewRepository;

        private bool blockedLeftClick;

        private MenuEntry dragMenuEntry;

        private Vector2 dragStartPosition;

        private MenuBase lastHoverEntry;

        private Vector2 position;

        private bool positionDirty;

        private bool sizeDirty;

        [ImportingConstructor]
        public MenuManager([Import] ViewRepository viewRepository, [Import] IRendererManager renderer, [Import] StyleRepository styleRepository, [Import] IInputManager input)
        {
            // TODO extract interface
            this.viewRepository = viewRepository;
            this.renderer = renderer;
            this.styleRepository = styleRepository;
            this.input = input;

            try
            {
                Directory.CreateDirectory(configDirectory);
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        public bool IsVisible { get; private set; } = true;

        public MenuBase LastHoverEntry
        {
            get
            {
                return lastHoverEntry;
            }

            set
            {
                if (lastHoverEntry != null)
                {
                    lastHoverEntry.IsHovered = false;
                }

                lastHoverEntry = value;
                if (lastHoverEntry != null)
                {
                    lastHoverEntry.IsHovered = true;
                }
            }
        }

        public MenuConfig MenuConfig { get; private set; }

        /// <summary>
        ///     Gets or sets the upper left corner position of the menu.
        /// </summary>
        public Vector2 Position
        {
            get
            {
                return position;
            }

            set
            {
                position = value;
                positionDirty = true;
            }
        }

        /// <summary>
        ///     Gets the total size of the menu as a rectangle.
        /// </summary>
        public Vector2 Size { get; private set; }

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
                SaveMenu(menu);
            }

            return rootMenus.RemoveAll(x => x.DataContext == menu) != 0;
        }

        public bool IsInsideMenu(Vector2 screenPosition)
        {
            return Position.X <= screenPosition.X
                && Position.Y <= screenPosition.Y
                && screenPosition.X <= (Position.X + Size.X)
                && screenPosition.Y <= (Position.Y + Size.Y);
        }

        public bool LoadMenu(object menu)
        {
            try
            {
                var type = menu.GetType();
                var assemblyName = type.Assembly.GetName().Name;
                var savePath = Path.Combine(configDirectory, assemblyName);
                var file = Path.Combine(savePath, $"{type.FullName}.json");
                if (!File.Exists(file))
                {
                    return false;
                }

                var rootMenu = rootMenus.First(x => x.DataContext == menu);
                var token = JToken.Parse(File.ReadAllText(file));

                LoadLayer(rootMenu, token);
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
            if (positionDirty)
            {
                // recalculate positions
                var pos = Position;
                foreach (var menuEntry in rootMenus)
                {
                    pos = UpdateMenuEntryPosition(menuEntry, pos);
                }

                sizeDirty = true;
                positionDirty = false;
             }

            if (sizeDirty)
            {
                // recalculate size
                CalculateMenuRenderSize(rootMenus);
                Size = CalculateMenuTotalSize(rootMenus);

                // recalculate positions by rendersize
                var pos = Position;
                foreach (var menuEntry in rootMenus)
                {
                    pos = UpdateMenuEntryRenderPosition(menuEntry, pos);
                }

                sizeDirty = false;
            }

            foreach (var menuEntry in rootMenus)
            {
                DrawMenuEntry(menuEntry);
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

            if (rootMenus.Any(x => x.DataContext == menu))
            {
                throw new ArgumentException($"{menu} is already registered");
            }

            var menuName = sdkAttribute.Name;
            if (string.IsNullOrEmpty(menuName))
            {
                menuName = dataType.Name;
            }

            var view = viewRepository.GetMenuView();

            var menuEntry = new MenuEntry(menuName, view, renderer, MenuConfig, menu, null);
            VisitInstance(menuEntry, menu);

            rootMenus.Add(menuEntry);

            LoadMenu(menu);

            positionDirty = true;
            sizeDirty = true;

            return menuEntry;
        }

        public bool SaveMenu(object menu)
        {
            try
            {
                var type = menu.GetType();
                var assemblyName = type.Assembly.GetName().Name;
                var savePath = Path.Combine(configDirectory, assemblyName);
                Directory.CreateDirectory(savePath);

                var file = Path.Combine(savePath, $"{type.FullName}.json");

                if (rootMenus.All(x => x.DataContext != menu))
                {
                    throw new Exception($"{menu} not registered as menu");
                }

                var settings = new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented,
                    DefaultValueHandling = DefaultValueHandling.Include | DefaultValueHandling.Populate,
                    Converters =
                    {
                        new MenuStyleConverter(styleRepository),
                        new StringEnumConverter(),
                    },
                    NullValueHandling = NullValueHandling.Ignore,
                    TypeNameHandling = TypeNameHandling.Auto,
                };
                JsonFactory.ToFile(file, menu, settings);
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
            MenuConfig = new MenuConfig();
            MenuConfig.GeneralConfig.ActiveStyle = new Selection<IMenuStyle>(styleRepository.Styles.ToArray());
            MenuConfig.GeneralConfig.ActiveStyle.Value = styleRepository.DefaultMenuStyle;

            RegisterMenu(MenuConfig);

            renderer.Draw += OnDraw;
            input.MouseMove += OnMouseMove;
            input.MouseClick += OnMouseClick;

            Position = new Vector2(200, 50); // TODO MenuConfig.Position
        }

        protected override void OnDeactivate()
        {
            renderer.Draw -= OnDraw;
            input.MouseMove -= OnMouseMove;
            input.MouseClick -= OnMouseClick;

            foreach (var menuEntry in rootMenus)
            {
                SaveMenu(menuEntry.DataContext);
            }

            MenuConfig = null;
        }

        private static void LoadLayer(MenuEntry menu, JToken token)
        {
            foreach (var child in menu.Children.OfType<MenuItemEntry>())
            {
                var entry = token[child.PropertyInfo.Name];
                if (entry != null)
                {
                    child.Value = entry.ToObject(child.PropertyInfo.PropertyType);
                }
                else
                {
                    // set default value by attribute
                    var defaultValue = child.PropertyInfo.GetCustomAttribute<DefaultValueAttribute>();
                    if (defaultValue != null)
                    {
                        child.Value = defaultValue.Value;
                    }
                }
            }

            foreach (var child in menu.Children.OfType<MenuEntry>())
            {
                var subToken = token[child.PropertyInfo.Name];
                if (subToken != null)
                {
                    LoadLayer(child, subToken);
                }
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
                    CalculateMenuRenderSize(menuEntry.Children);
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
                    var childSize = CalculateMenuTotalSize(menuEntry.Children);
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
                if (CollapseLayer(menuEntry, entry.Children.OfType<MenuEntry>().ToList()))
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
                DrawMenuEntry(menu);
            }

            foreach (var item in entry.Children.OfType<MenuItemEntry>())
            {
                item.Draw();
            }
        }

        [CanBeNull]
        private MenuBase OnClickCheck(MenuEntry entry, Vector2 mousePosition)
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
                entry.OnClick(mousePosition);
                return entry;
            }

            MenuEntry hoveredEntry = null;
            foreach (var menu in entry.Children.OfType<MenuEntry>())
            {
                if (OnClickCheck(menu, mousePosition) != null)
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
                        item.OnClick(mousePosition);
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
                var hoveredEntry = OnInsideCheck(menu, mousePosition);
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
                // handle drag and drop
                if (dragMenuEntry != null)
                {
                    // TODO:
                    dragMenuEntry = null;
                }

                if (blockedLeftClick)
                {
                    e.Process = false;
                    blockedLeftClick = false;
                }

                return;
            }

            if (!IsVisible || !IsInsideMenu(e.Position))
            {
                return;
            }

            // check for click
            if (LastHoverEntry != null)
            {
                LastHoverEntry.OnClick(e.Position);
                if (LastHoverEntry is MenuEntry menuEntry)
                {
                    if (!menuEntry.IsCollapsed)
                    {
                        CollapseLayer(menuEntry, rootMenus);
                    }
                }

                e.Process = false;
                blockedLeftClick = true;
            }
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (!IsVisible || !IsInsideMenu(e.Position))
            {
                if (LastHoverEntry != null)
                {
                    Log.Info($"MouseLeave {LastHoverEntry}");
                    LastHoverEntry = null;
                }

                return;
            }

            if (dragMenuEntry != null)
            {
                return;
            }

            // check for mouse hover
            foreach (var menuEntry in rootMenus)
            {
                var hoverItem = OnInsideCheck(menuEntry, e.Position);
                if (hoverItem != null)
                {
                    if (LastHoverEntry != hoverItem)
                    {
                        if (LastHoverEntry != null)
                        {
                            Log.Info($"MouseLeave {LastHoverEntry}");
                        }

                        Log.Info($"MouseHover {hoverItem}");
                        LastHoverEntry = hoverItem;

                        // check for drag and drop of menu entries (possible to swap positions)
                        if ((e.Buttons & MouseButtons.Left) != 0 && LastHoverEntry is MenuEntry hoverEntry && LastHoverEntry.DataContext != MenuConfig)
                        {
                            dragStartPosition = e.Position;
                            dragMenuEntry = hoverEntry;
                        }
                    }

                    return;
                }
            }

            if (LastHoverEntry != null)
            {
                Log.Info($"MouseLeave {LastHoverEntry}");
                LastHoverEntry = null;
            }
        }

        private Vector2 UpdateMenuEntryPosition(MenuEntry entry, Vector2 pos)
        {
            entry.Position = pos;

            var currentPos = pos + new Vector2(entry.Size.X, 0);
            foreach (var menu in entry.Children.OfType<MenuEntry>())
            {
                currentPos = UpdateMenuEntryPosition(menu, currentPos);
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
                currentPos = UpdateMenuEntryRenderPosition(menu, currentPos);
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
            VisitMenu(parent, instance);
            VisitItem(parent, instance);
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

                var view = viewRepository.GetView(propertyInfo.PropertyType);
                var menuItemEntry = new MenuItemEntry(menuItemName, view, renderer, MenuConfig, instance, propertyInfo);

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

                var menuItemEntry = new MenuEntry(menuItemName, viewRepository.GetMenuView(), renderer, MenuConfig, propertyValue, propertyInfo);
                VisitInstance(menuItemEntry, propertyValue);

                parent.AddChild(menuItemEntry);
            }
        }
    }
}