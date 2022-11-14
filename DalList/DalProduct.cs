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
        DataSource.products[DataSource.Config.currentSizeProduct++] = newProduct;
    }

    public void deleteProduct(int id)
    {
        for(int i=0;i < DataSource.Config.currentSizeProduct; i++)
        {
            if (DataSource.products[i].ID == id)
            {
                DataSource.products[i].ID = 0; //in order to delete it
                return;
            }
        }
        throw new Exception("Product Not Found");
    }

    public void update(Product updatedProduct)
    {
        for(int i=0;i<DataSource.Config.currentSizeProduct;i++)
        {
            if(DataSource.products[i].ID== updatedProduct.ID)
            {
                DataSource.products[i] = updatedProduct;
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

    public Product[] getAll()
    {
        Product[] products = new Product[DataSource.Config.currentSizeProduct];
        for(int i=0;i<DataSource.Config.currentSizeProduct;i++)
        {
            products[i] = DataSource.products[i];
        }
        return products;
    }
}

