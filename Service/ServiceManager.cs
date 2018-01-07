// <copyright file="ServiceManager.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Service
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    

    using PlaySharp.Toolkit.Helper.Annotations;
    using NLog;

    public abstract class ServiceManager<TService> : ControllableService, IServiceManager<TService>
        where TService : class
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        private TService active;

        protected ServiceManager(bool activateOnCreation = false)
            : base(activateOnCreation)
        {
        }

        public TService Active
        {
            get
            {
                if (this.active == null)
                {
                    try
                    {
                        this.active = this.GetSelection();
                        this.active?.TryActivate();
                    }
                    catch (Exception e)
                    {
                        Log.Warn(e);
                    }
                }

                return this.active;
            }

            set
            {
                if (EqualityComparer<TService>.Default.Equals(this.active, value))
                {
                    return;
                }

                if (this.active != null)
                {
                    try
                    {
                        this.active.TryDeactivate();
                        this.active = null;
                    }
                    catch (Exception e)
                    {
                        Log.Warn(e);
                    }
                }

                if (value == null)
                {
                    return;
                }

                try
                {
                    this.active = value;
                    this.active.TryActivate();
                }
                catch (Exception e)
                {
                    Log.Warn(e);
                }
            }
        }

        public abstract IEnumerable<Lazy<TService>> Services { get; protected set; }

        protected abstract TService GetSelection();
    }

    public abstract class ServiceManager<TService, TServiceMetadata> : ControllableService, IServiceManager<TService, TServiceMetadata>
        where TService : class
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        private TService active;

        protected ServiceManager(bool activateOnCreation = false)
            : base(activateOnCreation)
        {
        }

        public TService Active
        {
            get
            {
                if (this.active == null)
                {
                    try
                    {
                        this.active = this.GetSelection();
                        this.active?.TryActivate();
                    }
                    catch (Exception e)
                    {
                        Log.Warn(e);
                    }
                }

                return this.active;
            }

            set
            {
                if (EqualityComparer<TService>.Default.Equals(this.active, value))
                {
                    return;
                }

                if (this.active != null)
                {
                    try
                    {
                        this.active.TryDeactivate();
                        this.active = null;
                    }
                    catch (Exception e)
                    {
                        Log.Warn(e);
                    }
                }

                if (value == null)
                {
                    return;
                }

                try
                {
                    this.active = value;
                    this.active?.TryActivate();
                }
                catch (Exception e)
                {
                    Log.Warn(e);
                }
            }
        }

        public abstract IEnumerable<Lazy<TService, TServiceMetadata>> Services { get; protected set; }

        [CanBeNull]
        protected abstract TService GetSelection();
    }
}