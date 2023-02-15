using BlApi;
using BO;
using PL.Order;
using PL.Product;
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
        BO.Cart cart = new BO.Cart();
        BO.Order order = new BO.Order();
        #region Dependency Properties
        public ObservableCollection<BO.OrderItem?> ItemLst
        {
            get { return (ObservableCollection<BO.OrderItem?>)GetValue(ItemLstProperty); }
            set { SetValue(ItemLstProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemLst.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemLstProperty =
            DependencyProperty.Register("ItemLst", typeof(ObservableCollection<BO.OrderItem>), typeof(CartWindow));

        public double CartTotalPrice
        {
            get { return (double)GetValue(CartTotalPriceProperty); }
            set { SetValue(CartTotalPriceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CartTotalPrice.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CartTotalPriceProperty =
            DependencyProperty.Register("CartTotalPrice", typeof(double), typeof(CartWindow));

        public string CostumerName
        {
            get { return (string)GetValue(CostumerNameProperty); }
            set { SetValue(CostumerNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CostumerName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CostumerNameProperty =
            DependencyProperty.Register("CostumerName", typeof(string), typeof(CartWindow));

        public string CostumerEmail
        {
            get { return (string)GetValue(CostumerEmailProperty); }
            set { SetValue(CostumerEmailProperty, value); }
        }

        // Using a DependencyProperty as the backing store for  CostumerEmail.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CostumerEmailProperty =
            DependencyProperty.Register("CostumerEmail", typeof(string), typeof(CartWindow));



        public string CostumerAddress
        {
            get { return (string)GetValue(CostumerAddressProperty); }
            set { SetValue(CostumerAddressProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CostumerAddress.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CostumerAddressProperty =
            DependencyProperty.Register("CostumerAddress", typeof(string), typeof(CartWindow));
        #endregion

        public CartWindow(BO.Cart c)
        {
            InitializeComponent();
            cart = c;
            ItemLst = new ObservableCollection<BO.OrderItem?>(bl.Cart.GetTheItems(c)!);
            CartTotalPrice = c.TotalPrice;
        }

        private void btnConfirmation_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                cart.CostumerName = CostumerNameBox.Text;
                cart.CostumerAddress = CostumerAddressBox.Text;
                cart.CostumerEmail = CostumerEmailBox.Text;
                bl.Cart.Confirmation(cart);
                if (cart.OrderItems.Count == 0)
                {
                    throw new BO.NotvalidException();
                }
                MessageBox.Show("Your order was confirmed successfuly!!");

                this.Close();
            }
            catch(BO.mayBeNullException) 
            {
                MessageBox.Show("Please enter data.");
            }
            catch(BO.NotvalidException)
            {
                MessageBox.Show("order must have at least one product");
            }
        }
        private void Minus_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = sender as Button ?? throw new BO.mayBeNullException();
                BO.OrderItem? item = button.DataContext as BO.OrderItem;
                if (item.Amount > 0)
                {
                    item.Amount--;
                    item.TotalPrice -= item.Price;
                }
                else throw new Exception("The amount is the minimal!");
                cart = bl.Cart.Update(cart, item.ProductId, item.Amount);
                ItemLst = new ObservableCollection<OrderItem?>(ItemLst);
            }
            catch (BO.NotExistInStockException) 
            {
                MessageBox.Show("There isnt enough items");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
        private void Plus_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = sender as Button ?? throw new BO.mayBeNullException();
                BO.OrderItem? item = button.DataContext as BO.OrderItem;
                bl.Cart.Add(cart, item.ProductId);
                ItemLst = new ObservableCollection<OrderItem?>(ItemLst);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
