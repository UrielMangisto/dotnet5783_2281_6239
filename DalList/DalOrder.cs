using DO;
using DalApi;
namespace Dal;
/// <summary>
/// Implementation of the CRUB functions on Order
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
            if (p.ID == entity.ID)
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
            count++;    
            if (p.ID == entity.ID)
            {
                DataSource.orders.Insert(count, entity);
                return;
            }
        }
        throw new NotFoundException();
    }
    public Order Get(int entity)
    {
        
        foreach (var p in DataSource.orders)
        {
            if (p.ID == entity)
                return p;
        }

        throw new NotFoundException();
    }
    public IEnumerable<Order> GetAll()
    {
        List <Order> orders = new List <Order> ();
        foreach (var p in DataSource.orders)
        {
            orders.Add(p);
        }
        return orders;
    }
        

}