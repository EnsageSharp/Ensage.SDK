// <copyright file="PriorityChanger.cs" company="Ensage">
//    Copyright (c) 2018 Ensage.
// </copyright>

namespace Ensage.SDK.Menu.Items
{
    using System.Collections.Generic;
    using System.Linq;

    using Newtonsoft.Json;

    public class PriorityChanger : ImageToggler
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

        public override bool AddImage(bool state, params string[] textureKey)
        {
            if (base.AddImage(state, textureKey))
            {
                this.FixPrioritiesAfterAddImage();
                return true;
            }

            return false;
        }

        public override bool AddImage(params KeyValuePair<string, bool>[] values)
        {
            if (base.AddImage(values))
            {
                this.FixPrioritiesAfterAddImage();
                return true;
            }

            return false;
        }

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

                // this.Priorities = other.Priorities;
                this.Priorities = other.Priorities.Where(x => this.Priorities.ContainsKey(x.Key)).ToDictionary(x => x.Key, x => x.Value);

                // TODO: fix multiple same values
                return true;
            }

            return false;
        }

        public override bool RemoveImage(string textureKey)
        {
            if (base.RemoveImage(textureKey))
            {
                var removedPriority = this.Priorities[textureKey];
                this.Priorities.Remove(textureKey);

                // shift higher priorities down
                foreach (var priority in this.Priorities.Where(x => x.Value >= removedPriority).ToList())
                {
                    this.Priorities[priority.Key] = priority.Value - 1;
                }

                return true;
            }

            return false;
        }

        private void FixPrioritiesAfterAddImage()
        {
            var newValues = this.PictureStates.Keys.Except(this.Priorities.Keys);
            var nextPriority = 0;
            if (this.Priorities.Count > 0)
            {
                nextPriority = this.Priorities.Values.Max() + 1;
            }

            foreach (var newValue in newValues)
            {
                this.Priorities[newValue] = nextPriority;
                nextPriority++;
            }
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