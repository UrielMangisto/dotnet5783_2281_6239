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
using BlApi;
using BO;
using DO;

namespace PL.Order
{
    /// <summary>
    /// Interaction logic for OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {

        #region Dependency Properties
        public int Id
        {
            get { return (int)GetValue(IdProperty); }
            set { SetValue(IdProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Id.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IdProperty =
            DependencyProperty.Register("Id", typeof(int), typeof(OrderWindow));


        public string CostumerName
        {
            get { return (string)GetValue(CostumerNameProperty); }
            set { SetValue(CostumerNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CostumerName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CostumerNameProperty =
            DependencyProperty.Register("CostumerName", typeof(string), typeof(OrderWindow));




        public string CostumerEmail
        {
            get { return (string)GetValue( CostumerEmailProperty); }
            set { SetValue( CostumerEmailProperty, value); }
        }

        // Using a DependencyProperty as the backing store for  CostumerEmail.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty  CostumerEmailProperty =
            DependencyProperty.Register("CostumerEmail", typeof(string), typeof(OrderWindow));



        public string CostumerAddress
        {
            get { return (string)GetValue(CostumerAddressProperty); }
            set { SetValue(CostumerAddressProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CostumerAddress.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CostumerAddressProperty =
            DependencyProperty.Register("CostumerAddress", typeof(string), typeof(OrderWindow));



        public string Status
        {
            get { return (string)GetValue(StatusProperty); }
            set { SetValue(StatusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Status.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StatusProperty =
            DependencyProperty.Register("Status", typeof(string), typeof(OrderWindow));



        public DateTime? OrderDate
        {
            get { return (DateTime?)GetValue(OrderDateProperty); }
            set { SetValue(OrderDateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for OrderDate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OrderDateProperty =
            DependencyProperty.Register("OrderDate", typeof(DateTime?), typeof(OrderWindow));



        public DateTime? ShipDate
        {
            get { return (DateTime?)GetValue(ShipDateProperty); }
            set { SetValue(ShipDateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ShipDate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShipDateProperty =
            DependencyProperty.Register("ShipDate", typeof(DateTime?), typeof(OrderWindow));



        public DateTime? DeliveryDate
        {
            get { return (DateTime?)GetValue(DeliveryDateProperty); }
            set { SetValue(DeliveryDateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DeliveryDate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DeliveryDateProperty =
            DependencyProperty.Register("DeliveryDate", typeof(DateTime?), typeof(OrderWindow));



        public double Price
        {
            get { return (double)GetValue(PriceProperty); }
            set { SetValue(PriceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Price.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PriceProperty =
            DependencyProperty.Register("Price", typeof(double), typeof(OrderWindow));



        public ObservableCollection<BO.OrderItem?> Items
        {
            get { return (ObservableCollection<BO.OrderItem?>)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("Items", typeof(ObservableCollection<BO.OrderItem?>), typeof(OrderWindow));



        public bool JustConfirmed
        {
            get { return (bool)GetValue(JustConfirmedProperty); }
            set { SetValue(JustConfirmedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for JustConfirmed.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty JustConfirmedProperty =
            DependencyProperty.Register("JustConfirmed", typeof(bool), typeof(OrderWindow));



        #endregion

        private readonly IBl bl = Factory.Get();
        private BO.Order porder = new BO.Order();
        public OrderWindow(int orderId)
        {
            InitializeComponent();
            porder = bl.Order.DetailsOfOrderForManager(orderId);
            Id = porder.Id;
            CostumerName = porder.CostumerName;
            CostumerEmail = porder.CostumerEmail;
            CostumerAddress = porder.CostumerAddress;
            Status = porder.Status.ToString();
            OrderDate = porder.OrderDate;
            ShipDate = porder.ShipDate;
            DeliveryDate= porder.DeliveryDate;
            Price = porder.TotalPrice;
            Items = new ObservableCollection<BO.OrderItem?>(bl.Order.getItemListFromOrder(porder.Id));
            JustConfirmed = !(porder.Status == Enums.OrderStatus.Confirmed);
        }

        private void UpdateOrderButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in Items) 
            {
                foreach(var it in porder.OrderItems ??throw new Exception())
                {
                    if(it.Id == item.Id)
                    {
                        it.Amount = item.Amount;
                    }
                }
            }
            porder.CostumerName = CostumerNameBox.Text;
            porder.CostumerEmail= CostumerEmailBox.Text;
            porder.CostumerAddress= CostumerAddressBox.Text;
            bl.Order.Update(porder);
            MessageBox.Show("Order was updated successfuly!");
            var lstWin = new OrdersListWindow();
            this.Close();
            lstWin.Show();
        }

        private void Minus_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = sender as Button ?? throw new BO.mayBeNullException();
                BO.OrderItem? item = button.DataContext as BO.OrderItem;

                foreach (var it in Items)
                {
                    if (it.Id == item.Id)
                    {
                        if (it.Amount > 0)
                            it.Amount--;
                        else throw new Exception("The amount is the minimal!");
                    }
                }
                Items = new ObservableCollection<BO.OrderItem?>(Items);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Plus_Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button ?? throw new BO.mayBeNullException();
            BO.OrderItem? item = button.DataContext as BO.OrderItem;
            foreach (var it in Items)
            {
                if (it.Id == item.Id)
                {
                    it.Amount++;
                }
            }
            Items = new ObservableCollection<BO.OrderItem?>(Items);
        }
    }
}
