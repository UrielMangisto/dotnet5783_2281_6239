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

        #region Depedency Properties
        public BO.ProductForList Product { get; set; }


        private Action<int> action;
        public Array EnumValuesArray
        {
            get { return (Array)GetValue(EnumValuesArrayProperty); }
            set { SetValue(EnumValuesArrayProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EnumValuesArray.  This enables animation, styling, binding, etc...
        public static DependencyProperty EnumValuesArrayProperty =
            DependencyProperty.Register("EnumValuesArray", typeof(Array), typeof(ProductWindow));



        public int ID
        {
            get { return (int)GetValue(IDProperty); }
            set { SetValue(IDProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ID.  This enables animation, styling, binding, etc...
        public static DependencyProperty IDProperty =
            DependencyProperty.Register("ID", typeof(int), typeof(ProductWindow));



        public string Name
        {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Name.  This enables animation, styling, binding, etc...
        public static DependencyProperty NameProperty =
            DependencyProperty.Register("Name", typeof(string), typeof(ProductWindow));



        public double Price
        {
            get { return (double)GetValue(PriceProperty); }
            set { SetValue(PriceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Price.  This enables animation, styling, binding, etc...
        public static DependencyProperty PriceProperty =
            DependencyProperty.Register("Price", typeof(double), typeof(ProductWindow));



        public int InStock
        {
            get { return (int)GetValue(InStockProperty); }
            set { SetValue(InStockProperty, value); }
        }

        // Using a DependencyProperty as the backing store for InStock.  This enables animation, styling, binding, etc...
        public static DependencyProperty InStockProperty =
            DependencyProperty.Register("InStock", typeof(int), typeof(ProductWindow));



        #endregion

        public ProductWindow(Action<int> action)
        {
            InitializeComponent();
            this.action = action;   
            add = true;
            EnumValuesArray = Enum.GetValues(typeof(BO.Enums.Category1));
        }
        public ProductWindow(BO.ProductForList productForList, Action<int> action)
        {
            InitializeComponent();
            add = false;

            this.action = action;

            Product = productForList;

            product = bl.Product.ProductDetailsForManager(productForList.Id);
            ID = product.ID;
            Name = product.Name;
            Price = product.Price;
            InStock = product.InStock;
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
                    newProduct.ID = ID;
                    newProduct.Category = (BO.Enums.Category1)CategoryComboBox.SelectedItem;
                    newProduct.Name = Name;
                    newProduct.Price = Price;
                    newProduct.InStock = InStock;
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
