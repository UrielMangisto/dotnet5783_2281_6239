using BlApi;
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

namespace PL.Order
{
    /// <summary>
    /// Interaction logic for OrderFromOrderTrackingWindow.xaml
    /// </summary>
    public partial class OrderFromOrderTrackingWindow : Window
    {
        #region Dependency Properties
        public int Id
        {
            get { return (int)GetValue(IdProperty); }
            set { SetValue(IdProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Id.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IdProperty =
            DependencyProperty.Register("Id", typeof(int), typeof(OrderFromOrderTrackingWindow));


        public string CostumerName
        {
            get { return (string)GetValue(CostumerNameProperty); }
            set { SetValue(CostumerNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CostumerName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CostumerNameProperty =
            DependencyProperty.Register("CostumerName", typeof(string), typeof(OrderFromOrderTrackingWindow));




        public string CostumerEmail
        {
            get { return (string)GetValue(CostumerEmailProperty); }
            set { SetValue(CostumerEmailProperty, value); }
        }

        // Using a DependencyProperty as the backing store for  CostumerEmail.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CostumerEmailProperty =
            DependencyProperty.Register("CostumerEmail", typeof(string), typeof(OrderFromOrderTrackingWindow));



        public string CostumerAddress
        {
            get { return (string)GetValue(CostumerAddressProperty); }
            set { SetValue(CostumerAddressProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CostumerAddress.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CostumerAddressProperty =
            DependencyProperty.Register("CostumerAddress", typeof(string), typeof(OrderFromOrderTrackingWindow));



        public string Status
        {
            get { return (string)GetValue(StatusProperty); }
            set { SetValue(StatusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Status.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StatusProperty =
            DependencyProperty.Register("Status", typeof(string), typeof(OrderFromOrderTrackingWindow));



        public DateTime? OrderDate
        {
            get { return (DateTime?)GetValue(OrderDateProperty); }
            set { SetValue(OrderDateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for OrderDate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OrderDateProperty =
            DependencyProperty.Register("OrderDate", typeof(DateTime?), typeof(OrderFromOrderTrackingWindow));



        public DateTime? ShipDate
        {
            get { return (DateTime?)GetValue(ShipDateProperty); }
            set { SetValue(ShipDateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ShipDate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShipDateProperty =
            DependencyProperty.Register("ShipDate", typeof(DateTime?), typeof(OrderFromOrderTrackingWindow));



        public DateTime? DeliveryDate
        {
            get { return (DateTime?)GetValue(DeliveryDateProperty); }
            set { SetValue(DeliveryDateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DeliveryDate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DeliveryDateProperty =
            DependencyProperty.Register("DeliveryDate", typeof(DateTime?), typeof(OrderFromOrderTrackingWindow));



        public double Price
        {
            get { return (double)GetValue(PriceProperty); }
            set { SetValue(PriceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Price.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PriceProperty =
            DependencyProperty.Register("Price", typeof(double), typeof(OrderFromOrderTrackingWindow));



        public ObservableCollection<BO.OrderItem?> Items
        {
            get { return (ObservableCollection<BO.OrderItem?>)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("Items", typeof(ObservableCollection<BO.OrderItem?>), typeof(OrderFromOrderTrackingWindow));

        #endregion

        private readonly IBl bl = Factory.Get();
        public OrderFromOrderTrackingWindow(int trackID)
        {
            InitializeComponent();
            var porder = bl.Order.DetailsOfOrderForManager(trackID);
            Id = porder.Id;
            CostumerName = porder.CostomerName;
            CostumerEmail = porder.CostomerEmail;
            CostumerAddress = porder.CostomerAdress;
            Status = porder.Status.ToString();
            OrderDate = porder.OrderDate;
            ShipDate = porder.ShipDate;
            DeliveryDate = porder.DeliveryDate;
            Price = porder.TotalPrice;
            Items = new ObservableCollection<BO.OrderItem?>(bl.Order.getItemListFromOrder(porder.Id));
        }
    }
}
