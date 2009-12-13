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