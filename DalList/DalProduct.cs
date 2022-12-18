using DO;
using DalApi;
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
            id = DataSource.Randomally.Next(100000, 999999);
            try
            {
                Get(id);
            }
            catch(Exception exception)
            {
                exict = false;

            }
        }
        entity.ID = id;
        DataSource.products.Add(entity);
        return id;
    }

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
        throw new Exception("Product Not Found");
    }

    public Product? Get(int id)
    {
        foreach (var p in DataSource.products)
        {
           if (p?.ID == id)
                return p;
        }
        throw new Exception("Product Not Found");
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
            List<Product?> products = new List<Product?>();
            foreach (var p in DataSource.products)
            {
                products.Add(p);
            }
            return products;
        }
        else
        {
            List<Product?> products = new List<Product?>();
            foreach (var p in DataSource.products)
            {
                if(selector(p))
                {
                    products.Add(p);
                }
            }
            return products;
        }
        
    }
}

