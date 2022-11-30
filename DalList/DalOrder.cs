using DO;
using DalApi;
namespace Dal;

public class DalOrder : IProduct
{
    Order Add(Order entity)
    {
        entity.ID = DataSource.Config.NextOrderId;
        DataSource.orders.Add(entity);
        return entity;
    }
    Order Delete(Order entity)
    {
        foreach (var p in DataSource.orders)
        {
            if (p.ID == entity.ID)
            {
                DataSource.orders.Remove(p); //in order to delete it
                return entity;
            }
        }
        throw new Exception("Order Not Found");
    }
    Order Update(Order entity)
    {
        int count = 0;
        foreach (var p in DataSource.orders)
        {
            count++;    
            if (p.ID == entity.ID)
            {
                DataSource.orders.Insert(count, entity);
                return entity;
            }
        }
        throw new Exception("Order Not Found");
    }
    Order Get(int entity)
    {
        
        foreach (var p in DataSource.orders)
        {
            if (p.ID == entity)
                return p;
        }

        throw new Exception("order Not Found");
    }
    IEnumerable<Order> GetAll()
    {
        List <Order> orders = new List <Order> ();
        foreach (var p in DataSource.orders)
        {
            orders.Add(p);
        }
        return orders;
    }
        

}