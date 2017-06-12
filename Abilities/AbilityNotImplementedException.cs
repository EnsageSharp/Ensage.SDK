// <copyright file="AbilityNotImplementedException.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class AbilityNotImplementedException : Exception
    {
        public AbilityNotImplementedException()
        {
        }

        public AbilityNotImplementedException(string message)
            : base(message)
        {
        }

        public AbilityNotImplementedException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected AbilityNotImplementedException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }
    }
}