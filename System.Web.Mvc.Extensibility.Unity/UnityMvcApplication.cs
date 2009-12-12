namespace System.Web.Mvc.Extensibility.Unity
{
    public class UnityMvcApplication : ExtendedMvcApplicationBase
    {
        protected override IBootstrapper CreateBootstrapper()
        {
            return new UnityBootstrapper();
        }
    }
}