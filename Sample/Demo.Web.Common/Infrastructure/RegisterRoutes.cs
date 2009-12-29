namespace Demo.Web
{
    using System.Web.Mvc;
    using System.Web.Mvc.Extensibility;
    using System.Web.Routing;

    using Microsoft.Practices.ServiceLocation;

    public class RegisterRoutes : RegisterRoutesBase
    {
        public RegisterRoutes(RouteCollection routes) : base(routes)
        {
        }

        protected override void ExecuteCore(IServiceLocator locator)
        {
            Routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            Routes.MapRoute("Default", "{controller}/{action}/{id}", new { controller = "Home", action = "Index", id = string.Empty });
        }
    }
}