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
using BO;

namespace PL.Order
{
    /// <summary>
    /// Interaction logic for wpf.xaml
    /// </summary>
    public partial class OrdersListWindow : Window
    {
        private readonly IBl bl = Factory.Get();


        public ObservableCollection<BO.OrderForList> Orders
        {
            get { return (ObservableCollection<BO.OrderForList>)GetValue(OrdersProperty); }
            set { SetValue(OrdersProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Orders.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OrdersProperty =
            DependencyProperty.Register("Orders", typeof(ObservableCollection<BO.OrderForList>), typeof(OrdersListWindow));

        //ObservableCollection<BO.OrderForList> Orders = new ObservableCollection<BO.OrderForList>();



        public OrdersListWindow()
        {
            try
            {
                InitializeComponent();
                Orders = new ObservableCollection<BO.OrderForList>(bl.Order.GetOrderList());
            }
            catch
            {

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
            try 
            {
                if ((BO.OrderForList)((ListView)sender).SelectedItem == null)
                    throw new BO.mayBeNullException();
                var orderWindow = new OrderWindow(((BO.OrderForList)((ListView)sender).SelectedItem).Id);
                orderWindow.ShowDialog();
            }
            catch(BO.mayBeNullException)
            {
                MessageBox.Show("Please choose an order to show");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MenuItem_SendOrder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int orderId = ((BO.OrderForList)lstOrders.SelectedItem).Id;
                bl.Order.ShippingUpdate(orderId);

                LoadOrders();

                MessageBox.Show("Order was sent successfuly!");
            }
            catch (BO.NotFoundException )
            {
                MessageBox.Show("already sent!");
            }
            
        }

        private void MenuItem_OrderDelivery_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int orderId = ((BO.OrderForList)lstOrders.SelectedItem).Id;
                bl.Order.UpdateDelivery(orderId);

                LoadOrders();

                MessageBox.Show("Order was delivered successfuly!");
            }
            catch (BO.mayBeNullException)
            {
                if(((BO.OrderForList)lstOrders.SelectedItem).Status == Enums.OrderStatus.Delivered)
                {
                    MessageBox.Show("Already delivered!");
                }
                else
                {
                    MessageBox.Show("You need to send it first!");
                }
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
