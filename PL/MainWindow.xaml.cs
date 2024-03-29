﻿using PL.Order;
using PL.Product;
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
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace PL
{
    
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        BlApi.IBl? bl = BlApi.Factory.Get();
        public MainWindow()
        {
            InitializeComponent();
        }
        private void ToListButton_Click(object sender, RoutedEventArgs e)
        {
            var managerWindow = new ManagerPanel();
            managerWindow.Show();
        }

        private void NewOrderButton_Click(object sender, RoutedEventArgs e)
        {
            var catalogWindow = new ProductItemListWindow();
            catalogWindow.Show();
        }

        private void OrderTrackingButton_Click(object sender, RoutedEventArgs e)
        {
            var track = new TrackShowWindow();
            track.Show();
        }

        private void SimulatorButton_Click(object sender, RoutedEventArgs e)
        {
            var simu = new SimulatorWindow();
            simu.Show();
        }
    }
}
