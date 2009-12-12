namespace System.Web.Mvc.Extensibility.Autofac
{
    public class AutofacMvcApplication : ExtendedMvcApplicationBase
    {
        protected override IBootstrapper CreateBootstrapper()
        {
            return new AutofacBootstrapper();
        }
    }
}