namespace System.Web.Mvc.Extensibility
{
    using Microsoft.Practices.ServiceLocation;

    public class RegisterViewEngines : BootstrapperTaskBase
    {
        public RegisterViewEngines(ViewEngineCollection viewEngines)
        {
            Invariant.IsNotNull(viewEngines, "viewEngines");

            ViewEngines = viewEngines;
        }

        protected ViewEngineCollection ViewEngines
        {
            get;
            private set;
        }

        protected override void ExecuteCore(IServiceLocator locator)
        {
            locator.GetAllInstances<IViewEngine>()
                   .Each(engine =>
                                  {
                                      if (!ViewEngines.Contains(engine))
                                      {
                                          ViewEngines.Add(engine);
                                      }
                                  });
        }
    }
}