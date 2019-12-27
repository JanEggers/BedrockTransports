using System;
using System.IO.Pipelines;
using System.Threading;

namespace Bedrock.Framework
{
    public class NonAllocThreadpoolScheduler : PipeScheduler, IThreadPoolWorkItem
    {
        private Action<object> _action;
        private object _state;

        public override void Schedule(Action<object> action, object state)
        {
            _action = action;
            _state = state;
            System.Threading.ThreadPool.UnsafeQueueUserWorkItem(this, false);
        }

        public void Execute()
        {
            _action(_state);
        }
    }
}
