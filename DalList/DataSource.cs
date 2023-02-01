using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Diagnostics;
using DO;
using DalApi;
namespace Dal;
/// <summary>
/// Those fields and methods are declaring the data
/// of the store and the access into the data. 
/// </summary>
internal static class DataSource
{
    internal static readonly Random Randomally = new Random();

    static internal List<Order?> orders { get; } = new List<Order?>() { };
    static internal List<Product?> products { get; } = new List<Product?>() { };
    static internal List<OrderItem?> orderItems { get; } = new List<OrderItem?>() { };

    public static void addProductsToTheArray()
    {
        products.Add ( new Product { Category = Category.Kids, productID = 769988, InStock = 7, Name = "Harry Poter 1", Price = 130.0 });
        products.Add ( new Product { Category = Category.Kids, productID = 784848, InStock = 3, Name = "Harry Poter 2", Price = 120.0 });
        products.Add ( new Product { Category = Category.Kids, productID = 939345, InStock = 5, Name = "Harry Poter 3", Price = 110.0 });
        products.Add ( new Product { Category = Category.Kids, productID = 923844, InStock = 2, Name = "Harry Poter 4", Price = 110.0 });
        products.Add ( new Product { Category = Category.Kids, productID = 493934, InStock = 0, Name = "Harry Poter 5", Price = 100.0 });
        products.Add ( new Product { Category = Category.Kids, productID = 847922, InStock = 4, Name = "Harry Poter 6", Price = 100.0 });
        products.Add ( new Product { Category = Category.Kids, productID = 759329, InStock = 12, Name = "Harry Poter 7", Price = 90.0 });
        products.Add ( new Product { Category = Category.Kids, productID = 843902, InStock = 0, Name = "Harry Poter 8", Price = 160.0 });
        products.Add ( new Product { Category = Category.Comics, productID = 123455, InStock = 25, Name = "Spiderman", Price = 60.0 });
        products.Add ( new Product { Category = Category.Comics, productID = 541235, InStock = 18, Name = "Superman", Price = 70.0 });
        products.Add ( new Product { Category = Category.Comics, productID = 548735, InStock = 5, Name = "Bon", Price = 80.0 });

    }
    public static void addOrderToTheArray()
    {
        DateTime dateOfStart = DateTime.Now - new TimeSpan(365, 0, 0, 0);
        for (int i = 0; i < 20; i++)
        {
            Order order = new Order();
            order.orderID = Config.NextOrderId;
            order.CostumerName = ((ClientName)(i % 10)).ToString();
            order.CostumerAddress = ((ClientAddress)(i % 10)).ToString();
            order.CostumerEmail = ((ClientName)(i % 10)).ToString() + "@gmail.com";

            order.OrderDate = dateOfStart + new TimeSpan(Randomally.Next(180), 0, 0, 0);
            if (i<= 16)
            {
                //Order.ShipDate = Order.OrderDate + new TimeSpan(Randomally.Next(90),0, 0, 0);
            }
            else
            {
                //Order.ShipDate = DateTime.MinValue;
            }
            if (i<= 10)
            {
                //Order.DeliveryDate = Order.OrderDate + new TimeSpan(90, 0, 0, 0);
            }
            else
            {
                //Order.DeliveryDate= DateTime.MinValue;
            }
            orders.Add (order);
        }
    }
    public static void addItemToTheArray()
    {  
        OrderItem orderItem = new OrderItem();

        for (int i = 0; i < orders.Count; i++)
        {
            for (int j = 0; j < Randomally.Next(1, 4); j++)
            {
                orderItem.orderItemID = Config.NextOrderItemId;
                orderItem.OrderID = orders[i]?.orderID ?? throw new mayBeNullException();

                Product? product = products[Randomally.Next(0, products.Count)];
                orderItem.ProductID = product?.productID ?? throw new mayBeNullException();
                orderItem.Price = product?.Price ?? throw new mayBeNullException();
                orderItem.Amount = Randomally.Next(1, 10);
                orderItems.Add(orderItem);
            }
        }
    }
    private static void s_Initialize()
    {
        addProductsToTheArray();
        addOrderToTheArray();
        addItemToTheArray();
    }

    static DataSource()
    {
        s_Initialize();
    }

/*    internal static class Config 
    {
        internal static int orderItemId = 1;
        internal static int orderId = 1;

        internal static int getOrderItemId => orderItemId++;
        internal static int getOrderId => orderItemId++;
    }*/
    internal static class Config
    {
        //Order
        internal const int s_startOrderId = 100000;
        private static int s_nextOrderId = s_startOrderId;
        internal static int NextOrderId { get => s_nextOrderId++; }

        //Order item
        internal const int s_startOrderItemId = 100000;
        private static int s_nextOrderItemId = s_startOrderItemId;
        internal static int NextOrderItemId { get => s_nextOrderItemId++; }

        //Product
        internal const int s_startProductId = 0;
        private static int s_nextProductId = s_startProductId;
        internal static int NextOrderProductId { get => Randomally.Next(100000, 999999); }

    }
}
