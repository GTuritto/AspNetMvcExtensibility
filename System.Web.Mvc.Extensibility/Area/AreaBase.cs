#region Copyright
// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility
{
    using Diagnostics;

    /// <summary>
    /// Defines a base class which represents an area and supports dynamic loading and unloading.
    /// </summary>
    public abstract class AreaBase : IArea
    {
        private string name;

        /// <summary>
        /// Initializes a new instance of the <see cref="AreaBase"/> class.
        /// </summary>
        protected AreaBase()
        {
            name = GetType().Name;
        }

        /// <summary>
        /// Gets the name of the area.
        /// </summary>
        /// <value>The name.</value>
        public string Name
        {
            [DebuggerStepThrough]
            get
            {
                return name;
            }

            [DebuggerStepThrough]
            protected set
            {
                Invariant.IsNotNull(value, "value");

                name = value;
            }
        }

        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets a value indicating whether this instance is loaded.
        /// </summary>
        /// <value><c>true</c> if this instance is loaded; otherwise, <c>false</c>.</value>
        public bool IsLoaded
        {
            get;
            private set;
        }

        /// <summary>
        /// Loads the area.
        /// </summary>
        /// <param name="context">The context.</param>
        public void Load(ExtendedAreaRegistrationContext context)
        {
            Invariant.IsNotNull(context, "context");

            if (!IsLoaded)
            {
                LoadCore(context);
                IsLoaded = true;
            }
        }

        /// <summary>
        /// Unloads the area.
        /// </summary>
        /// <param name="context">The context.</param>
        public void Unload(ExtendedAreaRegistrationContext context)
        {
            Invariant.IsNotNull(context, "context");

            if (IsLoaded)
            {
                UnloadCore(context);
                IsLoaded = false;
            }
        }

        /// <summary>
        /// Loads the area.
        /// </summary>
        /// <param name="context">The context.</param>
        public abstract void LoadCore(ExtendedAreaRegistrationContext context);

        /// <summary>
        /// Unloads the area.
        /// </summary>
        /// <param name="context">The context.</param>
        public abstract void UnloadCore(ExtendedAreaRegistrationContext context);
    }
}