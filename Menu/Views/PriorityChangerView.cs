// <copyright file="PriorityChangerView.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Menu.Views
{
    using System.Collections.Generic;

    using Ensage.SDK.Input;
    using Ensage.SDK.Menu.Attributes;
    using Ensage.SDK.Menu.Entries;
    using Ensage.SDK.Menu.Items;

    using SharpDX;

    [ExportView(typeof(PriorityChanger))]
    public class PriorityChangerView : PicturePickerView
    {
        private KeyValuePair<string, bool>? dragObject;

        public override void Draw(MenuBase context)
        {
            if (!dragObject.HasValue)
            {
                base.Draw(context);
                return;
            }
        }

        public override bool OnClick(MenuBase context, MouseButtons buttons, Vector2 clickPosition)
        {
            var item = (MenuItemEntry)context;
            var propValue = item.PropertyBinding.GetValue<PriorityChanger>();
            if ((buttons & MouseButtons.LeftDown) == MouseButtons.LeftDown)
            {
                var state = GetItemUnderMouse(context, clickPosition);
                if (state.HasValue)
                {
                    dragObject = state;
                    return true;
                }
    
            }

            if (dragObject.HasValue && (buttons & MouseButtons.LeftUp) == MouseButtons.LeftUp)
            {
                dragObject = null;
                return true;
            }

            if (propValue.Selectable && (buttons & MouseButtons.LeftUp) == MouseButtons.LeftUp)
            {
                return base.OnClick(context, buttons, clickPosition);
            }

            return false;
        }

        public override Vector2 GetSize(MenuBase context)
        {
            return base.GetSize(context);
        }
    }
}