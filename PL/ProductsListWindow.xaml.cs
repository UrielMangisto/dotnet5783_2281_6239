﻿using System;
using System.Collections.Generic;
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
    public partial class ProductsListWindow : Window ,INotifyPropertyChanged
    {
        BlApi.IBl? bl = BlApi.Factory.Get();

        public event PropertyChangedEventHandler? PropertyChanged;

        public ProductsListWindow()
        {
            InitializeComponent();
            ProductListView.ItemsSource = bl.Product.GetProductList();
            ProductsSelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category));
        }


        private void ProductsSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BO.Enums.Category category = (BO.Enums.Category)ProductsSelector.SelectedItem;
            if(category == BO.Enums.Category.All)
            {
                ProductListView.ItemsSource = bl.Product.GetProductList();
            }
            else
            {
                ProductListView.ItemsSource = bl.Product.GetProductsByTerm(x => x?.Category == category);
            }
        }

        private void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            new ProductWindow().Show();
        }

        private void ProductListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            new ProductWindow((BO.ProductForList)ProductListView.SelectedItem).Show();
        }
        /*private BO.ProductForList selectedProduct;
        public BO.ProductForList SelectedProduct 
        {
            get => selectedProduct;
            set
            {
                selectedProduct = value;
                OnPropertyChanged(nameof(SelectedProduct));
               
            }
        }*/
    }
}
