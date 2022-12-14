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
using BlApi;
using BlImplementation;

namespace PL
{
    /// <summary>
    /// Interaction logic for ProductsListWindow.xaml
    /// </summary>
    public partial class ProductsListWindow : Window
    {
        private IBl bl = new BL();
        public ProductsListWindow()
        {
            InitializeComponent();
            ProductListView.ItemsSource = bl.Product.GetProductList();
            ProductsSelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category));
        }

        private void ProductsSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BO.Enums.Category category = (BO.Enums.Category)ProductsSelector.SelectedItem;
            //ProductListView.ItemsSource = bl.Product.GetProductsByTerm( x => x?.C== );
        }
    }
}
