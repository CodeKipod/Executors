using System;

namespace Roman.Ambinder.Executors.ActionExecutors
{
    public class TryCatchActionsExecutor : BaseConsumerOf<Action>
    {
        public TryCatchActionsExecutor(uint numberOfConsumers)
            : base(numberOfConsumers)
        { }

        protected sealed override void OnHandle(in Action action)
        {
            try
            {
                action();
            }
            catch
            {
                // ignored
            }
        }
    }
}