namespace Demo.Web
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    //using System.Web.Mvc.Extensibility;

    public class ProductDisplayModel
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        public string Name { get; set; }

        [DisplayName("Category")]
        public string CategoryName { get; set; }

        [DisplayName("Supplier")]
        public string SupplierName { get; set; }

        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
    }

    /*
    public class ProductDisplayModelConfiguration : ModelMetadataConfigurationBase<ProductDisplayModel>
    {
        public ProductDisplayModelConfiguration()
        {
            Configure(model => model.Id).Hide();
            Configure(model => model.CategoryName).DisplayName("Category");
            Configure(model => model.SupplierName).DisplayName("Supplier");
            Configure(model => model.Price).FormatAsCurrency();
        }
    }
    */
}