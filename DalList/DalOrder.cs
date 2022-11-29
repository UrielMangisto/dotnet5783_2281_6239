using DO;
namespace Dal;

public class DalOrder
{
    public int addNewOrder(Order newOrder)
    {
        newOrder.ID = DataSource.Config.getOrderId;
        DataSource.orders.Add(newOrder);
        return newOrder.ID;
    }
    public void deleteOrder(int id)
    {
        foreach (var p in DataSource.orders)
        {
            if (p?.ID == id)
            {
                DataSource.orders.Remove(p); //in order to delete it
                return;
            }
        }
        throw new Exception("Order Not Found");
    }
    public void updateOrder(Order? updatedOrder)
    {
        foreach (var p in DataSource.orders)
        {
            if (p?.ID == updatedOrder?.ID)
            {
                p = updatedOrder;
                return;
            }
        }
        throw new Exception("Order Not Found");
    }
    public Order? getOrder(int id)
    {
        
        foreach (var p in DataSource.orders)
        {
            if (p?.ID == id)
                return p;
        }

        throw new Exception("order Not Found");
    }
    public List<Order?> getAllOrders()
    {
        List <Order?> orders = new List <Order?> ();
        foreach (var p in DataSource.orders)
        {
            orders.Add(p);
        }
        return orders;
    }
        

}