namespace Demo.Web.Ninject
{
    using System;
    using System.Web.Mvc;

    using Inject = global::Ninject.InjectAttribute;

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class PopulateSuppliersAttribute : PopulateModelAttribute
    {
        [Inject]
        public IRepository<Supplier> Repository { get; set; }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            ProductEditModel editModel = filterContext.Controller.ViewData.Model as ProductEditModel;

            if (editModel != null)
            {
                editModel.Suppliers = new SelectList(Repository.All(), "Id", "CompanyName", editModel.Supplier);
            }
        }
    }
}