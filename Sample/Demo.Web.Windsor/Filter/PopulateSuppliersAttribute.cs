namespace Demo.Web.Windsor
{
    using System;
    using System.Web.Mvc;

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class PopulateSuppliersAttribute : PopulateModelAttribute
    {
        private readonly IRepository<Supplier> repository;

        public PopulateSuppliersAttribute(IRepository<Supplier> repository)
        {
            this.repository = repository;
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            ProductEditModel editModel = filterContext.Controller.ViewData.Model as ProductEditModel;

            if (editModel != null)
            {
                editModel.Suppliers = new SelectList(repository.All(), "Id", "CompanyName", editModel.Supplier);
            }
        }
    }
}