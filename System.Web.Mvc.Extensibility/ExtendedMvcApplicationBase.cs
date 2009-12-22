#region Copyright
/// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
/// This source is subject to the Microsoft Public License. 
/// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
/// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility
{
    using Diagnostics;
    using Linq;
    using Web;

    using Microsoft.Practices.ServiceLocation;

    public abstract class ExtendedMvcApplicationBase : HttpApplication
    {
        private IBootstrapper bootstrapper;

        public IBootstrapper Bootstrapper
        {
            [DebuggerStepThrough]
            get
            {
                return bootstrapper ?? (bootstrapper = CreateBootstrapper());
            }
        }

        public override void Init()
        {
            base.Init();

            BeginRequest += HandleBeginRequest;
            EndRequest += HandleEndRequest;
        }

        public void Application_Start()
        {
            Bootstrapper.Execute();
            OnStart();
        }

        public void Application_End()
        {
            OnEnd();
            Bootstrapper.Dispose();
        }

        protected abstract IBootstrapper CreateBootstrapper();

        protected virtual void OnStart()
        {
        }

        protected virtual void OnBeginRequest()
        {
        }

        protected virtual void OnEndRequest()
        {
        }

        protected virtual void OnEnd()
        {
        }

        private void HandleBeginRequest(object sender, EventArgs e)
        {
            HttpContextBase httpContext = new HttpContextWrapper(Context);
            IServiceLocator serviceLocator = Bootstrapper.ServiceLocator;

            PerRequestExecutionContext perRequestExecutionContext = new PerRequestExecutionContext(httpContext, serviceLocator);

            serviceLocator.GetAllInstances<IPerRequestTask>()
                          .OrderBy(task => task.Order)
                          .Each(task => task.Execute(perRequestExecutionContext));

            OnBeginRequest();
        }

        private void HandleEndRequest(object sender, EventArgs e)
        {
            OnEndRequest();

            Bootstrapper.ServiceLocator
                        .GetAllInstances<IPerRequestTask>()
                        .OrderByDescending(task => task.Order)
                        .Each(task => task.Dispose());
        }
    }
}