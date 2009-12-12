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