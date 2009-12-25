namespace Demo.Web
{
    using System.Web.Mvc.Extensibility;

    public class ProductDisplayModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string CategoryName { get; set; }

        public string SupplierName { get; set; }

        public decimal Price { get; set; }
    }

    public class ProductDisplayModelConfiguration : ModelMetadataConfigurationBase<ProductDisplayModel>
    {
        public ProductDisplayModelConfiguration()
        {
            Configure(model => model.Id).Hide();
            Configure(model => model.CategoryName).DisplayName("Category");
            Configure(model => model.SupplierName).DisplayName("Supplier");
            Configure(model => model.Price).AsCurrency();
        }
    }
}