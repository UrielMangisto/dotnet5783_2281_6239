using BlApi;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL.Order
{
    /// <summary>
    /// Interaction logic for TrackShowWindow.xaml
    /// </summary>
    public partial class TrackShowWindow : Window
    {
        private readonly IBl bl = Factory.Get();



        public ObservableCollection<BO.OrderTracking> OrderTrackings
        {
            get { return (ObservableCollection<BO.OrderTracking>)GetValue(OrderTrackingsProperty); }
            set { SetValue(OrderTrackingsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OrderTrackingsProperty =
            DependencyProperty.Register("OrderTrackings", typeof(ObservableCollection<BO.OrderTracking>), typeof(TrackShowWindow));


        public TrackShowWindow()
        {
            InitializeComponent();
          //  var tracklst = new ObservableCollection<BO.OrderTracking>((IEnumerable<BO.OrderTracking>)bl.Order.GetOrderList());
            OrderTrackings = new ObservableCollection<BO.OrderTracking>(bl.Order.getTracList());
        }
        private void TracklstOrders_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try 
            {
                if (((BO.OrderTracking)((ListView)sender).SelectedItem) == null)
                    throw new BO.mayBeNullException();
                var orderWindow = new OrderFromOrderTrackingWindow(((BO.OrderTracking)((ListView)sender).SelectedItem).Id);
                orderWindow.ShowDialog();
            } 
            catch(BO.mayBeNullException)
            {
                MessageBox.Show("Choose an order to track");
            }
            
        }
    }
}
