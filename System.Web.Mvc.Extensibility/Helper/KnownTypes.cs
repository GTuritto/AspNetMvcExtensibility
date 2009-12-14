#region Copyright
/// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
/// This source is subject to the Microsoft Public License. 
/// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
/// All other rights reserved.
#endregion

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
        public static readonly Type ActionResultType = typeof(ActionResult);
    }
}