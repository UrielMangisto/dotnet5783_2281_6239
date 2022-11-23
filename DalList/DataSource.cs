
using System.Diagnostics;
using DO;
namespace Dal;
/// <summary>
/// Those fields and methods are declaring the data
/// of the store and the access into the data. 
/// </summary>
internal static class DataSource
{
    public static readonly Random Randomally = new Random();

    static internal Product[] products = new Product[50];
    static internal Order[] orders = new Order[100];
    static internal OrderItem[] orderItems = new OrderItem[200];

    public static void addProductsToTheArray()
    {
        products[Config.currentSizeProduct++] = new Product { Category = Category.Kids, ID = 769988, InStock = 7, Name = "Harry Poter 1", Price = 130.0 };
        products[Config.currentSizeProduct++] = new Product { Category = Category.Kids, ID = 784848, InStock = 3, Name = "Harry Poter 2", Price = 120.0 };
        products[Config.currentSizeProduct++] = new Product { Category = Category.Kids, ID = 939345, InStock = 5, Name = "Harry Poter 3", Price = 110.0 };
        products[Config.currentSizeProduct++] = new Product { Category = Category.Kids, ID = 923844, InStock = 2, Name = "Harry Poter 4", Price = 110.0 };
        products[Config.currentSizeProduct++] = new Product { Category = Category.Kids, ID = 493934, InStock = 0, Name = "Harry Poter 5", Price = 100.0 };
        products[Config.currentSizeProduct++] = new Product { Category = Category.Kids, ID = 847922, InStock = 4, Name = "Harry Poter 6", Price = 100.0 };
        products[Config.currentSizeProduct++] = new Product { Category = Category.Kids, ID = 759329, InStock = 12, Name = "Harry Poter 7", Price = 90.0 };
        products[Config.currentSizeProduct++] = new Product { Category = Category.Kids, ID = 843902, InStock = 0, Name = "Harry Poter 8", Price = 160.0 };
        products[Config.currentSizeProduct++] = new Product { Category = Category.Comics, ID = 123455, InStock = 25, Name = "Spiderman", Price = 60.0 };
        products[Config.currentSizeProduct++] = new Product { Category = Category.Comics, ID = 541235, InStock = 18, Name = "Superman", Price = 70.0 };
    }
    public static void addOrderToTheArray()
    {
        DateTime dateOfStart = DateTime.Now - new TimeSpan(365, 0, 0, 0);
        for (int i = 0; i < 20; i++)
        {
            Order order = new Order();
            order.ID = Config.getOrderId;
            order.CostumerName = ((ClientName)(i % 10)).ToString();
            order.CostumerAddress = ((ClientAddress)(i % 10)).ToString();
            order.CostumerEmail = ((ClientName)(i % 10)).ToString() + "@gmail.com";

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

    public static void addItemToTheArray()
    {  
        OrderItem orderItem = new OrderItem();

        for (int i = 0; i < Config.currentSizeOrderItem; i++)
        {
            for (int j = 0; j < Randomally.Next(1, 4); j++)
            {
                orderItem.ID = Config.getOrderItemId;
                orderItem.OrderID = orders[i].ID;

                Product product = products[Randomally.Next(0, Config.currentSizeProduct)];
                orderItem.ProductID = product.ID;
                orderItem.Price = product.Price;
                orderItem.Amount = Randomally.Next(1, 10);
                orderItems[Config.currentSizeOrderItem++] = orderItem;
            }
        }
    }

    static DataSource()
    {
        s_Initialize();
    }

    private static void s_Initialize() 
    {
        addProductsToTheArray();
        addOrderToTheArray();
        addItemToTheArray();
    }

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
