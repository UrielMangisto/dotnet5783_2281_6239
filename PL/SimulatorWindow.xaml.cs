using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
namespace PL;

/// <summary>
/// Interaction logic for SimulatorWindow.xaml
/// </summary>
public partial class SimulatorWindow : Window
{
    private BlApi.IBl? _bl = BlApi.Factory.Get(); 
    private Stopwatch _stopWatch = new();
    private volatile bool _isTimerRun;
    private BackgroundWorker _backgroundWorker = new();
    public string ExpectedOrderDetails
    {
        get => (string)GetValue(ExpectedOrderDetailsProperty);
        set => SetValue(ExpectedOrderDetailsProperty, value);
    }
    /// <summary>
    /// Using a DependencyProperty as the backing store for ExpectedOrderDetails.  This enables animation, styling, binding, etc...
    /// </summary>
    public static readonly DependencyProperty ExpectedOrderDetailsProperty =
        DependencyProperty.Register(nameof(ExpectedOrderDetails), typeof(string), typeof(SimulatorWindow), new PropertyMetadata(null));

    public string CurrentOrderHandle
    {
        get => (string)GetValue(CurrentOrderHandleProperty);
        set => SetValue(CurrentOrderHandleProperty, value);
    }
    // Using a DependencyProperty as the backing store for CurrentOrderHandle.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty CurrentOrderHandleProperty =
        DependencyProperty.Register(nameof(CurrentOrderHandle), typeof(string), typeof(SimulatorWindow), new PropertyMetadata(null));

    public string winClock
    {
        get => (string)GetValue(winClockProperty);
        set => SetValue(winClockProperty, value);
    }
    ///<summary>
    /// Using a DependencyProperty as the backing store for winClock.  This enables animation, styling, binding, etc...
    /// </summary>
    public static readonly DependencyProperty winClockProperty =
        DependencyProperty.Register(nameof(winClock), typeof(string), typeof(SimulatorWindow), new PropertyMetadata(null));


    public SimulatorWindow()
    {
        InitializeComponent();
        winClock = "00:00:00";
        _backgroundWorker.DoWork += Work;
        _backgroundWorker.ProgressChanged += UpdateScreen;
        _backgroundWorker.RunWorkerCompleted += closeHandler;

        Simulator.Simulator.WindowUpdate += Simulator_ScreenUpdate;
        Simulator.Simulator.simulationStop += Simulator_StopSimu;

        _backgroundWorker.WorkerReportsProgress = true;
        _backgroundWorker.WorkerSupportsCancellation = true;

        _stopWatch.Start();
        _isTimerRun = true;
        _backgroundWorker.RunWorkerAsync();
    }

    private void UpdateScreen(Object? sender, ProgressChangedEventArgs? e)
    {
        //update the screen - call from ReportProgress - if want to update order details send order id, for clock update send 1
        if (e?.ProgressPercentage > 1)//want to update order details
        {
            var args = (Tuple<BO.Order, int>)e.UserState!;//extract the Tuple that contain Random time to end and the order details


            CurrentOrderHandle = "ID : " + args.Item1.Id + "\nCurrent status : " + args.Item1.Status;//update current order handle details 


            string timerText = _stopWatch.Elapsed.ToString();//extract start time and end time
            timerText = timerText.Substring(0, 8);
            string endTime = (_stopWatch.Elapsed + TimeSpan.FromSeconds((double)(e?.ProgressPercentage))).ToString()[..8];


            ExpectedOrderDetails = "Started at : " + timerText.ToString() + "\nSwitch time : " + endTime + "\nWill be " + (args.Item1.Status == BO.Enums.OrderStatus.Sent ? "Deliveried." : "Sent.");//update expected time & status of order after work will done
        }
        else if (e?.ProgressPercentage == 1)//clock update
        {
            string timerText = _stopWatch.Elapsed.ToString();
            winClock = timerText.Substring(0, 8);
        }

    }

    private void endSmBtn_Click(object sender, RoutedEventArgs e)
    {
        _backgroundWorker.CancelAsync();
    }

    private void Window_Closing(object sender, CancelEventArgs e)
    {
        if (_backgroundWorker.CancellationPending == false && _isTimerRun)
        {
            e.Cancel = true;
        }
    }

    private void closeHandler(object? sender, RunWorkerCompletedEventArgs? e)
    {
        
        Simulator.Simulator.DeActivate();
        _isTimerRun = false;
        Close();
    }

    private void Simulator_ScreenUpdate(BO.Enums.OrderStatus OldStatus, DateTime StartTime, BO.Enums.OrderStatus NewStatus, int EndTime, BO.Order order)//
    {
        Tuple<BO.Order, int> t = new Tuple<BO.Order, int>(order, EndTime);//Tuple order and random time, then send it to ReportProgress 
        _backgroundWorker.ReportProgress(EndTime, t);
    }

    private void Work(object? sender, DoWorkEventArgs? e)
    {
        Simulator.Simulator.Activate();
        while (!_backgroundWorker.CancellationPending)//handle clock
        {
            _backgroundWorker.ReportProgress(1);
            Thread.Sleep(1000);
        }
    }

    private void Simulator_StopSimu()
    {
        _backgroundWorker.CancelAsync();
        MessageBox.Show(@"The Orders Are Done! Bye", "Message", MessageBoxButton.OK, MessageBoxImage.Exclamation);
    }
}
