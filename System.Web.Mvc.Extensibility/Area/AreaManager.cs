#region Copyright
// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility
{
    using Collections.Generic;
    using Diagnostics;
    using Linq;

    using Microsoft.Practices.ServiceLocation;

    /// <summary>
    /// Desines a class which is used to manage area.
    /// </summary>
    public class AreaManager : IAreaManager
    {
        private readonly IDictionary<string, IArea> areas;

        /// <summary>
        /// Initializes a new instance of the <see cref="AreaManager"/> class.
        /// </summary>
        /// <param name="areas">The areas.</param>
        /// <param name="serviceLocator">The service locator.</param>
        public AreaManager(IEnumerable<IArea> areas, IServiceLocator serviceLocator)
        {
            Invariant.IsNotNull(areas, "areas");
            Invariant.IsNotNull(serviceLocator, "routes");

            this.areas = areas.ToDictionary(area => area.Name, area => area, StringComparer.OrdinalIgnoreCase);
            ServiceLocator = serviceLocator;
        }

        /// <summary>
        /// Gets the available areas.
        /// </summary>
        /// <value>The available areas.</value>
        public IEnumerable<IArea> AvailableAreas
        {
            [DebuggerStepThrough]
            get
            {
                return areas.Values;
            }
        }

        /// <summary>
        /// Gets the service locator.
        /// </summary>
        /// <value>The service locator.</value>
        protected IServiceLocator ServiceLocator
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the area.
        /// </summary>
        /// <param name="areaName">Name of the area.</param>
        /// <returns></returns>
        public IArea GetArea(string areaName)
        {
            IArea area;

            return areas.TryGetValue(areaName, out area) ? area : null;
        }

        /// <summary>
        /// Loads the area.
        /// </summary>
        /// <param name="areaNames">The area names. if no name is specified all the available areas will be loaded</param>
        /// <returns></returns>
        public virtual IAreaManager LoadArea(string[] areaNames)
        {
            return PerformAction(areaNames, true);
        }

        /// <summary>
        /// Unloads the area.
        /// </summary>
        /// <param name="areaNames">The area names. if no name is specified all the available areas will be loaded</param>
        /// <returns></returns>
        public virtual IAreaManager UnloadArea(string[] areaNames)
        {
            return PerformAction(areaNames, false);
        }

        /// <summary>
        /// Creates area registration context for the given area.
        /// </summary>
        /// <param name="area">The area.</param>
        /// <returns></returns>
        protected ExtendedAreaRegistrationContext CreateAreaRegistrationContext(IArea area)
        {
            ExtendedAreaRegistrationContext context = new ExtendedAreaRegistrationContext(area.Name, ServiceLocator);

            string areaNamespace = area.GetType().Namespace;

            if (areaNamespace != null)
            {
                context.Namespaces.Add(areaNamespace);
            }

            return context;
        }

        private IAreaManager PerformAction(IEnumerable<string> areaNames, bool load)
        {
            if (areaNames.Any())
            {
                foreach (string areaName in areaNames)
                {
                    IArea area = areas[areaName];

                    if (load)
                    {
                        area.Load(CreateAreaRegistrationContext(area));
                    }
                    else
                    {
                        area.Unload(CreateAreaRegistrationContext(area));
                    }
                }
            }
            else
            {
                foreach (IArea area in areas.Values)
                {
                    if (load)
                    {
                        area.Load(CreateAreaRegistrationContext(area));
                    }
                    else
                    {
                        area.Unload(CreateAreaRegistrationContext(area));
                    }
                }
            }

            return this;
        }
    }
}