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



        public ObservableCollection<BO.OrderTracking?> OrderTrackings
        {
            get { return (ObservableCollection<BO.OrderTracking?>)GetValue(OrderTrackingsProperty); }
            set { SetValue(OrderTrackingsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OrderTrackingsProperty =
            DependencyProperty.Register("OrderTrackings", typeof(ObservableCollection<BO.OrderTracking?>), typeof(TrackShowWindow));


        public TrackShowWindow()
        {
            InitializeComponent();
            foreach (var r in bl.Order.GetOrderList())
            {
                OrderTrackings.Add(bl.Order.Track((r ?? throw new Exception()).Id));
            }
        }
    }
}
