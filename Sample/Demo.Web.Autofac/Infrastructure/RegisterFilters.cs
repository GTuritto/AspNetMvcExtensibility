namespace Demo.Web.Autofac
{
    using System.Web.Mvc.Extensibility;

    using Microsoft.Practices.ServiceLocation;

    public class RegisterFilters : RegisterFiltersBase
    {
        public RegisterFilters(IFilterRegistry filterRegistry) : base(filterRegistry)
        {
        }

        protected override void ExecuteCore(IServiceLocator serviceLocator)
        {
            FilterRegistry.Register<ProductController, PopulateCategoriesAttribute, PopulateSuppliersAttribute>(c => c.Create())
                          .Register<ProductController, PopulateCategoriesAttribute, PopulateSuppliersAttribute>(c => c.Create(null))
                          .Register<ProductController, PopulateCategoriesAttribute, PopulateSuppliersAttribute>(c => c.Edit(0))
                          .Register<ProductController, PopulateCategoriesAttribute, PopulateSuppliersAttribute>(c => c.Edit(null));
        }
    }
}