using DO;
using DalApi;
using static DalApi.ICrud<DO.Order>;

namespace Dal;
/// <summary>
/// Implementation of the CRUD functions on Order
/// </summary>
public class DalOrder : IOrder
{
    public int Add(Order entity)
    {
        entity.ID = DataSource.Config.NextOrderId;
        DataSource.orders.Add(entity);
        return entity.ID;
    }

    public void Delete(Order entity)
    {
        foreach (var p in DataSource.orders)
        {
            if (p?.ID == entity.ID)
            {
                DataSource.orders.Remove(p); //in order to delete it
                return;
            }
        }
        throw new NotFoundException();
    }
    public void Update(Order entity)
    {
        int count = 0;
        foreach (var p in DataSource.orders)
        {   
            if (p?.ID == entity.ID)
            {
                DataSource.orders[count] = entity;
                return;
            }
            count++;
        }
        throw new NotFoundException();
    }
    public Order? Get(int entity)
    {
        
        foreach (var p in DataSource.orders)
        {
            if (p?.ID == entity)
                return p;
        }
        throw new NotFoundException();
    }
    public Order? Get(Func<Order?, bool>? selector)
    {
        if (selector == null)
        {
            throw new mayBeNullException();
        }
        else
        {
            foreach (var p in DataSource.orders)
            {
                if (selector(p))
                    return p;
            }
        }
        throw new NotFoundException();
    }
    public IEnumerable<Order?> GetAll(Func<Order?, bool>? selector = null)
    {
        List<Order?> orders = new List<Order?>();

        if (selector==null)
        { 
            foreach (var p in DataSource.orders)
            {
                orders.Add(p);
            }
            return orders;
        }
        else
        {
            foreach (var p in DataSource.orders)
            {
                if (selector(p))
                {
                    orders.Add(p);
                }
                
            }
            return orders;
        }
    }
}