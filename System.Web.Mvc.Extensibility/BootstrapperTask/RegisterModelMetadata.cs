#region Copyright
// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility
{
    using Collections.Generic;

    using Microsoft.Practices.ServiceLocation;

    /// <summary>
    /// Defines a class which is used to register the default <seealso cref="ModelMetadataProvider"/>.
    /// </summary>
    public class RegisterModelMetadata : BootstrapperTaskBase
    {
        /// <summary>
        /// Executes the task.
        /// </summary>
        /// <param name="serviceLocator">The service locator.</param>
        protected override void ExecuteCore(IServiceLocator serviceLocator)
        {
            IEnumerable<IModelMetadataConfiguration> configurations = serviceLocator.GetAllInstances<IModelMetadataConfiguration>();

            IModelMetadataRegistry registry = serviceLocator.GetInstance<IModelMetadataRegistry>();

            configurations.Each(configuration => registry.Register(configuration.ModelType, configuration.Configurations));

            ModelMetadataProviders.Current = serviceLocator.GetInstance<CompositeModelMetadataProvider>();

            // We have to make sure that custom provider will appear first,
            // otherwise it will return wrong validation messages.
            serviceLocator.GetAllInstances<ModelValidatorProvider>()
                          .Each(provider => ModelValidatorProviders.Providers.Insert(0, provider));
        }
    }
}