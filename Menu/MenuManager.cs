// <copyright file="MenuManager.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Menu
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Windows.Forms;

    using Ensage.Common.Extensions;
    using Ensage.SDK.Input;
    using Ensage.SDK.Menu.Config;
    using Ensage.SDK.Renderer;
    using Ensage.SDK.Service;

    using log4net;

    using PlaySharp.Toolkit.Helper.Annotations;
    using PlaySharp.Toolkit.Logging;

    using SharpDX;

    using MouseEventArgs = Ensage.SDK.Input.MouseEventArgs;

    [Export]
    [PublicAPI]
    public class MenuManager : ControllableService
    {
        private static readonly ILog Log = AssemblyLogs.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly IInputManager input;

        private readonly IRendererManager renderer;

        private readonly List<MenuEntry> rootMenus = new List<MenuEntry>();

        private readonly ViewRepository viewRepository;

        private MenuBase lastHoverEntry;

        private Vector2 position;

        private bool positionDirty;

        private bool sizeDirty;

        [ImportingConstructor]
        public MenuManager([Import] ViewRepository viewRepository, [Import] IRendererManager renderer, [Import] IInputManager input)
        {
            this.viewRepository = viewRepository;
            this.renderer = renderer;
            this.input = input;
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

        public bool DeregisterMenu(object menu)
        {
            var dataType = menu.GetType();
            var sdkAttribute = dataType.GetCustomAttribute<MenuAttribute>();
            if (sdkAttribute == null)
            {
                throw new Exception($"Missing attribute {nameof(MenuAttribute)}");
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

        public bool SaveMenu(object menu)
        {
            var file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config", "Assemblies", "ensage.sdk.json");

            return true;
        }

        public bool LoadMenu(object menu)
        {
            try
            {
                var file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config", "Assemblies", "ensage.sdk.json");
                if (!File.Exists(file))
                {
                    return false;
                }



                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public void RegisterMenu(object menu)
        {
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

            var menuEntry = new MenuEntry(menuName, view, this.renderer, menu);
            this.VisitInstance(menuEntry, menu);

            this.rootMenus.Add(menuEntry);
            this.positionDirty = true;
            this.sizeDirty = true;
        }

        protected override void OnActivate()
        {
            this.MenuConfig = new MenuConfig();
            this.RegisterMenu(this.MenuConfig);

            this.renderer.Draw += this.OnDraw;
            this.input.MouseMove += this.OnMouseMove;
            this.input.MouseClick += this.OnMouseClick;

            // TODO: load config
            this.Position = new Vector2(200, 50);
        }

        protected override void OnDeactivate()
        {
            this.renderer.Draw -= this.OnDraw;
            this.input.MouseMove -= this.OnMouseMove;
            this.input.MouseClick -= this.OnMouseClick;

            this.DeregisterMenu(this.MenuConfig);
            this.MenuConfig = null;

            // TODO: save to config
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

            // TODO: test for left button down to drag menu items

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

                var menuItemEntry = new MenuEntry(menuItemName, this.viewRepository.GetMenuView(), this.renderer, propertyValue);
                this.VisitInstance(menuItemEntry, propertyValue);

                parent.AddChild(menuItemEntry);
            }
        }
    }
}