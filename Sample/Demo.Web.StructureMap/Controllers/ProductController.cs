namespace Demo.Web.StructureMap
{
    using System.Linq;
    using System.Web.Mvc;

    public class ProductController : Controller
    {
        private readonly IRepository<Product> repository;

        public ProductController(IRepository<Product> repository)
        {
            this.repository = repository;
        }

        public ActionResult Index()
        {
            return View(repository.All().Select(product => product.AsDisplayModel()));
        }

        public ActionResult Details(int id)
        {
            return View(repository.Get(id).AsDisplayModel());
        }

        [PopulateCategories, PopulateSuppliers]
        public ActionResult Create()
        {
            return View(new ProductEditModel());
        }

        [HttpPost, PopulateCategories, PopulateSuppliers]
        public ActionResult Create(FormCollection form)
        {
            ProductEditModel model = new ProductEditModel();

            if (TryUpdateModel(model, form.ToValueProvider()))
            {
                Product product = model.AsProduct();
                product.Id = repository.All().LastOrDefault().Id + 1;

                repository.Add(product);

                return RedirectToAction("Index");
            }

            return View(model);
        }

        [PopulateCategories, PopulateSuppliers]
        public ActionResult Edit(int id)
        {
            return View(repository.Get(id).AsEditModel());
        }

        [HttpPost, PopulateCategories, PopulateSuppliers]
        public ActionResult Edit(ProductEditModel model)
        {
            if (ModelState.IsValid)
            {
                Product product = model.AsProduct();

                repository.Update(product);

                return RedirectToAction("Index");
            }

            return View(model);
        }

        public ActionResult Delete(int id)
        {
            return View(repository.Get(id).AsDisplayModel());
        }

        [HttpPost]
        public ActionResult Delete(int id, string confirm)
        {
            repository.Delete(id);

            return RedirectToAction("Index");
        }
    }
}