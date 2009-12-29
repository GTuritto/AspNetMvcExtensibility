namespace Demo.Web
{
    using System.Web.Mvc.Extensibility;

    public class ProductEditModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Category Category { get; set; }

        public Supplier Supplier { get; set; }

        public decimal Price { get; set; }
    }

    public class ProductEditModelConfiguration : ModelMetadataConfigurationBase<ProductEditModel>
    {
        public ProductEditModelConfiguration()
        {
            Configure(model => model.Id).Hide();

            Configure(model => model.Name).Required("Name cannot be blank.")
                                          .MaximumLength(64, "Name cannot be more than 64 characters.");

            Configure(model => model.Category).DisplayName("Category")
                                              .Required("Category must be selected.")
                                              .AsDropDownList("categories", "[Select category]");

            Configure(model => model.Supplier).DisplayName("Supplier")
                                              .Required("Supplier must be selected.")
                                              .AsDropDownList("suppliers", "[Select supplier]");

            Configure(model => model.Price).FormatAsCurrency()
                                           .Required("Price cannot be blank.")
                                           .Range(10.00m, 1000.00m, "Price must be between 10.00-1000.00.");
        }
    }
}