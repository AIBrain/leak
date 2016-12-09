﻿using System.Threading;

namespace Leak.Tasks
{
    public abstract class LeakQueueBase<TContext>
    {
        private readonly Thread worker;
        protected readonly ManualResetEventSlim onReady;

        private TContext data;
        private bool completed;

        protected LeakQueueBase()
        {
            onReady = new ManualResetEventSlim(false);
            worker = new Thread(Process);
            worker.Start();
        }

        public void Start(TContext context)
        {
            data = context;
            onReady.Set();
        }

        public void Stop()
        {
            completed = true;
            onReady.Set();
            worker.Join();
        }

        private void Process()
        {
            while (completed == false)
            {
                if (onReady.Wait(200))
                {
                    onReady.Reset();
                    OnProcess(data);
                }
            }
        }

        protected abstract void OnProcess(TContext context);
    }
}