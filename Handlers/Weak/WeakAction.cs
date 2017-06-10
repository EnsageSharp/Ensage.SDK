// <copyright file="WeakAction.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Handlers.Weak
{
    using System;
    using System.Reflection;

    /// <summary>
    /// Stores an Action without causing a hard reference
    /// to be created to the Action's owner. The owner can be garbage collected at any time.
    /// </summary>
    /// <typeparam name="T">The type of the Action's parameter.</typeparam>
    public class WeakAction<T> : WeakAction, IExecuteWithObject
    {
        private Action<T> staticAction;

        /// <summary>
        /// Initializes a new instance of the WeakAction class.
        /// </summary>
        /// <param name="action">The action that will be associated to this instance.</param>
        public WeakAction(Action<T> action)
            : this(action == null ? null : action.Target, action)
        {
        }

        /// <summary>
        /// Initializes a new instance of the WeakAction class.
        /// </summary>
        /// <param name="target">The action's owner.</param>
        /// <param name="action">The action that will be associated to this instance.</param>
        public WeakAction(object target, Action<T> action)
        {
            if (action.Method.IsStatic)
            {
                this.staticAction = action;

                if (target != null)
                {
                    // Keep a reference to the target to control the
                    // WeakAction's lifetime.
                    this.Reference = new WeakReference(target);
                }

                return;
            }

            this.Method = action.Method;
            this.ActionReference = new WeakReference(action.Target);
            this.Reference = new WeakReference(target);
        }

        /// <summary>
        /// Gets a value indicating whether the Action's owner is still alive, or if it was collected
        /// by the Garbage Collector already.
        /// </summary>
        public override bool IsAlive
        {
            get
            {
                if (this.staticAction == null && this.Reference == null)
                {
                    return false;
                }

                if (this.staticAction != null)
                {
                    if (this.Reference != null)
                    {
                        return this.Reference.IsAlive;
                    }

                    return true;
                }

                return this.Reference.IsAlive;
            }
        }

        /// <summary>
        /// Gets the name of the method that this WeakAction represents.
        /// </summary>
        public override string MethodName
        {
            get
            {
                if (this.staticAction != null)
                {
                    return this.staticAction.Method.Name;
                }

                return this.Method.Name;
            }
        }

        /// <summary>
        /// Executes the action. This only happens if the action's owner
        /// is still alive. The action's parameter is set to default(T).
        /// </summary>
        public new void Execute()
        {
            this.Execute(default(T));
        }

        /// <summary>
        /// Executes the action. This only happens if the action's owner
        /// is still alive.
        /// </summary>
        /// <param name="parameter">A parameter to be passed to the action.</param>
        public void Execute(T parameter)
        {
            if (this.staticAction != null)
            {
                this.staticAction(parameter);
                return;
            }

            var actionTarget = this.ActionTarget;

            if (this.IsAlive)
            {
                if (this.Method != null && this.ActionReference != null && actionTarget != null)
                {
                    this.Method.Invoke(
                        actionTarget,
                        new object[]
                            {
                                parameter
                            });
                }
            }
        }

        /// <summary>
        /// Executes the action with a parameter of type object. This parameter
        /// will be casted to T. This method implements <see cref="IExecuteWithObject.ExecuteWithObject" />
        /// and can be useful if you store multiple WeakAction{T} instances but don't know in advance
        /// what type T represents.
        /// </summary>
        /// <param name="parameter">The parameter that will be passed to the action after
        /// being casted to T.</param>
        public void ExecuteWithObject(object parameter)
        {
            var parameterCasted = (T)parameter;
            this.Execute(parameterCasted);
        }

        /// <summary>
        /// Sets all the actions that this WeakAction contains to null,
        /// which is a signal for containing objects that this WeakAction
        /// should be deleted.
        /// </summary>
        public new void MarkForDeletion()
        {
            this.staticAction = null;
            base.MarkForDeletion();
        }
    }

    /// <summary>
    /// Stores an <see cref="Action" /> without causing a hard reference
    /// to be created to the Action's owner. The owner can be garbage collected at any time.
    /// </summary>
    public class WeakAction
    {
        private Action staticAction;

        /// <summary>
        /// Initializes a new instance of the <see cref="WeakAction" /> class.
        /// </summary>
        /// <param name="action">The action that will be associated to this instance.</param>
        public WeakAction(Action action)
            : this(action == null ? null : action.Target, action)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WeakAction" /> class.
        /// </summary>
        /// <param name="target">The action's owner.</param>
        /// <param name="action">The action that will be associated to this instance.</param>
        public WeakAction(object target, Action action)
        {
            if (action.Method.IsStatic)
            {
                this.staticAction = action;

                if (target != null)
                {
                    // Keep a reference to the target to control the
                    // WeakAction's lifetime.
                    this.Reference = new WeakReference(target);
                }

                return;
            }

            this.Method = action.Method;
            this.ActionReference = new WeakReference(action.Target);

            this.Reference = new WeakReference(target);
        }

        /// <summary>
        /// Initializes an empty instance of the <see cref="WeakAction" /> class.
        /// </summary>
        protected WeakAction()
        {
        }

        /// <summary>
        /// Gets a value indicating whether the Action's owner is still alive, or if it was collected
        /// by the Garbage Collector already.
        /// </summary>
        public virtual bool IsAlive
        {
            get
            {
                if (this.staticAction == null && this.Reference == null)
                {
                    return false;
                }

                if (this.staticAction != null)
                {
                    if (this.Reference != null)
                    {
                        return this.Reference.IsAlive;
                    }

                    return true;
                }

                return this.Reference.IsAlive;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the WeakAction is static or not.
        /// </summary>
        public bool IsStatic
        {
            get
            {
                return this.staticAction != null;
            }
        }

        /// <summary>
        /// Gets the name of the method that this WeakAction represents.
        /// </summary>
        public virtual string MethodName
        {
            get
            {
                if (this.staticAction != null)
                {
                    return this.staticAction.Method.Name;
                }

                return this.Method.Name;
            }
        }

        /// <summary>
        /// Gets the Action's owner. This object is stored as a 
        /// <see cref="WeakReference" />.
        /// </summary>
        public object Target
        {
            get
            {
                return this.Reference?.Target;
            }
        }

        /// <summary>
        /// Gets or sets a WeakReference to this WeakAction's action's target.
        /// This is not necessarily the same as
        /// <see cref="Reference" />, for example if the
        /// method is anonymous.
        /// </summary>
        protected WeakReference ActionReference { get; set; }

        /// <summary>
        /// The target of the weak reference.
        /// </summary>
        protected object ActionTarget
        {
            get
            {
                return this.ActionReference?.Target;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="MethodInfo" /> corresponding to this WeakAction's
        /// method passed in the constructor.
        /// </summary>
        protected MethodInfo Method { get; set; }

        /// <summary>
        /// Gets or sets a WeakReference to the target passed when constructing
        /// the WeakAction. This is not necessarily the same as
        /// <see cref="ActionReference" />, for example if the
        /// method is anonymous.
        /// </summary>
        protected WeakReference Reference { get; set; }

        /// <summary>
        /// Executes the action. This only happens if the action's owner
        /// is still alive.
        /// </summary>
        public void Execute()
        {
            if (this.staticAction != null)
            {
                this.staticAction();
                return;
            }

            var actionTarget = this.ActionTarget;

            if (this.IsAlive)
            {
                if (this.Method != null && this.ActionReference != null && actionTarget != null)
                {
                    this.Method.Invoke(actionTarget, null);
                    return;
                }
            }
        }

        /// <summary>
        /// Sets the reference that this instance stores to null.
        /// </summary>
        public void MarkForDeletion()
        {
            this.Reference = null;
            this.ActionReference = null;
            this.Method = null;
            this.staticAction = null;
        }
    }
}