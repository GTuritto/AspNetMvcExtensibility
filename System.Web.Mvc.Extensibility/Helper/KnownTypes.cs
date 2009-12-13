namespace System.Web.Mvc.Extensibility
{
    public static class KnownTypes
    {
        public static readonly Type BindingAttributeType = typeof(BindingTypesAttribute);
        public static readonly Type BootstrapperTaskType = typeof(IBootstrapperTask);
        public static readonly Type ModelBinderType = typeof(IModelBinder);
        public static readonly Type ControllerType = typeof(Controller);
        public static readonly Type FilterAttributeType = typeof(FilterAttribute);
        public static readonly Type ViewEngineType = typeof(IViewEngine);
    }
}