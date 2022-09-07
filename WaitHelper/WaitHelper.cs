using System;
using System.ComponentModel;

namespace WaitHelper
{
    public static class WaitHelper
    {

        public static void Wait(Action action, int sleepTime)
        {
            BackgroundWorker backgroundWorker = new BackgroundWorker() { WorkerSupportsCancellation = true, WorkerReportsProgress = true };

            backgroundWorker.DoWork += BackgroundWorker_DoWork;
            backgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;
            void BackgroundWorker_DoWork(object DWsender, DoWorkEventArgs DWe)
            {
                var DWWorker = DWsender as BackgroundWorker;
                (int Sleep, Action action) DWWorkerArgs = ((int Sleep, Action action))DWe.Argument;

                System.Threading.Thread.Sleep(DWWorkerArgs.Sleep);

                DWe.Result = DWWorkerArgs.action;
            }
            void BackgroundWorker_RunWorkerCompleted(object WCsender, RunWorkerCompletedEventArgs WCe)
            {
                var ActionToContinue = WCe.Result as Action;
                ActionToContinue.Invoke();

            }
            var WorkerArgs = (sleepTime, action);
            backgroundWorker.RunWorkerAsync(WorkerArgs);


        }

    }
}
