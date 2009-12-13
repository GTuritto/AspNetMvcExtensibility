namespace System.Web.Mvc.Extensibility
{
    using Routing;

    public abstract class RegisterRoutesBase : BootstrapperTaskBase
    {
        protected RegisterRoutesBase(RouteCollection routes)
        {
            Invariant.IsNotNull(routes, "routes");

            Routes = routes;
        }

        protected RouteCollection Routes
        {
            get;
            private set;
        }
    }
}