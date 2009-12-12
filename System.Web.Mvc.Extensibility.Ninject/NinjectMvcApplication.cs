namespace System.Web.Mvc.Extensibility.Ninject
{
    public class NinjectMvcApplication : ExtendedMvcApplicationBase
    {
        protected override IBootstrapper CreateBootstrapper()
        {
            return new NinjectBootstrapper();
        }
    }
}