using System;
using System.Collections.Generic;
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
using PL.Cart;

namespace PL
{
    /// <summary>
    /// Interaction logic for ManagerPanel.xaml
    /// </summary>
    public partial class ManagerPanel : Window
    {
        public ManagerPanel()
        {
            InitializeComponent();
        }

        private void btnProductsList_Click(object sender, RoutedEventArgs e)
        {
            var productsWindow = new ProductsListWindow();
            productsWindow.Show();
        }

        private void btnOdersList_Click(object sender, RoutedEventArgs e)
        {
            var ordersWindow = new Order.OrdersListWindow();
            ordersWindow.Show();
        }
    }
}
