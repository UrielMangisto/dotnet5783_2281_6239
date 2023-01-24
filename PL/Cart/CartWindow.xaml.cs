using BlApi;
using PL.Order;
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

namespace PL.Cart
{
    /// <summary>
    /// Interaction logic for CartWindow.xaml
    /// </summary>
    public partial class CartWindow : Window
    {
        private BlApi.IBl bl = Factory.Get();

        public ObservableCollection<BO.OrderForList> Orders
        {
            get { return (ObservableCollection<BO.OrderForList>)GetValue(OrdersProperty); }
            set { SetValue(OrdersProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Orders.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OrdersProperty =
            DependencyProperty.Register("Orders", typeof(ObservableCollection<BO.OrderForList>), typeof(CartWindow));

        public CartWindow()
        {
            InitializeComponent();


            Orders = new ObservableCollection<BO.OrderForList>(bl.Order.GetOrderList());
        }

        private void lstCartOrders_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void MenuItem_Remove_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
