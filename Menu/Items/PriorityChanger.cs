// <copyright file="PriorityChanger.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Menu.Items
{
    using System.Collections.Generic;
    using System.Linq;

    using Newtonsoft.Json;

    public class PriorityChanger : PicturePicker
    {
        public PriorityChanger()
        {
        }

        public PriorityChanger(bool defaultValue = false, params string[] textureKeys)
            : base(defaultValue, textureKeys)
        {
            this.Selectable = true;
            this.InitIndices();
        }

        public PriorityChanger(params KeyValuePair<string, bool>[] values)
            : base(values)
        {
            this.Selectable = true;
            this.InitIndices();
        }

        public PriorityChanger(params string[] textureKeys)
            : base(false, textureKeys)
        {
            this.Selectable = false;
            this.InitIndices();
        }

        public Dictionary<string, int> Priorities { get; set; } = new Dictionary<string, int>();

        /// <summary>
        ///     Gets the currently active priority.
        /// </summary>
        [JsonIgnore]
        public IEnumerable<string> Priority
        {
            get
            {
                return this.Priorities.OrderBy(x => x.Value).Select(x => x.Key);
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether the items can be selected and unselected.
        /// </summary>
        public bool Selectable { get; set; }

        public override object Clone()
        {
            var result = (PriorityChanger)base.Clone();
            result.Priorities = new Dictionary<string, int>(this.Priorities);
            return result;
        }

        public override bool Load(object data)
        {
            if (base.Load(data))
            {
                var other = (PriorityChanger)data;
                this.Selectable = other.Selectable;
                this.Priorities = other.Priorities;
                return true;
            }

            return false;
        }

        private void InitIndices()
        {
            var index = 0;
            foreach (var state in this.PictureStates)
            {
                this.Priorities[state.Key] = index++;
            }
        }
    }
}