using DO;

namespace Dal;

public class DalOrderitem
{
    public int addItem(OrderItem item)
    {
        item.ID = DataSource.Config.orderItemId;
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
    //get order item by the id of the product and the order
    public OrderItem? specificItemGet(int idOfProduct, int idOfOrder)
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
    public List<OrderItem> getItemsByOrder(Order order)
    {
        int sizeOfNew = 0;
        foreach (var p in DataSource.orderItems)
        {
            if(p.ID == order.ID)
            {
                sizeOfNew++;
            }
        }
        List<OrderItem> specificItems = new List<OrderItem>() { };
        foreach (var p in DataSource.orderItems)
        {
            if (p.ID == order.ID)
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
    public List<OrderItem?> getAllItems()
    {
        List<OrderItem?> allItems = new List<OrderItem?>() { };
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