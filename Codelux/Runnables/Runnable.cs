using System;

namespace Codelux.Runnables
{
    public abstract class Runnable : IRunnable, IDisposable
    {
        private RunnableStatus _status = RunnableStatus.Stopped;
        public bool IsRunning => _status == RunnableStatus.Running;

        protected abstract void OnStart(object context = null);
        protected abstract void OnStop();

        public void Start(object context = null)
        {
            if (_status == RunnableStatus.Running) return;

            try
            {
                _status = RunnableStatus.Running;

                lock (this)
                    OnStart(context);

            }
            catch (Exception)
            {
                Stop();
                throw;
            }
        }

        public void Stop()
        {
            if (_status == RunnableStatus.Stopped) return;

            try
            {
                _status = RunnableStatus.Stopping;

                lock (this)
                    OnStop();
            }
            finally
            {
                _status = RunnableStatus.Stopped;
            }
        }

        protected virtual void Dispose(bool disposing) => Stop();

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
