#region Copyright
// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility
{
    /// <summary>
    /// Defines an static class which contains all the known <see cref="Type"/>s.
    /// </summary>
    public static class KnownTypes
    {
        /// <summary>
        /// The <seealso cref="BindingTypesAttribute"/> type.
        /// </summary>
        public static readonly Type BindingAttributeType = typeof(BindingTypesAttribute);

        /// <summary>
        /// The <seealso cref="BootstrapperTaskType"/> type.
        /// </summary>
        public static readonly Type BootstrapperTaskType = typeof(IBootstrapperTask);

        /// <summary>
        /// The <seealso cref="PerRequestTaskType"/> type.
        /// </summary>
        public static readonly Type PerRequestTaskType = typeof(IPerRequestTask);

        /// <summary>
        /// The <seealso cref="ModelBinderType"/> type.
        /// </summary>
        public static readonly Type ModelBinderType = typeof(IModelBinder);

        /// <summary>
        /// The <seealso cref="ControllerType"/> type.
        /// </summary>
        public static readonly Type ControllerType = typeof(Controller);

        /// <summary>
        /// The <seealso cref="FilterAttributeType"/> type.
        /// </summary>
        public static readonly Type FilterAttributeType = typeof(FilterAttribute);

        /// <summary>
        /// The <seealso cref="ViewEngineType"/> type.
        /// </summary>
        public static readonly Type ViewEngineType = typeof(IViewEngine);

        /// <summary>
        /// The <seealso cref="ActionResultType"/> type.
        /// </summary>
        public static readonly Type ActionResultType = typeof(ActionResult);

        #if (!MVC1)

        /// <summary>
        /// The <seealso cref="ExtendedModelMetadataProviderBase"/> type.
        /// </summary>
        public static readonly Type ExtendedModelMetadataProviderType = typeof(ExtendedModelMetadataProviderBase);

        /// <summary>
        /// The <seealso cref="ModelMetadataConfigurationType"/> type.
        /// </summary>
        public static readonly Type ModelMetadataConfigurationType = typeof(IModelMetadataConfiguration);

        /// <summary>
        /// The <seealso cref="IArea"/> type.
        /// </summary>
        public static readonly Type AreaType = typeof(IArea);

        #endif
    }
}