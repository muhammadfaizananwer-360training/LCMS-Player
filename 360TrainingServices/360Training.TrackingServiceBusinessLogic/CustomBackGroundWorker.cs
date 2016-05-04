using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace _360Training.TrackingServiceBusinessLogic
{
    public delegate void delegateMethod(object sender, DoWorkEventArgs e);

    /// <summary>
    /// Custom Backgroupworker Class
    /// This class will call any method under the dedicated worker and 
    /// show progress while the work is being done.
    /// </summary>

    public class CustomBackGroundWorker : IDisposable
    {

        #region Event
        public delegate void OnCompletedDelegate(RunWorkerCompletedEventArgs e);
        public event OnCompletedDelegate OnCompleted;
        #endregion


        #region Variables
        BackgroundWorker bgw;
        #endregion

        #region Constructor
        public CustomBackGroundWorker()
        {
            Initialize();
        }
        #endregion

        #region Properties

        private delegateMethod aMethod;

        public delegateMethod AMethod
        {
            set { aMethod = value; }
        }

        #endregion

        #region Backgroudworder events & private methods

        private void Initialize()
        {
            bgw = new BackgroundWorker();
            bgw.WorkerSupportsCancellation = true;
            bgw.DoWork += new DoWorkEventHandler(bgw_DoWork);
            bgw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgw_RunWorkerCompleted);
        }

        void progress_OnCancelEvent()
        {
            bgw.CancelAsync();
        }

        public void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {

            }
            else if (e.Error != null)
            {

            }
            else
            {
                if (e.Result is string)
                {

                }
                //progress.ProgressTitle = e.Result.ToString();

                //System.Threading.Thread.Sleep(1000);

            }

            if (OnCompleted != null)
                OnCompleted(e);
            //Application.UseWaitCursor = false;
        }



        void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            aMethod.Invoke(sender, e);
        }

        #endregion

        #region Public methods

        public void StartWorker()
        {
            StartWorker(null);
        }
        public void StartWorker(object argument)
        {
            bgw.RunWorkerAsync(argument);
        }

        public void ReportProgress(int percentProgress, object userState)
        {
            bgw.ReportProgress(percentProgress, userState);
        }

        #endregion


        #region IDisposable Members

        public void Dispose()
        {
            bgw = null;
            GC.SuppressFinalize(this);
        }

        #endregion
    }

}
