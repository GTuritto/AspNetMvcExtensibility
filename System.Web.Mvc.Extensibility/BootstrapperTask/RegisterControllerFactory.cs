namespace System.Web.Mvc.Extensibility
{
    using Microsoft.Practices.ServiceLocation;

    public class RegisterControllerFactory : BootstrapperTaskBase
    {
        public RegisterControllerFactory(ControllerBuilder controllerBuilder)
        {
            Invariant.IsNotNull(controllerBuilder, "controllerBuilder");

            ControllerBuilder = controllerBuilder;
        }

        protected ControllerBuilder ControllerBuilder
        {
            get;
            private set;
        }

        protected override void ExecuteCore(IServiceLocator serviceLocator)
        {
            ControllerBuilder.SetControllerFactory(serviceLocator.GetInstance<IControllerFactory>());
        }
    }
}