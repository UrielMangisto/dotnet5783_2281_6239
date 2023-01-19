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

using BlApi;

namespace PL.Order
{
    /// <summary>
    /// Interaction logic for wpf.xaml
    /// </summary>
    public partial class OrdersListWindow : Window
    {
        private readonly IBl bl = Factory.Get();
        ObservableCollection<BO.OrderForList> Orders = new ObservableCollection<BO.OrderForList>();

        public OrdersListWindow()
        {
            InitializeComponent();

            try
            {
                lstOrders.ItemsSource = Orders;
                LoadOrders();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadOrders()
        {
            Orders.Clear();
            foreach (var order in bl.Order.GetOrderList())
            {
                if(order != null)
                    Orders.Add(order);
            }
        }

        private void lstOrders_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var orderWindow = new OrderWindow(((BO.OrderForList)lstOrders.SelectedItem).Id);
            orderWindow.ShowDialog();
        }

        private void MenuItem_SendOrder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int orderId = ((BO.OrderForList)lstOrders.SelectedItem).Id;
                bl.Order.ShippingUpdate(orderId);
                MessageBox.Show("Order was sent successfuly.");
            }
            catch (Exception )
            {
                MessageBox.Show("already sent");
            }
        }

        private void MenuItem_OrderDelivery_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int orderId = ((BO.OrderForList)lstOrders.SelectedItem).Id;
                bl.Order.UpdateDelivery(orderId);
                MessageBox.Show("Order was delivered successfuly.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("already delivered");
            }
        }

        private void MenuItem_OrderTracking_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var orderTrackingWindow = new OrderTrackingWindow(((BO.OrderForList)lstOrders.SelectedItem).Id);
                orderTrackingWindow.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
