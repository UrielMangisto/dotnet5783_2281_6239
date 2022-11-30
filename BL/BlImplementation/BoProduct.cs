


internal class BoProduct:BlApi.IProduct
{
    private BlApi.IDal dal = new Dal.DalList();
    public IEnumerable<ProductForList> GetProducts()
    {
        List<DO.Product> products = new List<DO.Product>();   
        products = dal.Product.GetProducts();

        List<BO.ProductForList> productsList = new List<BO.ProductForList>();

        foreach(var product in products)
        {
            BO.ProductForList productList = new BO.ProductForList();
            productList.Id = product.Id;
            productList.Name = product.Name;    
            productList.Price = product.Price;  
            productList.Category = product.Category;
            productsList.Add(product);
        }
        return productsList;
    }

    public BO.Product ProductDetailsM(int id)
    {
        if(id>0)
        {

        }
    }

    public BO.ProductItem ProductDetailsC(int id, Cart cart)
    {

    }


}
