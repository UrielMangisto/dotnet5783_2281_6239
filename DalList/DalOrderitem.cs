using DO;
using DalApi;
namespace Dal;
/// <summary>
/// Implementation of the CRUD functions on Order item
/// </summary>
public class DalOrderitem :  IOrderItem
{
    public int Add(OrderItem entity)
    {
        entity.ID = DataSource.Config.NextOrderItemId;
        DataSource.orderItems.Add(entity);
        return entity.ID;
    }

    public OrderItem? Get(int id)
    {
        foreach (var p in DataSource.orderItems)
        {
            if (p?.ID==id)
            {
                return p;
            }
        }
        throw new NotFoundException();
    }
    /// <summary>
    /// get Order item by the id of the Product and the Order
    /// <param name="idOfProduct"></param>
    /// <param name="idOfOrder"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    /// </summary>
    /// 
    public OrderItem? Get(Func<OrderItem?, bool>? selector)
    {
        if (selector == null)
        {
            throw new mayBeNullException();
        }
        else
        {
            foreach (var p in DataSource.orderItems)
            {
                if (selector(p))
                    return p;
            }
        }
        throw new NotFoundException();
    }
    public OrderItem? specificItemGet(int idOfProduct, int idOfOrder)
    {
        foreach (var p in DataSource.orderItems)
        {
            if (idOfProduct == p?.ID && idOfOrder == p?.OrderID)
            {
                return p;
            }
        }
        throw new NotFoundException();
    }
    /// <summary>
    /// get Order items by the id of the Order
    /// </summary>
    /// <param name="orderID"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public IEnumerable<OrderItem?> GetItemsByOrder(int orderID, Func<OrderItem?, bool>? selector=null)
    {
        List<OrderItem?> specificItems = new List<OrderItem?>() { };
        if (selector == null)
        {
            
            foreach (var p in DataSource.orderItems)
            {
                if (p?.ID == orderID)
                {
                    specificItems.Add(p);
                }
            }

            if (specificItems == null)
            {
                throw new NotFoundException();
            }
        }
        else
        {
            foreach (var p in DataSource.orderItems)
            {
                if ((p?.ID == orderID) && ( selector(p)))
                {
                    specificItems.Add(p);
                }
            }
        }
        return specificItems;
    }
       
    //returns an array of all products
    public IEnumerable<OrderItem?> GetAll(Func<OrderItem?, bool>? selector=null)
    {
        if(selector == null)
        {
            List<OrderItem?> allItems = new List<OrderItem?>() { };
            foreach (var p in DataSource.orderItems)
            {
                allItems.Add(p);
            }
            return allItems;
        }
        else
        {
            List<OrderItem?> allItems = new List<OrderItem?>() { };
            foreach (var p in DataSource.orderItems)
            {
                if (selector(p))
                {
                    allItems.Add(p);
                }

            }
            return allItems;
        }
        
    }

    public void Delete(OrderItem entity)
    {
        foreach (var p in DataSource.orderItems)
        {
            if(entity.ID == p?.ID)
            {
                DataSource.orderItems.Remove(p);
                return;
            }
        }
        throw new NotFoundException();
    }

    public void Update(OrderItem entity)
    {
        int count = 0;
        foreach (var p in DataSource.orderItems)
        {
            count++;
            if(entity.ID == p?.ID)
            {
                DataSource.orderItems.Insert(count, entity);
                return;
            }
        }
        
        throw new NotFoundException();

    }

}