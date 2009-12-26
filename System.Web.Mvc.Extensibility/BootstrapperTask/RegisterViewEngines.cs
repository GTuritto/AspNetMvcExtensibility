#region Copyright
// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility
{
    using Collections.Generic;
    using Linq;

    using Microsoft.Practices.ServiceLocation;

    /// <summary>
    /// Defines a class which is used to register available <seealso cref="IViewEngine"/>s.
    /// </summary>
    public class RegisterViewEngines : BootstrapperTaskBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterViewEngines"/> class.
        /// </summary>
        /// <param name="viewEngines">The view engines.</param>
        public RegisterViewEngines(ViewEngineCollection viewEngines)
        {
            Invariant.IsNotNull(viewEngines, "viewEngines");

            ViewEngines = viewEngines;
        }

        /// <summary>
        /// Gets or sets the view engines.
        /// </summary>
        /// <value>The view engines.</value>
        protected ViewEngineCollection ViewEngines
        {
            get;
            private set;
        }

        /// <summary>
        /// Executes the task.
        /// </summary>
        /// <param name="serviceLocator">The service locator.</param>
        protected override void ExecuteCore(IServiceLocator serviceLocator)
        {
            IEnumerable<Type> viewEngineTypes = ViewEngines.Select(ve => ve.GetType());

            serviceLocator.GetAllInstances<IViewEngine>()
                          .Each(engine =>
                                  {
                                      if (!viewEngineTypes.Any(type => type == engine.GetType()))
                                      {
                                          ViewEngines.Add(engine);
                                      }
                                  });
        }
    }
}