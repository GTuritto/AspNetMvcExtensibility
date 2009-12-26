#region Copyright
// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility
{
    using Linq;

    using Microsoft.Practices.ServiceLocation;

    /// <summary>
    /// Defines a class which is used to register available <seealso cref="IModelBinder"/>s.
    /// </summary>
    public class RegisterModelBinders : BootstrapperTaskBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterModelBinders"/> class.
        /// </summary>
        /// <param name="binders">The binders.</param>
        public RegisterModelBinders(ModelBinderDictionary binders)
        {
            Invariant.IsNotNull(binders, "binders");

            Binders = binders;
        }

        /// <summary>
        /// Gets or sets the binders.
        /// </summary>
        /// <value>The binders.</value>
        protected ModelBinderDictionary Binders
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
            serviceLocator.GetAllInstances<IModelBinder>()
                          .Select(binder => new
                                         {
                                             Binder = binder,
                                             Types = binder.GetType()
                                                           .GetCustomAttributes(KnownTypes.BindingAttributeType, true)
                                                           .OfType<BindingTypesAttribute>()
                                                           .SelectMany(attribute => attribute.Types)
                                         })
                           .Each(pair => pair.Types.Each(type =>
                                                         {
                                                             if (!Binders.ContainsKey(type))
                                                             {
                                                                 Binders.Add(type, pair.Binder);
                                                             }
                                                         }));
        }
    }
}