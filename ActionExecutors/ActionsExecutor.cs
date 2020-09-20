using System;

namespace Roman.Ambinder.Executors.ActionExecutors
{
    public class ActionsExecutor : BaseConsumerOf<Action>
    {
        public ActionsExecutor(uint numberOfConsumers) : base(numberOfConsumers)
        { }

        protected sealed override void OnHandle(in Action action) => action();
    }
}