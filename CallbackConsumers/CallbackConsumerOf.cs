using System;
using Roman.Ambinder.Executors.Delegates;

namespace Roman.Ambinder.Executors.CallbackConsumers
{
    public class CallbackConsumerOf<T> : BaseConsumerOf<T>
    {
        private readonly ActionInParameterOf<T> _onHandleInParameter;
        private readonly Action<T> _onHandle;

        public CallbackConsumerOf(uint numberOfConsumers, Action<T> onHandle, bool start)
            : base(numberOfConsumers)
        {
            _onHandle = onHandle;
            if (start)
                TryStart();
        }

        public CallbackConsumerOf(uint numberOfConsumers, ActionInParameterOf<T> onHandleInParameter, bool start)
            : base(numberOfConsumers)
        {
            _onHandleInParameter = onHandleInParameter;
            if (start)
                TryStart();
        }

        protected sealed override void OnHandle(in T item)
        {
            if (_onHandleInParameter != null)
                _onHandleInParameter(in item);
            else
                _onHandle(item);
        }
    }

}