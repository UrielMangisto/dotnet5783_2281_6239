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
    private static void addOrderToTheArray(Order order)
    {
        //if (currentSizeOrder == (orders.Length - 1))
        //    Console.WriteLine("the array is full");
        orders[currentSizeOrder++] = order;
    }
    private static void addItemToTheArray(OrderItem item)
    {  //if (currentSizeOrderItem == (orderItems.Length - 1))
        //    Console.WriteLine("the array is full");
        orderItems[currentSizeOrderItem++] = item;
    }
    private static void s_Initialize() { }
}
