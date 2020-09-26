using System;
using Roman.Ambinder.Executors.Delegates;

namespace Roman.Ambinder.Executors.CallbackConsumers
{
    public sealed class CallbackSingleConsumerOf<T> : CallbackConsumerOf<T>
    {
        public CallbackSingleConsumerOf(ActionInParameterOf<T> onHandle, bool start)
            : base(numberOfConsumers: 1, onHandle, start)
        { }
        public CallbackSingleConsumerOf(Action<T> onHandle, bool start)
            : base(numberOfConsumers: 1, onHandle, start)
        { }
    }
}