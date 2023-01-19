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
    public int Add(Product entity)
    {
        int id = 0;
        bool exict = true;
        while (exict)
        {
            id = entity.ID;
            try
            {
                Get(id);
            }
            catch(Exception)
            {
                exict = false;

            }
        }
        entity.ID = id;
        DataSource.products.Add(entity);
        return id;
    }
    /*public void Delete(int id)
{
if (_ds._students.RemoveAll(p => p?.ID == id) == 0)
throw new DoesNotExistException("Can't delete non-existing student");
}*/
    public void Delete(Product entity)
    {
         
        foreach (var p in DataSource.products)
        {
            if (p?.ID == entity.ID)
            {
                DataSource.products.Remove(p);
                return;
            }
        }
        throw new Exception("Product Not Found");
    }

    public void Update(Product entity)
    {
        int count = 0;
        foreach(var p in DataSource.products)
        {
            if(p?.ID == entity.ID)
            {
                DataSource.products[count] = entity;
                return;
            }
            count++;
        }
     

        throw new ("Product Not Found");
    }

    public Product? Get(int id)
    {
        foreach (var p in DataSource.products)
        {
           if (p?.ID == id)
                return p;
        }
        throw new NotFoundException("Product Not Found");
    }
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

