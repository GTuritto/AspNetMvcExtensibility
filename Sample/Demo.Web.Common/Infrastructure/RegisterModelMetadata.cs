namespace Demo.Web
{
    using System.Web.Mvc;
    using System.Web.Mvc.Extensibility;

    using Microsoft.Practices.ServiceLocation;

    public class RegisterModelMetadata : BootstrapperTaskBase
    {
        protected override void ExecuteCore(IServiceLocator serviceLocator)
        {
            IModelMetadataRegistry registry = serviceLocator.GetInstance<IModelMetadataRegistry>();

            registry.Register<ProductDisplayModel>(configurator =>
                                                   {
                                                       configurator.Configure(model => model.Id).Hide();
                                                       configurator.Configure(model => model.Price).AsCurrency();
                                                   })
                    .Register<ProductEditModel>(configurator =>
                                                   {
                                                       configurator.Configure(model => model.Id).Hide();
                                                       configurator.Configure(model => model.Name).Required("Name cannot be blank.").MaximumLength(64, "Name cannot be more than 64 characters.");
                                                       configurator.Configure(model => model.Category).DisplayName("Category").Required("Category must be selected.");
                                                       configurator.Configure(model => model.Supplier).DisplayName("Supplier").Required("Supplier must be selected.");
                                                       configurator.Configure(model => model.Price).AsCurrency().Required("Price cannot be blank.").Range(10, 1000, "Price must be between 10.00-1000.00.");
                                                       configurator.Configure(model => model.Categories).Hide();
                                                       configurator.Configure(model => model.Suppliers).Hide();
                                                   });

            ModelMetadataProviders.Current = new ExtendedModelMetadataProvider(registry);
        }
    }
}