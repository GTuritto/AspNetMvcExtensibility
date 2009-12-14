#region Copyright
/// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
/// This source is subject to the Microsoft Public License. 
/// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
/// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility
{
    using Linq;

    using Microsoft.Practices.ServiceLocation;

    public class RegisterModelBinders : BootstrapperTaskBase
    {
        public RegisterModelBinders(ModelBinderDictionary binders)
        {
            Invariant.IsNotNull(binders, "binders");

            Binders = binders;
        }

        protected ModelBinderDictionary Binders
        {
            get;
            private set;
        }

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