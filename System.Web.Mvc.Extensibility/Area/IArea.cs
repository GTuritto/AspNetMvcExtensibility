#region Copyright
// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility
{
    /// <summary>
    /// Represents an interface which acts like a module and supports dynamic loading and unloading.
    /// </summary>
    public interface IArea
    {
        /// <summary>
        /// Gets the name of the area.
        /// </summary>
        /// <value>The name.</value>
        string Name
        {
            get;
        }

        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <value>The description.</value>
        string Description
        {
            get;
        }

        /// <summary>
        /// Gets a value indicating whether this instance is loaded.
        /// </summary>
        /// <value><c>true</c> if this instance is loaded; otherwise, <c>false</c>.</value>
        bool IsLoaded
        {
            get;
        }

        /// <summary>
        /// Loads the area.
        /// </summary>
        /// <param name="context">The context.</param>
        void Load(ExtendedAreaRegistrationContext context);

        /// <summary>
        /// Unloads the area.
        /// </summary>
        /// <param name="context">The context.</param>
        void Unload(ExtendedAreaRegistrationContext context);
    }
}