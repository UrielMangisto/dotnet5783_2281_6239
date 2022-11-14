using DO;

namespace Dal;

internal class DalProduct
{
    public void add(Product newProduct)
    {
        DataSource.addProductsToTheArray();
    }
    public void delete(Product delProduct)
      {
        bool isdelate = false;
        for(int i=0;i < DataSource.products.Length&&!isdelate;i++)
        {
            if(delProduct.ID==DataSource.products[i].ID)
            {
                isdelate = true;
                DataSource.products[i].ID = 0;//if id=0 its deleated
            }
        }
    }
    public void update()
    {

    }
    public void get()
    {

    }

}

