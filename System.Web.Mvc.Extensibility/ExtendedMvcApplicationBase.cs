#region Copyright
/// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
/// This source is subject to the Microsoft Public License. 
/// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
/// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility
{
    using Diagnostics;
    using Web;

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

        protected virtual void OnEnd()
        {
        }
    }
}