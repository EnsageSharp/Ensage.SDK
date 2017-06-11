// <copyright file="AbilityNotAnItemException.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>
namespace Ensage.SDK.Abilities
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class AbilityNotAnItemException : Exception
    {
        public AbilityNotAnItemException()
        {
        }

        public AbilityNotAnItemException(string message)
            : base(message)
        {
        }

        public AbilityNotAnItemException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected AbilityNotAnItemException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }
    }
}