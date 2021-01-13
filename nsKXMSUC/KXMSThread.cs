using System.Threading;

namespace nsKXMSUC
{
    class KXMSThread
    {
    }
    public partial class ThreadTimer
    {
        public int gboTickTime;
        public int gboTimes;
        public bool gboTimerEnable;
        public Thread TimerThread;
        public delegate void TimerHandler();
        public ManualResetEvent TdEvent = new ManualResetEvent(false);
        public event TimerHandler TimerTick;

        public ThreadTimer()
        {

            gboTimes = 0;
            gboTimerEnable = true;
        }

        public void TimerTickFunction()
        {
            do
            {
                if (gboTimerEnable == false)
                {
                    TdEvent.WaitOne();
                }
                else
                {
                    System.Threading.Thread.Sleep(gboTickTime);
                    TimerTick();
                }

            } while (true);
        }

        public void ChooseThreads(enThreadState threadState)
        {
            switch (threadState)
            {
                case enThreadState.StartOrResume:
                    if (TimerThread == null)
                    {
                        TimerThread = new System.Threading.Thread(new System.Threading.ThreadStart(this.TimerTickFunction));
                        TimerThread.Start();
                    }
                    else
                    {
                        gboTimerEnable = true;
                        TdEvent.Set();
                    }

                    break;
                case enThreadState.Suspend:
                    if (TimerThread == null)
                    {
                    }
                    else
                    {
                        gboTimerEnable = false;
                        TdEvent.Reset();
                    }

                    break;
                case enThreadState.Stop:
                    if (TimerThread == null)
                    {
                    }
                    else
                    {
                        gboTimerEnable = false;
                        TimerThread.Abort();
                    }
                    break;

            }
        }
    }
    public enum enThreadState
    {
        StartOrResume = 1,
        Suspend = 2,
        Stop = 3,
    }

}
