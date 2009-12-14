#region Copyright
/// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
/// This source is subject to the Microsoft Public License. 
/// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
/// All other rights reserved.
#endregion

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