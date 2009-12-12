namespace Demo.Web.Unity
{
    using System;
    using System.Web.Mvc;

    using Microsoft.Practices.Unity;

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class PopulateSuppliersAttribute : PopulateModelAttribute
    {
        [Dependency]
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