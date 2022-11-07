
using DO;

namespace Dal;

internal static class DataSource
{

    static readonly Random R = new Random(); 
    static internal Product[] products = new Product[50];
   
    static int currentSize = 0;
    internal static void addProductsToTheArray(Product p)
    {
        //if (something == (products.Length - 1))
        //    Console.WriteLine("the array is full");
        products[currentSize++] = p;
    }

    static internal Order[] orders = new Order[100];
    private static void addOrderToTheArray(Order order) { orders[order.ID] = order; }
    static internal OrderItem[] orderitems = new OrderItem[200];
    private static void addItemToTheArray(OrderItem item) { orderitems[item.ID] = item;}
    private static void s_Initialize() { }
}
