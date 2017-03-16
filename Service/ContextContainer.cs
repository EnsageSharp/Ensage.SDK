// <copyright file="ContextContainer.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Service
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.ComponentModel.Composition.Hosting;
    using System.Reflection;
    using System.Security;

    using log4net;

    using PlaySharp.Toolkit.Helper.Annotations;
    using PlaySharp.Toolkit.Logging;

    [SecuritySafeCritical]
    public class ContextContainer<TContext> : IEquatable<ContextContainer<TContext>>
        where TContext : class, IServiceContext
    {
        private static readonly ILog Log = AssemblyLogs.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public ContextContainer([NotNull] TContext context, [NotNull] CompositionContainer container)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (container == null)
            {
                throw new ArgumentNullException(nameof(container));
            }

            this.Context = context;
            this.Container = container;
        }

        public CompositionContainer Container { get; }

        public TContext Context { get; }

        public static bool operator ==(ContextContainer<TContext> left, ContextContainer<TContext> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ContextContainer<TContext> left, ContextContainer<TContext> right)
        {
            return !Equals(left, right);
        }

        public void BuildUp([NotNull] object instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            Log.Debug($"BuildUp {instance.GetType().Name}");
            this.Container.SatisfyImportsOnce(instance);
        }

        public bool Equals(ContextContainer<TContext> other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return EqualityComparer<TContext>.Default.Equals(this.Context, other.Context);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            var other = obj as ContextContainer<TContext>;
            return other != null && this.Equals(other);
        }

        public virtual T Get<T>([CanBeNull] string contract = null)
        {
            if (contract == null)
            {
                return this.Container.GetExportedValue<T>();
            }

            return this.Container.GetExportedValue<T>(contract);
        }

        public virtual IEnumerable<T> GetAll<T>([CanBeNull] string contract = null)
        {
            if (contract == null)
            {
                return this.Container.GetExportedValues<T>();
            }

            return this.Container.GetExportedValues<T>(contract);
        }

        public override int GetHashCode()
        {
            return EqualityComparer<TContext>.Default.GetHashCode(this.Context);
        }

        public virtual void RegisterValue<T>([NotNull] T value, [CanBeNull] string contract = null)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            Log.Debug($"ComposeExportedValue {value} {contract}");

            if (contract == null)
            {
                this.Container.ComposeExportedValue(value);
            }
            else
            {
                this.Container.ComposeExportedValue(contract, value);
            }
        }
    }
}