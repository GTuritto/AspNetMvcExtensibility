namespace System.Web.Mvc.Extensibility
{
    using Diagnostics;

    public abstract class DisposableBase : IDisposable
    {
        private bool isDisposed;

        [DebuggerStepThrough]
        ~DisposableBase()
        {
            Dispose(false);
        }

        [DebuggerStepThrough]
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        [DebuggerStepThrough]
        protected virtual void DisposeCore()
        {
        }

        [DebuggerStepThrough]
        private void Dispose(bool disposing)
        {
            if (!isDisposed && disposing)
            {
                DisposeCore();
            }

            isDisposed = true;
        }
    }
}