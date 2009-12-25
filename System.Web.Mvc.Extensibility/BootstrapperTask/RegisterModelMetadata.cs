#region Copyright
/// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
/// This source is subject to the Microsoft Public License. 
/// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
/// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility
{
    using Collections.Generic;
    using Linq;

    using Microsoft.Practices.ServiceLocation;

    public class RegisterModelMetadata : BootstrapperTaskBase
    {
        protected override void ExecuteCore(IServiceLocator serviceLocator)
        {
            IEnumerable<IModelMetadataConfiguration> configurations = serviceLocator.GetAllInstances<IModelMetadataConfiguration>();

            if (configurations.Any())
            {
                IModelMetadataRegistry registry = serviceLocator.GetInstance<IModelMetadataRegistry>();

                configurations.Each(configuration => registry.Register(configuration.ModelType, configuration.Configurations));

                ModelMetadataProviders.Current = new ExtendedModelMetadataProvider(registry);
                ModelValidatorProviders.Providers.Clear();
                ModelValidatorProviders.Providers.Add(new ExtendedModelValidatorProvider());
            }
        }
    }
}