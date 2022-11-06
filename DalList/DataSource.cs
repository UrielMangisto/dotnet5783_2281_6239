
using DO;

namespace Dal;

internal static class DataSource
{
    static readonly Random R = new Random(); 
    static internal Product[] products = new Product[50];
    private static void addProductsToTheArray(Product p) { products[p.ID] = p; }
    static internal Order[] orders = new Order[100];
    private static void addOrderToTheArray(Order order) { orders[order.ID] = order; }
    static internal OrderItem[] items = new OrderItem[200];
    private static void addItemToTheArray(OrderItem item) { items[item.ID] = item; }
    private static void s_Initialize() { }
}
