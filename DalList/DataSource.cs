using System;
using System.Runtime.CompilerServices;
using DO;
namespace Dal;
/// <summary>
/// Those fields and methods are declaring the data
/// of the store and the access into the data. 
/// </summary>
internal static class DataSource
{
    static readonly Random R = new Random();

    static int currentSizeProduct = 0;
    static int currentSizeOrder = 0;
    static int currentSizeOrderItem = 0;

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
            order.ID = Config.GetOrderId;
            order.CostumerName = ((Enums.ClientName)(i % 10)).ToString();
            order.CostumerAddress = ((Enums.ClientAddress)(i % 10)).ToString();
            order.CostumerEmail = ((Enums.ClientName)(i % 10)).ToString() + "@gmail.com";
        }
    }
    private static void addItemToTheArray(OrderItem item)
    {  //if (currentSizeOrderItem == (orderItems.Length - 1))
        //    Console.WriteLine("the array is full");
        orderItems[currentSizeOrderItem++] = item;
    }
    private static void s_Initialize() { }
    private static class Config {}
}
