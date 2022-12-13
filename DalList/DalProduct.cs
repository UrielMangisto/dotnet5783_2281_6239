using DO;
using DalApi;
namespace Dal;

/// <summary>
/// Implementation of the CRUB functions on Product
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
            count++;
            if(p?.ID == entity.ID)
            {
                DataSource.products.Insert(count, entity);
                return;
            }
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
        foreach (var p in DataSource.products)
        {
            if (selector(p) == null)
                throw new mayBeNullException();
            if (selector(p))
                return p;
        }
        throw new Exception("Product Not Found");
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

