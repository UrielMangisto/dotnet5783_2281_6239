﻿using System.Runtime.CompilerServices;
using DO;
using DalApi;
using System.Linq;
using System;

namespace Dal;

/// <summary>
/// Implementation of the CRUD functions on Product
/// </summary>
public class DalProduct: IProduct
{
    [MethodImpl(MethodImplOptions.Synchronized)]
    public int Add(Product entity)
    {
        int id = 0;
        bool exict = true;
        while (exict)
        {
            id = entity.productID;
            try
            {
                Get(id);
            }
            catch(Exception)
            {
                exict = false;

            }
        }
        entity.productID = id;
        DataSource.products.Add(entity);
        return id;
    }
    /*public void Delete(int id)
{
if (_ds._students.RemoveAll(p => p?.orderID == id) == 0)
throw new DoesNotExistException("Can't delete non-existing student");
}*/
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Delete(Product entity)
    {
         
        foreach (var p in DataSource.products)
        {
            if (p?.productID == entity.productID)
            {
                DataSource.products.Remove(p);
                return;
            }
        }
        throw new DO.NotFoundException("Product Not Found");
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Update(Product entity)
    {
        int count = 0;
        foreach(var p in DataSource.products)
        {
            if(p?.productID == entity.productID)
            {
                DataSource.products[count] = entity;
                return;
            }
            count++;
        }
     

        throw new ("Product Not Found");
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public Product? Get(int id)
    {
        foreach (var p in DataSource.products)
        {
           if (p?.productID == id)
                return p;
        }
        throw new NotFoundException("Product Not Found");
    }
    [MethodImpl(MethodImplOptions.Synchronized)]
    public Product? Get(Func<Product?, bool>? selector)
    {
        if (selector == null)
        {
            throw new mayBeNullException();
        }
        else
        {
            foreach (var p in DataSource.products)
            {
                if (selector(p))
                    return p;
            }
        }
        throw new NotFoundException();
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<Product?> GetAll(Func<Product?, bool>? selector=null)
    {
        if(selector==null)
        {
            var products = from dp in DataSource.products 
                           select (dp);
            return products;
        }
        else
        {
            var a = from dp in DataSource.products
                    group new { dp } by dp?.Name;


            var products = DataSource.products.Where(selector);
            return products;
        }
        
    }
}

