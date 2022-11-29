using DO;

namespace Dal;

public class DalOrderitem
{
    public int addItem(OrderItem item)
    {
        item.ID = DataSource.Config.NextOrderItemId;
        DataSource.orderItems.Add(item);
        return item.ID;
    }

    public OrderItem? getOrderItem(int id)
    {
        foreach (var p in DataSource.orderItems)
        {
            if (id == p.ID)
            {
                return p;
            }
        }

        throw new Exception("Order Item Not Found");
    }
    /// <summary>
    /// get order item by the id of the product and the order
    /// <param name="idOfProduct"></param>
    /// <param name="idOfOrder"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    /// </summary>
    public OrderItem specificItemGet(int idOfProduct, int idOfOrder)
    {
        foreach (var p in DataSource.orderItems)
        {
            if (idOfProduct == p.ID && idOfOrder == p.OrderID)
            {
                return p;
            }
        }
        throw new Exception("Order Item Not Found");
    }
    //get order items by the id of the order
    public IEnumerable<OrderItem> getItemsByOrder(int orderID)
    {
        int sizeOfNew = 0;
        foreach (var p in DataSource.orderItems)
        {
            if(p.ID == orderID)
            {
                sizeOfNew++;
            }
        }
        List<OrderItem> specificItems = new List<OrderItem>() { };
        foreach (var p in DataSource.orderItems)
        {
            if (p.ID == orderID)
            {
                specificItems.Add(p);
            }
        }

        if (sizeOfNew == 0)
        {
            throw new Exception("Order Not Found");
        }
        return specificItems;
    }
    //returns an array of all products
    public List<OrderItem> getAllItems()
    {
        List<OrderItem> allItems = new List<OrderItem>() { };
        foreach (var p in DataSource.orderItems)
        {
            allItems.Add(p);
        }
        return allItems;
    }

    public void deleteItem(int id)
    {
        foreach (var p in DataSource.orderItems)
        {
            if(id == p.ID)
            {
                DataSource.orderItems.Remove(p);
                return;
            }
        }
        throw new Exception("Order Item Not Found");
    }

    public void updateItem (OrderItem updatedItem)
    {
        foreach (var p in DataSource.orderItems)
        {
            if(updatedItem.ID == p.ID)
            {
                p = updatedItem;
                return;
            }
        }
        
        throw new Exception("Order Item Not Found");

    }

}