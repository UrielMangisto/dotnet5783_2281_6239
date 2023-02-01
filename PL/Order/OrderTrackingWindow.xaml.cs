using BlApi;
using BO;
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
    /// Interaction logic for OrderTrackingWindow.xaml
    /// </summary>
    public partial class OrderTrackingWindow : Window
    {

        private readonly IBl bl = Factory.Get();
        #region Dependency Properties

        public int ID
        {
            get { return (int)GetValue(IDProperty); }
            set { SetValue(IDProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ID.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IDProperty =
            DependencyProperty.Register("ID", typeof(int), typeof(OrderTrackingWindow));



        public BO.Enums.OrderStatus? Status
        {
            get { return (BO.Enums.OrderStatus?)GetValue(StatusProperty); }
            set { SetValue(StatusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Status.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StatusProperty =
            DependencyProperty.Register("Status", typeof(BO.Enums.OrderStatus?), typeof(OrderTrackingWindow));



        public ObservableCollection<TrackLst> trackLst
        {
            get { return (ObservableCollection<TrackLst>)GetValue(trackLstProperty); }
            set { SetValue(trackLstProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty trackLstProperty =
            DependencyProperty.Register("trackLst", typeof(ObservableCollection<TrackLst>), typeof(OrderTrackingWindow));


        #endregion
        public OrderTrackingWindow(int orderId)
        {
            InitializeComponent();
            var porder = bl.Order.Track(orderId);
            ID = porder.Id;
            Status = porder.Status;
            trackLst = new ObservableCollection<TrackLst>(bl.Order.getListOfTrack(porder));
            
        }

    }
}
