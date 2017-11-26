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
    using Ensage.SDK.Renderer;
    using Ensage.SDK.Service;

    using log4net;

    using Newtonsoft.Json;
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

        private readonly ViewRepository viewRepository;

        private bool blockedLeftClick;

        private MenuBase lastHoverEntry;

        private Vector2 position;

        private bool positionDirty;

        private bool sizeDirty;

        [ImportingConstructor]
        public MenuManager([Import] ViewRepository viewRepository, [Import] IRendererManager renderer, [Import] IInputManager input)
        {
            // TODO extract interface
            this.viewRepository = viewRepository;
            this.renderer = renderer;
            this.input = input;

            try
            {
                Directory.CreateDirectory(this.configDirectory);
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
                var type = menu.GetType();
                var assemblyName = type.Assembly.GetName().Name;
                var savePath = Path.Combine(this.configDirectory, assemblyName);
                var file = Path.Combine(savePath, $"{type.FullName}.json");
                if (!File.Exists(file))
                {
                    return false;
                }

                var rootMenu = this.rootMenus.First(x => x.DataContext == menu);
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
            if (this.positionDirty)
            {
                // recalculate positions
                var pos = this.Position;
                foreach (var menuEntry in this.rootMenus)
                {
                    pos = this.UpdateMenuEntryPosition(menuEntry, pos);
                }

                this.positionDirty = false;
            }

            if (this.sizeDirty)
            {
                // recalculate size
                this.CalculateMenuRenderSize(this.rootMenus);
                this.Size = this.CalculateMenuTotalSize(this.rootMenus);

                this.sizeDirty = false;
            }

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

            var menuName = sdkAttribute.Name;
            if (string.IsNullOrEmpty(menuName))
            {
                menuName = dataType.Name;
            }

            var view = this.viewRepository.GetMenuView();

            var menuEntry = new MenuEntry(menuName, view, this.renderer, menu, null);
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
                var type = menu.GetType();
                var assemblyName = type.Assembly.GetName().Name;
                var savePath = Path.Combine(this.configDirectory, assemblyName);
                Directory.CreateDirectory(savePath);

                var file = Path.Combine(savePath, $"{type.FullName}.json");

                if (this.rootMenus.All(x => x.DataContext != menu))
                {
                    throw new Exception($"{menu} not registered as menu");
                }

                var settings = new JsonSerializerSettings
                                   {
                                       DefaultValueHandling = DefaultValueHandling.Include
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
            this.MenuConfig = new MenuConfig();
            this.RegisterMenu(this.MenuConfig);

            this.renderer.Draw += this.OnDraw;
            this.input.MouseMove += this.OnMouseMove;
            this.input.MouseClick += this.OnMouseClick;

            this.Position = new Vector2(200, 50); // TODO MenuConfig.Position
        }

        protected override void OnDeactivate()
        {
            this.renderer.Draw -= this.OnDraw;
            this.input.MouseMove -= this.OnMouseMove;
            this.input.MouseClick -= this.OnMouseClick;

            foreach (var menuEntry in this.rootMenus)
            {
                this.SaveMenu(menuEntry.DataContext);
            }

            this.MenuConfig = null;
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
            foreach (var child in entryList)
            {
                var menuEntry = child as MenuEntry;
                if (menuEntry != null)
                {
                    var childSize = this.CalculateMenuTotalSize(menuEntry.Children);
                    menuEntry.TotalSize = childSize + new Vector2(firstSize.X, 0);

                    totalSize.X = Math.Max(totalSize.X, firstSize.X + childSize.X);
                    totalSize.Y = Math.Max(totalSize.Y, childSize.Y);
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
                if (this.OnClickCheck(menu, mousePosition) != null)
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
                return;
            }

            if (!this.IsVisible || !this.IsInsideMenu(e.Position))
            {
                return;
            }

            // check for click
            if (this.LastHoverEntry != null)
            {
                this.LastHoverEntry.OnClick(e.Position);
                if (this.LastHoverEntry is MenuEntry menuEntry)
                {
                    if (!menuEntry.IsCollapsed)
                    {
                        this.CollapseLayer(menuEntry, this.rootMenus);
                    }
                }

                e.Process = false;
                this.blockedLeftClick = true;
            }
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
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

            if (this.LastHoverEntry != null)
            {
                Log.Info($"MouseLeave {this.LastHoverEntry}");
                this.LastHoverEntry = null;
            }
        }

        private Vector2 dragStartPosition;

        private MenuEntry dragMenuEntry;

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

                var view = this.viewRepository.GetView(propertyInfo.PropertyType);
                var menuItemEntry = new MenuItemEntry(menuItemName, view, this.renderer, instance, propertyInfo);

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

                var menuItemEntry = new MenuEntry(menuItemName, this.viewRepository.GetMenuView(), this.renderer, propertyValue, propertyInfo);
                this.VisitInstance(menuItemEntry, propertyValue);

                parent.AddChild(menuItemEntry);
            }
        }
    }
}