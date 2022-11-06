
using DO;

namespace Dal;

internal static class DataSource
{
    static readonly Random R = new Random(); 
    static internal Product[] products = new Product[50];
    private static void createpProducts() { }
    static internal Order[] orders = new Order[100];
    private static void createpOrder() { }
    static internal OrderItem[] items = new OrderItem[200];
    private static void createpItem() { }
    private static void s_Initialize()
}
