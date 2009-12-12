namespace System.Web.Mvc.Extensibility.StructureMap
{
    public class StructureMapMvcApplication : ExtendedMvcApplicationBase
    {
        protected override IBootstrapper CreateBootstrapper()
        {
            return new StructureMapBootstrapper();
        }
    }
}