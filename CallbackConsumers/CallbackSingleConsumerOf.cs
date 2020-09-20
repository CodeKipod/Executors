using System;

namespace Roman.Ambinder.Executors.CallbackConsumers
{
    public sealed class CallbackSingleConsumerOf<T> : CallbackConsumerOf<T>
    {
        public CallbackSingleConsumerOf(Action<T> onHandle, bool start)
            : base(numberOfConsumers: 1, onHandle, start)
        { }
    }
}