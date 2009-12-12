namespace Demo.Web.Ninject
{
    public class Product : EntityBase
    {
        public string Name { get; set; }

        public Category Category { get; set; }

        public Supplier Supplier { get; set; }

        public decimal Price { get; set; }
    }
}