namespace System.Web.Mvc.Extensibility
{
    using System;
    using Mvc;
    #if (!MVC1)
    using Routing;
    #endif

    using Microsoft.Practices.ServiceLocation;

    public class ExtendedControllerFactory : DefaultControllerFactory
    {
        public ExtendedControllerFactory(IServiceLocator locator)
        {
            Invariant.IsNotNull(locator, "locator");

            ServiceLocator = locator;
        }

        protected IServiceLocator ServiceLocator
        {
            get;
            private set;
        }

        #if (MVC1)

        protected override IController GetControllerInstance(Type controllerType)
        {
            return CreateController(controllerType) ?? base.GetControllerInstance(controllerType);
        }

        #else

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return CreateController(controllerType) ?? base.GetControllerInstance(requestContext, controllerType);
        }

        #endif

        private Controller CreateController(Type controllerType)
        {
            Controller controller = null;

            if (controllerType != null)
            {
                controller = ServiceLocator.GetInstance(controllerType) as Controller;

                if (controller != null)
                {
                    controller.ActionInvoker = new ExtendedControllerActionInvoker(ServiceLocator);
                }
            }

            return controller;
        }
    }
}