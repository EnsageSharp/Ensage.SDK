// <copyright file="CircularIndicatorHelper.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Helpers
{
    using System.Collections.Generic;

    using SharpDX;

    public static class CircularIndicatorHelper
    {
        private static Dictionary<string, CircularIndicator> CircleIndicators =
            new Dictionary<string, CircularIndicator>();

        public static void DrawRange(this Unit unit, string id, float range)
        {
            range *= 1.1f;
            if (!CircleIndicators.ContainsKey(id))
            {
                CircleIndicators.Add(id, new CircularIndicator());
            }

            var indicator = CircleIndicators[id];

            if (indicator.Range != range && indicator.Effect != null)
            {
                indicator.Effect.Dispose();
                indicator.Effect = null;
            }

            if (indicator.Effect == null)
            {
                indicator.Effect = unit.AddParticleEffect("particles/ui_mouseactions/drag_selected_ring.vpcf");
                indicator.Effect.SetControlPoint(1, new Vector3(0, 255, 255));

                indicator.Range = range;
                indicator.Effect.SetControlPoint(2, new Vector3(range, 255, 0));
            }
        }
    }
}