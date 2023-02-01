using BO;
namespace Simulator
{

    public static class Simulator
    {
        /// <summary>
        /// access bl
        /// </summary>
        private static readonly BlApi.IBl? _bl = BlApi.Factory.Get();
        /// <summary>
        /// random number generator
        /// </summary>
        private static Random _r = new Random(DateTime.Now.Millisecond);

        /// <summary>
        /// flag for escaping the thread
        /// </summary>
        private static volatile bool _isRunning;

        /// <summary>
        /// delegate to update event
        /// </summary>
        /// <param name="OldStatus"></param>
        /// <param name="StartTime"></param>
        /// <param name="NewStatus"></param>
        /// <param name="EndTime"></param>
        /// <param name="ord"></param>
        public delegate void update(BO.Enums.OrderStatus OldStatus, DateTime StartTime,
            BO.Enums.OrderStatus NewStatus, int EndTime, BO.Order ord);

        /// <summary>
        /// update event
        /// </summary>
        public static event update? ScreenUpdate;

        /// <summary>
        /// delegate for stop event
        /// </summary>
        public delegate void Stop();

        /// <summary>
        /// stop event
        /// </summary>
        public static event Stop? StopSim;

        /// <summary>
        /// activates the simulator 
        /// </summary>
        public static void Activate()
        {
            _isRunning = true; // set the flag to true and start the thread

            new Thread(() =>
            {
                while (_isRunning)
                {
                    int? OrderId = _bl?.Order.GetOrderForHandle();
                    if (OrderId != null)
                    {
                        BO.Order tmporder = _bl?.Order.DetailsOfOrderForManager((int)OrderId);
                        BO.Enums.OrderStatus OldStatus = (Enums.OrderStatus)tmporder.Status;

                        int RunTime = _r.Next(3, 11);
                        BO.Enums.OrderStatus NewStatus = (Enums.OrderStatus)tmporder.Status;
                        DateTime StartTime = DateTime.Now;
                        DateTime EndTime = DateTime.Now.AddSeconds(RunTime);
                        ScreenUpdate(OldStatus, StartTime, NewStatus, RunTime, tmporder);

                        Thread.Sleep(1000 * RunTime);
                        if (tmporder.ShipDate == null)
                        {
                            _bl?.Order.ShippingUpdate((int)OrderId); // need todo its random date
                        }
                        else if (tmporder.DeliveryDate == null)
                        {
                            _bl?.Order.UpdateDelivery((int)OrderId);
                        }

                    }
                    else
                    {
                        Thread.Sleep(1000);
                    }
                }
            }).Start();
        }

        /// <summary>
        /// deactivates the simulator on demand
        /// </summary>
        public static void DeActivate()
        {
            _isRunning = false;
        }
    }
}