namespace System.Web.Mvc.Extensibility.Windsor
{
    public class WindsorMvcApplication : ExtendedMvcApplicationBase
    {
        protected override IBootstrapper CreateBootstrapper()
        {
            return new WindsorBootstrapper();
        }
    }
}