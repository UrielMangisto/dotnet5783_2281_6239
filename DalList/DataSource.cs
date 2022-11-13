
using System.Diagnostics;
using DO;
namespace Dal;
/// <summary>
/// Those fields and methods are declaring the data
/// of the store and the access into the data. 
/// </summary>
internal static class DataSource
{
    static readonly Random Randomally = new Random();

    static internal Product[] products = new Product[50];
    static internal Order[] orders = new Order[100];
    static internal OrderItem[] orderItems = new OrderItem[200];

    internal static void addProductsToTheArray(Product p)
    {
        //if (currentSizeProduct == (products.Length - 1))
        //    Console.WriteLine("the array is full");
        products[currentSizeProduct++] = p;
    }
    private static void addOrderToTheArray()
    {
        DateTime dateOfStart = DateTime.Now - new TimeSpan(365, 0, 0, 0);
        for (int i = 0; i < 20; i++)
        {
            Order order = new Order();
            order.ID = Config.getOrderId;
            order.CostumerName = ((Enums.ClientName)(i % 10)).ToString();
            order.CostumerAddress = ((Enums.ClientAddress)(i % 10)).ToString();
            order.CostumerEmail = ((Enums.ClientName)(i % 10)).ToString() + "@gmail.com";

            order.OrderDate = dateOfStart + new TimeSpan(Randomally.Next(180), 0, 0, 0);
            if (i<= 16)
            {
                order.ShipDate = order.OrderDate + new TimeSpan(Randomally.Next(90),0, 0, 0);
            }
            else
            {
                order.ShipDate = DateTime.MinValue;
            }
            if (i<= 10)
            {
                order.DeliveryDate = order.OrderDate + new TimeSpan(90, 0, 0, 0);
            }
            else
            {
                order.DeliveryDate= DateTime.MinValue;
            }
            orders[Config.currentSizeOrder++] = order;
        }
    }
    private static void addItemToTheArray(OrderItem item)
    {  
        OrderItem orderItem = new OrderItem();

        for(int i = 0; i < Config.currentSizeOrderItem; i++)
        {
            for(int j = 0; j < Randomally.Next(); j++)
        }
    }
    private static void s_Initialize() { }
    internal static class Config 
    {
        internal static int currentSizeProduct = 0;
        internal static int currentSizeOrder = 0;
        internal static int currentSizeOrderItem = 0;

        internal static int orderItemId = 1;
        internal static int orderId = 1;

        internal static int getOrderItemId => orderItemId++;
        internal static int getOrderId => orderItemId++;
    }
}
