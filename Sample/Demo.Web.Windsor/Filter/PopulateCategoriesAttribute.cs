namespace Demo.Web.Windsor
{
    using System;
    using System.Web.Mvc;

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class PopulateCategoriesAttribute : PopulateModelAttribute
    {
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