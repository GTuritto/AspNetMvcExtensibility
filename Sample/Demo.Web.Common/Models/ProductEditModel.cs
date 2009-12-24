namespace Demo.Web
{
    using System.Web.Mvc;

    public class ProductEditModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Category Category { get; set; }

        public Supplier Supplier { get; set; }

        public decimal Price { get; set; }

        public SelectList Categories { get; set; }

        public SelectList Suppliers { get; set; }
    }
}