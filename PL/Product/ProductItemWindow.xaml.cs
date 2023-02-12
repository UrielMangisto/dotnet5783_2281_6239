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
using BO;
using DO;

namespace PL.Product
{
    /// <summary>
    /// Interaction logic for ProductItemWindow.xaml
    /// </summary>
    public partial class ProductItemWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();

        private BO.Product product { get; set;}

        public BO.ProductItem Product { get; set; }

        #region Depedency Properties

        public BO.Enums.Category? category
        {
            get { return (Enums.Category?)GetValue(categoryProperty); }
            set { SetValue(categoryProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EnumValuesArray.  This enables animation, styling, binding, etc...
        public static DependencyProperty categoryProperty =
            DependencyProperty.Register("category", typeof(BO.Enums.Category?), typeof(ProductItemWindow));



        public int ID
        {
            get { return (int)GetValue(IDProperty); }
            set { SetValue(IDProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ID.  This enables animation, styling, binding, etc...
        public static DependencyProperty IDProperty =
            DependencyProperty.Register("ID", typeof(int), typeof(ProductItemWindow));



        public string Name
        {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Name.  This enables animation, styling, binding, etc...
        public static DependencyProperty NameProperty =
            DependencyProperty.Register("Name", typeof(string), typeof(ProductItemWindow));



        public double Price
        {
            get { return (double)GetValue(PriceProperty); }
            set { SetValue(PriceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Price.  This enables animation, styling, binding, etc...
        public static DependencyProperty PriceProperty =
            DependencyProperty.Register("Price", typeof(double), typeof(ProductItemWindow));



        public int Amount
        {
            get { return (int)GetValue(AmountProperty); }
            set { SetValue(AmountProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Amount.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AmountProperty =
            DependencyProperty.Register("Amount", typeof(int), typeof(ProductItemWindow));



        public bool InStock
        {
            get { return (bool)GetValue(InStockProperty); }
            set { SetValue(InStockProperty, value); }
        }

        // Using a DependencyProperty as the backing store for InStock.  This enables animation, styling, binding, etc...
        public static DependencyProperty InStockProperty =
            DependencyProperty.Register("InStock", typeof(bool), typeof(ProductItemWindow));



        #endregion
        public ProductItemWindow(BO.ProductItem pi)
        {
            InitializeComponent();
            ID = pi.Id;
            Name = pi.Name;
            Price= pi.Price;
            category= pi.Category;
            InStock = pi.InStock;
            Amount = pi.Amount;
        }
    }
}
