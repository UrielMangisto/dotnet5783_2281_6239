using DO;

namespace Dal;

public class DalOrder
{
    public void addNewOrder(Order newOrder)
    {
        int id = 0;
        bool exict = true;
        while (exict)
        {
            id = DataSource.Randomally.Next(100000, 999999);
            try
            {
                getOrder(id);
            }
            catch (Exception exception)
            {
                exict = false;
            }
        }
        newOrder.ID = id;
        DataSource.orders[DataSource.Config.currentSizeOrder++] = newOrder;
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
    public void update(Order updatedOrder)
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
    public Order[] getAll()
    {
        Order[] orders = new Order[DataSource.Config.currentSizeOrder];
        for (int i = 0; i < DataSource.Config.currentSizeOrder; i++)
        {
            orders[i] = DataSource.orders[i];
        }
        return orders;
    }


}