// <copyright file="PriorityChangerView.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Menu.Views
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Ensage.Common.Extensions;
    using Ensage.SDK.Input;
    using Ensage.SDK.Menu.Attributes;
    using Ensage.SDK.Menu.Entries;
    using Ensage.SDK.Menu.Items;

    using SharpDX;

    [ExportView(typeof(PriorityChanger))]
    public class PriorityChangerView : ImageTogglerView
    {
        private const int MinDragTime = 150;
        
        private Vector2 dragMousePosition;

        private KeyValuePair<string, bool>? dragObject;

        private int dragStartTime;

        public override void Draw(MenuBase context)
        {
            var item = (MenuItemEntry)context;
            var propValue = item.ValueBinding.GetValue<PriorityChanger>();

            var pos = context.Position;
            var size = context.RenderSize;

            var activeStyle = context.MenuConfig.GeneralConfig.ActiveStyle.Value;
            var styleConfig = activeStyle.StyleConfig;
            var border = styleConfig.Border;

            context.Renderer.DrawTexture(activeStyle.Item, new RectangleF(pos.X, pos.Y, size.X, size.Y));

            pos += new Vector2(border.Thickness[0], border.Thickness[1]);

            var font = styleConfig.Font;
            context.Renderer.DrawText(pos, context.Name, font.Color, font.Size, font.Family);

            var dragElementPos = this.dragMousePosition;

            var picturePickerStyle = styleConfig.PicturePicker;
            pos.X = (context.Position.X + size.X) - border.Thickness[2] - picturePickerStyle.PictureSize.X - picturePickerStyle.LineWidth;
            foreach (var state in propValue.Priorities.ToList().OrderByDescending(x => x.Value))
            {
                if (state.Key != this.dragObject?.Key)
                {
                    var rect = new RectangleF(pos.X, pos.Y, picturePickerStyle.PictureSize.X, picturePickerStyle.PictureSize.Y);
                    context.Renderer.DrawTexture(state.Key, rect);

                    var val = propValue.PictureStates.First(x => x.Key == state.Key);
                    context.Renderer.DrawRectangle(rect, val.Value ? picturePickerStyle.SelectedColor : picturePickerStyle.Color, picturePickerStyle.LineWidth);
                }
                else
                {
                    dragElementPos.X -= pos.X;
                    dragElementPos.Y = pos.Y;
                }

                pos.X -= picturePickerStyle.PictureSize.X + (picturePickerStyle.LineWidth * 2);
            }

            if (this.dragObject.HasValue)
            {
                dragElementPos.X = Game.MouseScreenPosition.X - dragElementPos.X;
                var dragRect = new RectangleF(dragElementPos.X, dragElementPos.Y, picturePickerStyle.PictureSize.X, picturePickerStyle.PictureSize.Y);
                context.Renderer.DrawTexture(this.dragObject.Value.Key, dragRect);
                context.Renderer.DrawRectangle(dragRect, this.dragObject.Value.Value ? picturePickerStyle.SelectedColor : picturePickerStyle.Color, picturePickerStyle.LineWidth);
            }
        }

        private int GetElementPosition(MenuBase context, Vector2 position)
        {
            var size = context.RenderSize;

            var styleConfig = context.MenuConfig.GeneralConfig.ActiveStyle.Value.StyleConfig;
            var border = styleConfig.Border;
            var picturePickerStyle = styleConfig.PicturePicker;

            var item = (MenuItemEntry)context;
            var propValue = item.ValueBinding.GetValue<PriorityChanger>();

            var result = 0;

            var minDistance = float.MaxValue;
            var posX = (context.Position.X + size.X) - border.Thickness[2] - picturePickerStyle.PictureSize.X - picturePickerStyle.LineWidth;
            for (var i = 0; i < propValue.PictureStates.Count; ++i)
            {
                var distance = Math.Abs(position.X - posX);
                if (distance < minDistance)
                {
                    result = i;
                    minDistance = distance;
                }

                posX -= picturePickerStyle.PictureSize.X + (picturePickerStyle.LineWidth * 2);
            }

            return propValue.PictureStates.Count - result - 1;
        }

        public override Vector2 GetSize(MenuBase context)
        {
            return base.GetSize(context);
        }

        public override bool OnClick(MenuBase context, MouseButtons buttons, Vector2 clickPosition)
        {
            var item = (MenuItemEntry)context;
            var propValue = item.ValueBinding.GetValue<PriorityChanger>();
            if (!this.dragObject.HasValue && (buttons & MouseButtons.LeftDown) == MouseButtons.LeftDown)
            {
                var state = this.GetItemUnderMouse(context, clickPosition);
                if (state.HasValue)
                {
                    this.dragObject = state;
                    this.dragMousePosition = clickPosition;
                    this.dragStartTime = Environment.TickCount;
                    //this.dragCollection = propValue.PictureStates.ToDictionary(entry => entry.Key, entry => entry.Value);
                    return true;
                }
            }

            if (this.dragObject.HasValue && (buttons & MouseButtons.LeftUp) == MouseButtons.LeftUp)
            {
                if ((Environment.TickCount - this.dragStartTime) > MinDragTime)
                {
                    var newIndex = this.GetElementPosition(context, clickPosition);
                    var currentIndex = propValue.Priorities.First(x => x.Key == this.dragObject.Value.Key).Value;
                    if (currentIndex != newIndex)
                    {
                        // update priority
                        if (newIndex < currentIndex)
                        {
                            foreach (var value in propValue.Priorities.Where(x => x.Value >= newIndex && x.Value < currentIndex).ToList())
                            {
                                propValue.Priorities[value.Key] = value.Value + 1;
                            }
                        }
                        else
                        {
                            foreach (var value in propValue.Priorities.Where(x => x.Value <= newIndex && x.Value > currentIndex).ToList())
                            {
                                propValue.Priorities[value.Key] = value.Value - 1;
                            }
                        }

                        propValue.Priorities[this.dragObject.Value.Key] = newIndex;
                    }

                    this.dragObject = null;
                    return true;
                }

                this.dragObject = null;
            }

            if (propValue.Selectable && (buttons & MouseButtons.LeftUp) == MouseButtons.LeftUp)
            {
                return base.OnClick(context, buttons, clickPosition);
            }

            return false;
        }

        protected override KeyValuePair<string, bool>? GetItemUnderMouse(MenuBase context, Vector2 mousePos) 
        {
            var item = (MenuItemEntry)context;
            var propValue = item.ValueBinding.GetValue<PriorityChanger>();

            var pos = context.Position;
            var size = context.RenderSize;

            var activeStyle = context.MenuConfig.GeneralConfig.ActiveStyle.Value;
            var styleConfig = activeStyle.StyleConfig;
            var border = styleConfig.Border;
            var picturePickerStyle = styleConfig.PicturePicker;

            pos.X += size.X - border.Thickness[2] - picturePickerStyle.PictureSize.X - picturePickerStyle.LineWidth;
            foreach (var state in propValue.Priorities.ToList().OrderByDescending(x => x.Value))
            {
                var testRect = new RectangleF(pos.X, pos.Y, picturePickerStyle.PictureSize.X + (picturePickerStyle.LineWidth * 2), size.Y);
                if (testRect.Contains(mousePos))
                {
                    return propValue.PictureStates.First(x => x.Key == state.Key);
                }

                pos.X -= picturePickerStyle.PictureSize.X + (picturePickerStyle.LineWidth * 2);
            }

            return null;
        }
    }
}