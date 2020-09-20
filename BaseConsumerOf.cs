using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using Roman.Ambinder.DataTypes.OperationResults;
using Roman.Ambinder.LifeCycleComponents.StartStopDisposable;

namespace Roman.Ambinder.Executors
{
    public abstract class BaseConsumerOf<T> : BaseStartStopDisposableComponent
    {
        private CancellationTokenSource _cts;
        private readonly BlockingCollection<T> _pending = new BlockingCollection<T>();
        private readonly Task[] _consumerTasks;

        protected BaseConsumerOf(uint numberOfConsumers)
        {
            _consumerTasks = new Task[numberOfConsumers];
        }

        protected sealed override OperationResult OnTryStart(string[] args)
        {
            _cts = new CancellationTokenSource();

            for (var i = 0; i < _consumerTasks.Length; i++)
            {
                _consumerTasks[i] = Task.Factory.StartNew(OnConsume, TaskCreationOptions.LongRunning);
            }

            return OperationResult.Successful;
        }

        protected sealed override void OnStop()
        {
            _cts.Cancel();
            Task.WaitAll(_consumerTasks);
            _cts.Dispose();
        }

        protected sealed override void OnAfterDispose(bool isDisposing) => _pending.Dispose();

        public void QueueForProcessing(T item) => _pending.TryAdd(item);

        private void OnConsume()
        {
            try
            {
                var cancellationToken = _cts.Token;
                while (true)
                {
                    var item = _pending.Take(cancellationToken);
                    OnHandle(item);
                }
            }
            catch
            {
                // ignored
            }
        }

        protected abstract void OnHandle(in T item);
    }
}
