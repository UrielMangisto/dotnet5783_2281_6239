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


namespace PL
{
    /// <summary>
    /// Interaction logic for AddProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        private bool add;//if false update mode
        
        private BO.Product product = new BO.Product();

        public BO.ProductForList Product { get; set; }


        private Action<int> action;
        public int ID
        { 
            get { return (int)GetValue(IdProperty); } 
            set { SetValue(IdProperty, value); }
        }

        public static DependencyProperty IdProperty =
            DependencyProperty.Register("ID", typeof(int), typeof(ProductWindow));
        public ProductWindow(Action<int> action)
        {
            InitializeComponent();
            this.action = action;   
            add = true;
            CategoryComboBox.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category1));
        }
        public ProductWindow(BO.ProductForList productForList, Action<int> action)
        {
            InitializeComponent();
            add = false;

            this.action = action;

            Product = productForList;

            product = bl.Product.ProductDetailsForManager(productForList.Id);
            this.DataContext = product;
            AddProductButton.Content = "Update";
            IdBox.IsReadOnly = true;
            CategoryComboBox.IsReadOnly = true;
            NameBox.IsReadOnly = true;

            CategoryComboBox.Items.Add(product.Category.ToString());
            CategoryComboBox.SelectedValue = product.Category.ToString();
        }

        private void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            if (add)
            {
                try
                {
                    BO.Product newProduct = new BO.Product();
                    newProduct.ID = int.Parse(IdBox.Text);
                    newProduct.Category = (BO.Enums.Category1)CategoryComboBox.SelectedItem;
                    newProduct.Name = NameBox.Text;
                    newProduct.Price = double.Parse(PriceBox.Text);
                    newProduct.InStock = int.Parse(InStockBox.Text);
                    bl.Product.Add(newProduct);
                    action?.Invoke(newProduct.ID);
                    MessageBox.Show("Product added succesfully!");
                }
                catch
                {
                    MessageBox.Show("Error!");
                    this.Close();
                }
            }
            else 
            { 
                product.Price = double.Parse(PriceBox.Text);
                product.InStock = int.Parse(InStockBox.Text);

                bl.Product.Update(product);

                action?.Invoke(product.ID);

                MessageBox.Show("Product updated succesfully!");
            }
            this.Close();
            return;
        }
    }
}
