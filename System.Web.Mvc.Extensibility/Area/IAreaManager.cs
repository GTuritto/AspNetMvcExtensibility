#region Copyright
// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility
{
    using Collections.Generic;

    /// <summary>
    /// Represents an interface which is used to manage Area.
    /// </summary>
    public interface IAreaManager : IFluentSyntax
    {
        /// <summary>
        /// Gets the available areas.
        /// </summary>
        /// <value>The available areas.</value>
        IEnumerable<IArea> AvailableAreas
        {
            get;
        }

        /// <summary>
        /// Gets the area.
        /// </summary>
        /// <param name="areaName">Name of the area.</param>
        /// <returns></returns>
        IArea GetArea(string areaName);

        /// <summary>
        /// Loads the area.
        /// </summary>
        /// <param name="areaNames">The area names.</param>
        /// <returns></returns>
        IAreaManager LoadArea(string[] areaNames);

        /// <summary>
        /// Unloads the area.
        /// </summary>
        /// <param name="areaNames">The area names.</param>
        /// <returns></returns>
        IAreaManager UnloadArea(string[] areaNames);
    }
}