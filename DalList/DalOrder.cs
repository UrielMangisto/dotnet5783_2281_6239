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
                DataSource.orders.Remove(p); //in Order to delete it
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
       
        if (selector==null)
        {      
            var orders = DataSource.orders.Select(Order => Order );
            return orders;
        }
        else
        {
            var orders = DataSource.orders.Where(selector);
            return orders;
        }
    }
}