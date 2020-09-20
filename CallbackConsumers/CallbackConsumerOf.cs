using System;

namespace Roman.Ambinder.Executors.CallbackConsumers
{
    public class CallbackConsumerOf<T> : BaseConsumerOf<T>
    {
        private readonly Action<T> _onHandle;

        public CallbackConsumerOf(uint numberOfConsumers, Action<T> onHandle, bool start)
            : base(numberOfConsumers)
        {
            _onHandle = onHandle;
            if (start)
                TryStart();
        }

        protected sealed override void OnHandle(in T item) => _onHandle(item);
    }
}