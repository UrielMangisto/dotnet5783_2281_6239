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



        public BO.Cart MyCart
        {
            get { return (BO.Cart)GetValue(MyCartProperty); }
            set { SetValue(MyCartProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyCart.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MyCartProperty =
            DependencyProperty.Register("MyCart", typeof(BO.Cart), typeof(CartWindow));



        public ObservableCollection<BO.OrderItem> itemsList
        {
            get { return (ObservableCollection<BO.OrderItem>)GetValue( itemsListProperty); }
            set { SetValue( itemsListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for  itemsList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty  itemsListProperty =
            DependencyProperty.Register(" itemsList", typeof(ObservableCollection<BO.OrderItem>), typeof(CartWindow));









        public CartWindow(BO.Cart c)
        {
            InitializeComponent();


            MyCart.OrderItems;
        }
        


        private void lstCartOrders_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void MenuItem_Remove_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
