using BlImplementation;
using BO;
using PL.Cart;
using PL.Order;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Metadata;
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

namespace PL.Product
{
    /// <summary>
    /// Interaction logic for ProductItemListWindow.xaml
    /// </summary>
    public partial class ProductItemListWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();


        public ObservableCollection<BO.ProductItem?> ProductItems
        {
            get { return (ObservableCollection<BO.ProductItem?>)GetValue(ProductItemsProperty); }
            set { SetValue(ProductItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ProductItems.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ProductItemsProperty =
            DependencyProperty.Register("ProductItems", typeof(ObservableCollection<BO.ProductItem?>), typeof(ProductItemListWindow));


        public ObservableCollection<BO.ProductItem?> ProductItems1
        {
            get { return (ObservableCollection<BO.ProductItem?>)GetValue(ProductItems1Property); }
            set { SetValue(ProductItems1Property, value); }
        }

        // Using a DependencyProperty as the backing store for ProductItems1.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ProductItems1Property =
            DependencyProperty.Register("ProductItems1", typeof(ObservableCollection<BO.ProductItem?>), typeof(ProductItemListWindow));



        
        public ProductItemListWindow()
        {
            InitializeComponent();
            ProductItems = new ObservableCollection<BO.ProductItem?>(bl.Product.CatalogRequest()!);
            ItemsSelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category));
        }
        

        private void ItemsSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BO.Enums.Category category = (BO.Enums.Category)ItemsSelector.SelectedItem;
            if (category == BO.Enums.Category.All)
            {
                ProductItems = new ObservableCollection<BO.ProductItem?>(bl.Product.CatalogRequest()!);
            }
            else
            {
                ProductItems = new ObservableCollection<BO.ProductItem?>(bl.Product.GetItemsByTerm(x => x?.Category == category));
            }
        }

        private void btnCartList_Click(object sender, RoutedEventArgs e)
        {
            BO.Cart cart = new BO.Cart();
            var cartWindow = new CartWindow(cart);
            cartWindow.Show();
        }

        private void ProductItemListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var Item = new ProductItemWindow(((BO.ProductItem)((ListView)sender).SelectedItem));
            Item.ShowDialog();
        }

        private void GrupingBtn_Click(object sender, RoutedEventArgs e)
        {
            ProductItems = new ObservableCollection<BO.ProductItem?>(bl.Product.catalogGrouping(bl.Product.CatalogRequest())!);
        }
        private void Minus_Click (object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = sender as Button ?? throw new BO.mayBeNullException();
                BO.ProductItem? product = button.DataContext as BO.ProductItem;
                
                foreach (var pi in ProductItems)
                {
                    if (pi?.Id == product.Id)
                    {
                        if (pi.Amount > 0)
                        {
                            pi.Amount--;
                        }
                        else
                            throw new Exception("the amount is the minimal!");
                    }
                }
                ProductItems = new ObservableCollection<ProductItem?>(ProductItems);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Plus_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button ?? throw new BO.mayBeNullException();
            BO.ProductItem? product = button.DataContext as BO.ProductItem;

            foreach (var pi in ProductItems)
            {
                if (pi?.Id == product.Id)
                {
                    pi.Amount++;
                    
                }
            }
            ProductItems = new ObservableCollection<ProductItem?>(ProductItems);
        }
    }
}
