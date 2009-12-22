namespace Demo.Web.Ninject
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class ProductEditModel
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name cannot be blank.")]
        [StringLength(64, ErrorMessage = "Name cannot be more than 64 characters.")]
        public string Name { get; set; }

        [DisplayName("Category")]
        [Required(ErrorMessage = "Category must be selected.")]
        public Category Category { get; set; }

        [DisplayName("Supplier")]
        [Required(ErrorMessage = "Supplier must be selected.")]
        public Supplier Supplier { get; set; }

        [Required(ErrorMessage = "Price cannot be blank.")]
        [Range(10, 1000, ErrorMessage = "Price must be between 10.00-1000.00.")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [ScaffoldColumn(false)]
        public SelectList Categories { get; set; }

        [ScaffoldColumn(false)]
        public SelectList Suppliers { get; set; }
    }
}