using System.Runtime.CompilerServices;
using DO;
using DalApi;
using System;

namespace Dal;
/// <summary>
/// Implementation of the CRUD functions on Order item
/// </summary>
public class DalOrderItem :  IOrderItem
{
    [MethodImpl(MethodImplOptions.Synchronized)]

    public int Add(OrderItem entity)
    {
        entity.orderItemID = DataSource.Config.NextOrderItemId;
        DataSource.orderItems.Add(entity);
        return entity.orderItemID;
    }

    [MethodImpl(MethodImplOptions.Synchronized)]

    public OrderItem? Get(int id)
    {
        foreach (var p in DataSource.orderItems)
        {
            if (p?.orderItemID==id)
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
    
    [MethodImpl(MethodImplOptions.Synchronized)]
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
    [MethodImpl(MethodImplOptions.Synchronized)]
    public OrderItem? specificItemGet(int idOfProduct, int idOfOrder)
    {
        foreach (var p in DataSource.orderItems)
        {
            if (idOfProduct == p?.ProductID && idOfOrder == p?.OrderID)
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
    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<OrderItem?> GetItemsByOrder(int orderID, Func<OrderItem?, bool>? selector=null)
    {
        
        bool chacking(OrderItem? orderItem)
        {
            //if (orderItem.Value.orderID == orderID)
            if (orderItem.Value.OrderID == orderID)
            {
                return true;
            }
            return false;
        }
        if (selector == null)
        {
            var specificItems = DataSource.orderItems.Where(chacking);
            if (specificItems == null)
            {
                throw new NotFoundException();
            }
            return specificItems;
        }
        else
        {
           
            var specificItems = DataSource.orderItems.Where(chacking).Where(selector);
            if (specificItems == null)
            {
                throw new NotFoundException();
            }
            return specificItems;
        }
        
    }
       
    /// <summary>
    /// returns an array of all products
    /// </summary>
    /// <param name="selector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<OrderItem?> GetAll(Func<OrderItem?, bool>? selector=null)
    {
        if(selector == null)
        {
            var a = from di in DataSource.orderItems
                    select new
                    {
                        di = di

                    };
             var allItems = DataSource.orderItems.Select(OrderItem => OrderItem);
            return allItems;
        }
        else
        {  
            var allItems = DataSource.orderItems.Where(selector);
            return allItems;
        }
        
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Delete(OrderItem entity)
    {
        foreach (var p in DataSource.orderItems)
        {
            if(entity.orderItemID == p?.orderItemID)
            {
                DataSource.orderItems.Remove(p);
                return;
            }
        }
        throw new NotFoundException();
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Update(OrderItem entity)
    {
        int count = 0;
        foreach (var p in DataSource.orderItems)
        {
            if(entity.orderItemID == p?.orderItemID)
            {
                DataSource.orderItems[count] = entity;
                return;
            }
            count++;
        }
        
        throw new NotFoundException();

    }

}