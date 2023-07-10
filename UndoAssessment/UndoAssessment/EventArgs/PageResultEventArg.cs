using System;

namespace UndoAssessment.EventHandlers
{
    public class PageResultEventArg : EventArgs
    {
        public object Result;

        public PageResultEventArg(object result)
        {
            this.Result = result;
        }
    }
}
