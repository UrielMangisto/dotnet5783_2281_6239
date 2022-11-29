using DO;
namespace Dal;

public class DalProduct
{
    public void addNewProduct(Product newProduct)
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
        newProduct.ID = id;
        DataSource.products.Add(newProduct);
    }

    public void deleteProduct(int id)
    {
        foreach (var p in DataSource.products)
        {
            if (p.ID == id)
            {
                DataSource.products.Remove(p);
                return;
            }
        }
        throw new Exception("Product Not Found");
    }

    public void update(Product updatedProduct)
    {
        foreach(var p in DataSource.products)
        {
            if(p.ID == updatedProduct.ID)
            {
                p = updatedProduct;

                return;
            }
        }
        throw new Exception("Product Not Found");
    }

    public Product getProduct(int id)
    {
        for(int i=0;i<DataSource.Config.currentSizeProduct ;i++)
        {
           if (DataSource.products[i].ID == id)
                return DataSource.products[i];
        }

        throw new Exception("Product Not Found");
    }

    public List<Product> getAllProducts()
    {
        List<Product> products = new List<Product>;
        for(var p in )
        {
            products[i] = DataSource.products[i];
        }
        return products;
    }
}

