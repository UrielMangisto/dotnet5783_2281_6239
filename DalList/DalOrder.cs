using DO;
namespace Dal;

public class DalOrder
{
    public int addNewOrder(Order newOrder)
    {
        newOrder.ID = DataSource.Config.getOrderId;
        DataSource.orders[DataSource.Config.currentSizeOrder++] = newOrder;
        return newOrder.ID;
    }
    public void deleteOrder(int id)
    {
        for (int i = 0; i < DataSource.Config.currentSizeOrder; i++)
        {
            if (DataSource.orders[i].ID == id)
            {
                DataSource.orders[i].ID = 0; //in order to delete it
                return;
            }
        }
        throw new Exception("Order Not Found");
    }
    public void updateOrder(Order updatedOrder)
    {
        for (int i = 0; i < DataSource.Config.currentSizeOrder; i++)
        {
            if (DataSource.orders[i].ID == updatedOrder.ID)
            {
                DataSource.orders[i] = updatedOrder;
                return;
            }
        }
        throw new Exception("Order Not Found");
    }
    public Order getOrder(int id)
    {
        for (int i = 0; i < DataSource.Config.currentSizeOrder; i++)
        {
            if (DataSource.orders[i].ID == id)
                return DataSource.orders[i];
        }

        throw new Exception("order Not Found");
    }
    public Order[] getAllOrders()
    {
        Order[] orders = new Order[DataSource.Config.currentSizeOrder];
        for (int i = 0; i < DataSource.Config.currentSizeOrder; i++)
        {
            orders[i] = DataSource.orders[i];
        }
        return orders;
    }


}