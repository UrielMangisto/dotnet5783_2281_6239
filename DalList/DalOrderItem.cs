﻿using DO;
using DalApi;
namespace Dal;

public class DalOrderitem :  IProduct
{
    OrderItem Add(OrderItem entity)
    {
        entity.ID = DataSource.Config.NextOrderItemId;
        DataSource.orderItems.Add(entity);
        return entity;
    }

    OrderItem Get(OrderItem entity)
    {
        foreach (var p in DataSource.orderItems)
        {
            if (entity.ID == p.ID)
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
    IEnumerable<OrderItem> GetAll()
    {
        List<OrderItem> allItems = new List<OrderItem>() { };
        foreach (var p in DataSource.orderItems)
        {
            allItems.Add(p);
        }
        return allItems;
    }

    OrderItem Delete(OrderItem entity)
    {
        foreach (var p in DataSource.orderItems)
        {
            if(entity.ID == p.ID)
            {
                DataSource.orderItems.Remove(p);
                return entity;
            }
        }
        throw new Exception("Order Item Not Found");
    }

    OrderItem Update(OrderItem entity)
    {
        int count = 0;
        foreach (var p in DataSource.orderItems)
        {
            count++;
            if(entity.ID == p.ID)
            {
                DataSource.orderItems.Insert(count, entity);
                return entity;
            }
        }
        
        throw new Exception("Order Item Not Found");

    }

}