// <copyright file="Utils.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Utils
{
    using System;

    public static class Utils
    {
        /// <summary>
        ///     Gets the pseudo chance from dota's used random distribution calculation.
        /// </summary>
        /// <param name="chance">The actual chance from 0 to 1.</param>
        /// <returns>The pseudo chance for each try.</returns>
        public static float GetPseudoChance(float chance)
        {
            if (chance == 0.05f)
            {
                return 0.00380f;
            }
            else if (chance == 0.10f)
            {
                return 0.01475f;
            }
            else if (chance == 0.15f)
            {
                return 0.03221f;
            }
            else if (chance == 0.20f)
            {
                return 0.05570f;
            }
            else if (chance == 0.25f)
            {
                return 0.08475f;
            }
            else if (chance == 0.30f)
            {
                return 0.11895f;
            }
            else if (chance == 0.35f)
            {
                return 0.14628f;
            }
            else if (chance == 0.40f)
            {
                return 0.18128f;
            }
            else if (chance == 0.45f)
            {
                return 0.21867f;
            }
            else if (chance == 0.50f)
            {
                return 0.25701f;
            }
            else if (chance == 0.55f)
            {
                return 0.29509f;
            }
            else if (chance == 0.60f)
            {
                return 0.33324f;
            }
            else if (chance == 0.65f)
            {
                return 0.38109f;
            }
            else if (chance == 0.70f)
            {
                return 0.42448f;
            }

            decimal cmid;
            var cupper = (decimal)chance;
            var clower = 0m;
            var p2 = 1m;
            while (true)
            {
                cmid = (cupper + clower) / 2m;
                var p1 = ComputePseudoFromChance(cmid);
                if (Math.Abs(p1 - p2) <= 0m)
                {
                    break;
                }

                if (p1 > (decimal)chance)
                {
                    cupper = cmid;
                }
                else
                {
                    clower = cmid;
                }

                p2 = p1;
            }

            return (float)cmid;
        }

        private static decimal ComputePseudoFromChance(decimal chance)
        {
            var procByN = 0m;
            var sumNpProcOnN = 0m;

            var maxFails = (int)Math.Ceiling(1m / chance);
            for (var n = 1; n <= maxFails; ++n)
            {
                var procOnN = Math.Min(1m, n * chance) * (1m - procByN);
                procByN += procOnN;
                sumNpProcOnN += n * procOnN;
            }

            return 1m / sumNpProcOnN;
        }
    }
}