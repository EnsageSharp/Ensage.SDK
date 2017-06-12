// <copyright file="AbilityNotFoundException.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class AbilityNotFoundException : Exception
    {
        public AbilityNotFoundException()
        {
        }

        public AbilityNotFoundException(string message)
            : base(message)
        {
        }

        public AbilityNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected AbilityNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}