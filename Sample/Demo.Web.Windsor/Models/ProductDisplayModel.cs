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

        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal Price { get; set; }
    }
}