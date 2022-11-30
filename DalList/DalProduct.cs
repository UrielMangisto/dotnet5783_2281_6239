using DO;
using DalApi;
namespace Dal;


internal class DalProduct: IProduct
{
    Product Add (Product entity)
    {
        int id = 0;
        bool exict = true;
        while (exict)
        {
            id = DataSource.Randomally.Next(100000, 999999);
            try
            {
                getProduct(id);
            }
            catch(Exception exception)
            {
                exict = false;
            }
        }
        entity.ID = id;
        DataSource.products.Add(entity);
    }

    Product Delete(Product entity)
    {
        foreach (var p in DataSource.products)
        {
            if (p.ID == entity.ID)
            {
                DataSource.products.Remove(p);
                return entity;
            }
        }
        throw new Exception("Product Not Found");
    }

    Product Update(Product entity)
    {
        int count = 0;
        foreach(var p in DataSource.products)
        {
            count++;
            if(p.ID == entity.ID)
            {
                DataSource.products.Insert(count, entity);
                return entity;
            }
        }
        throw new Exception("Product Not Found");
    }

    Product Get(int id)
    {
        foreach (var p in DataSource.products)
        {
           if (p.ID == id)
                return p;
        }

        throw new Exception("Product Not Found");
    }

    IEnumerable<Product> GetAll()
    {
        List<Product> products = new List<Product>();
        foreach (var p in DataSource.products)
        {
            products.Add (p);
        }
        return products;
    }
}

