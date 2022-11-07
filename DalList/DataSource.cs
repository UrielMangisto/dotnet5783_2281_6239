
using DO;

namespace Dal;

internal static class DataSource
{

    static readonly Random R = new Random(); 
    static internal Product[] products = new Product[50];
    private static void addProductsToTheArray(Product p) 
    {
        bool isadd=false;
        for (int i = 0; i < products.Length&&!isadd; i++)
        {
            if (products[i].Name==null)//the place is empty
            {
                products[i] = p;
                isadd=true; 
            }
        }
    }
    static internal Order[] orders = new Order[100];
    private static void addOrderToTheArray(Order order) { orders[order.ID] = order; }
    static internal OrderItem[] orderitems = new OrderItem[200];
    private static void addItemToTheArray(OrderItem item) { orderitems[item.ID] = item;}
    private static void s_Initialize() { }
}
