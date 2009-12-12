namespace Demo.Web.Ninject
{
    using System;
    using System.Web.Mvc;

    using Inject = global::Ninject.InjectAttribute;

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class PopulateCategoriesAttribute : PopulateModelAttribute
    {
        [Inject]
        public IRepository<Category> Repository { get; set; }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            ProductEditModel editModel = filterContext.Controller.ViewData.Model as ProductEditModel;

            if (editModel != null)
            {
                editModel.Categories = new SelectList(Repository.All(), "Id", "Name", editModel.Category);
            }
        }
    }
}