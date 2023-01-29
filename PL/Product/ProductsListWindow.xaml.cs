using DO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Common;
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


namespace PL
{
    /// <summary>
    /// Interaction logic for ProductsListWindow.xaml
    /// </summary>
    public partial class ProductsListWindow : Window 
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        
        public ObservableCollection<BO.ProductForList> ProductForLists
        {
            get { return (ObservableCollection<BO.ProductForList>)GetValue(ProductForListsProperty); }
            set { SetValue(ProductForListsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ProductForLists.  This enables animation, styling, binding, etc...
        private static readonly DependencyProperty ProductForListsProperty =
            DependencyProperty.Register("ProductForLists", typeof(ObservableCollection<BO.ProductForList>), typeof(ProductsListWindow));

        //public event PropertyChangedEventHandler? PropertyChanged; 


        public ProductsListWindow()
        {
            InitializeComponent();
            
           
            ProductForLists = new ObservableCollection<BO.ProductForList>(bl.Product.GetProductList()!);
            
            ProductsSelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category));

           // ProductListView.ItemsSource = ProductForLists;
        }


        private void ProductsSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BO.Enums.Category category = (BO.Enums.Category)ProductsSelector.SelectedItem;
            if (category == BO.Enums.Category.All)
            {
                ProductForLists = new ObservableCollection<BO.ProductForList>(bl.Product.GetProductList()!);
            }
            else
            {
                ProductForLists = new ObservableCollection<BO.ProductForList>(bl.Product.GetProductsByTerm(x => x?.Category == category));
            }
        }
        
        private void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            new ProductWindow(addProductForList).Show(); 
        }
        private void addProductForList(int productId)
        {
            ProductForLists.Add(bl.Product.GetProductForList(productId));
        }

        private void updateProductForList(int productId)
        {
            int index = ProductListView.SelectedIndex;
            ProductForLists[index] = bl.Product.GetProductForList(productId);
        }

        private void ProductListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var prod = (BO.ProductForList)ProductListView.SelectedItem;
            new ProductWindow(prod, updateProductForList).Show();
        }
    }
}
