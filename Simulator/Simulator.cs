using BO;
namespace Simulator
{

    public static class Simulator
    {

        private static readonly BlApi.IBl? _bl = BlApi.Factory.Get();
        private static Random _r = new Random(DateTime.Now.Millisecond);

        private static volatile bool _isRunning;//in order to escape the threade

        /// <summary>
        /// in order to update event
        /// </summary>
        public delegate void update(BO.Enums.OrderStatus OldStatus, DateTime StartTime,
            BO.Enums.OrderStatus NewStatus, int EndTime, BO.Order ord);

        public static event update? WindowUpdate;//update the event

        /// <summary>
        /// in order to stop the event
        /// </summary>
        public delegate void Stop();

        public static event Stop? simulationStop;//stop the event

        /// <summary>
        /// in order to activate the simulation
        /// </summary>
        public static void Activate()
        {
            _isRunning = true; 

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
                        WindowUpdate(OldStatus, StartTime, NewStatus, RunTime, tmporder);

                        Thread.Sleep(1000 * RunTime);
                        if (tmporder.ShipDate == null)
                        {
                            _bl?.Order.ShippingUpdate((int)OrderId); 
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
        /// in order to deactivate the simulation
        /// </summary>
        public static void DeActivate()
        {
            _isRunning = false;
        }
    }
}