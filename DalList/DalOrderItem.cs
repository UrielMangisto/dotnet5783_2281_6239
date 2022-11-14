using DO;

namespace Dal;

internal class DalOrderitem
{
    public int addItem(OrderItem item)
    {
        item.ID = DataSource.Config.orderItemId;
        DataSource.orderItems[DataSource.Config.currentSizeOrderItem++] = item;
        return item.ID;
    }

    public OrderItem getOrderItem(int id)
    {
        for (int i = 0; i < DataSource.Config.currentSizeOrderItem; i++)
        {
            if (id == DataSource.orderItems[i].ID)
            {
                return DataSource.orderItems[i];
            }
        }

        throw new Exception("Order Item Not Found");
    }

    public OrderItem specificItemGet(int idOfProduct, int idOfOrder)
    {
        for(int i = 0; i < DataSource.Config.currentSizeOrderItem; i++)
        {
            if(idOfProduct == DataSource.orderItems[i].ID && idOfOrder == DataSource.orderItems[i].OrderID)
            {
                return DataSource.orderItems[i];
            }
        }
        throw new Exception("Order Item Not Found");
    }

    public OrderItem[] getItemsByOrder(Order order)
    {
        int sizeOfNew = 0;
        for (int i = 0; i<DataSource.Config.currentSizeOrderItem; i++)
        {
            if(DataSource.orderItems[i].ID == order.ID)
            {
                sizeOfNew++;
            }
        }
        OrderItem[] specificItems = new OrderItem[sizeOfNew];
        int counter = 0;
        for (int i = 0; i < DataSource.Config.currentSizeOrderItem; i++)
        {
            if (DataSource.orderItems[i].ID == order.ID)
            {
                specificItems[counter++] = DataSource.orderItems[i];
            }
        }

        if (sizeOfNew == 0)
        {
            throw new Exception("Order Not Found");
        }
        return specificItems;
    }
    public OrderItem[] getAllItems()
    {
        OrderItem[] allItems = new OrderItem[DataSource.Config.currentSizeOrderItem];
        for(int i = 0; i < DataSource.Config.currentSizeOrderItem; i++)
        {
            allItems[i] = DataSource.orderItems[i];
        }
        return allItems;
    }

    public void deleteItem(int id)
    {
        for(int i = 0; i < DataSource.Config.currentSizeOrderItem; i++)
        {
            if(id == DataSource.orderItems[i].ID)
            {
                DataSource.orderItems[i].ID = 0;
                return;
            }
        }
        throw new Exception("Order Item Not Found");
    }

    public void updateItem (OrderItem updatedItem)
    {
        for(int j = 0; j < DataSource.Config.currentSizeOrderItem; j++)
        {
            if(updatedItem.ID == DataSource.orderItems[j].ID)
            {
                DataSource.orderItems[j] = updatedItem;
                return;
            }
        }
        throw new Exception("Order Item Not Found");
    }


}