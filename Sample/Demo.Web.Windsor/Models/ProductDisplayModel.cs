namespace Demo.Web.Windsor
{
    using System.ComponentModel.DataAnnotations;

    public class ProductDisplayModel
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        public string Name { get; set; }

        public string CategoryName { get; set; }

        public string SupplierName { get; set; }

        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
    }
}