namespace Demo.Web.Unity
{
    using System;
    using System.Web.Mvc;

    using Microsoft.Practices.Unity;

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class PopulateCategoriesAttribute : PopulateModelAttribute
    {
        [Dependency]
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