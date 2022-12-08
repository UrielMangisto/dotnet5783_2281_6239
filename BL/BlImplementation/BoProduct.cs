using BlApi;
using Dal;
using DalApi;
using BO;
namespace BlImplementation;
/// <summary>
/// the implementation of the dProduct
/// </summary>
public class BoProduct : BlApi.IProduct
{
    private IDal dal = new Dal.DalList();
    /// <summary>
    /// returns list of the dProducts
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public IEnumerable<BO.ProductForList> GetProductList()
    {
        List<DO.Product> dalProducts = new List<DO.Product>();
        dalProducts = (List<DO.Product>)dal.Product.GetAll();

        List<BO.ProductForList> blProducts = new List<BO.ProductForList>();

        foreach(var dalProduct in dalProducts)
        {
            try
            {
                BO.ProductForList blProduct = new BO.ProductForList();
                blProduct.Id = dalProduct.ID;
                blProduct.Name = dalProduct.Name;
                blProduct.Price = dalProduct.Price;
                blProduct.Category = (BO.Enums.Category)dalProduct.Category;
                blProducts.Add(blProduct);
            }
            catch
            {
                throw new BO.alreadyexist();
            }
        }
        return blProducts;
    }
    /// <summary>
    /// returns dProduct for manager
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public BO.Product ProductDetailsForManager(int id)
    {
        try
        {
            if (id >= 0)
            {
                DO.Product DProduct = new DO.Product();
                BO.Product? BProduct = new BO.Product();

                DProduct = dal.Product.Get(id);

                BProduct.ID = DProduct.ID;
                BProduct.Name = DProduct.Name;
                BProduct.Price = DProduct.Price;
                BProduct.Category = DProduct.Category;
                BProduct.InStock = DProduct.InStock;
                return BProduct;
            }
            else
            {
                throw new BO.NotvalidException();
            }
        }
        catch(Exception)
        {
            throw new BO.NotvalidException();
        }
    }

    /// <summary>
    /// returns dProduct for costumer
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cart"></param>
    /// <returns></returns>
    public ProductItem ProductDetailsForCostumer(int id, Cart cart)
    {
        if (cart.OrderItems == null)
        {
            throw new BO.NotvalidException();
        }
        if (id >= 0)
        {
            DO.Product dProduct = new DO.Product();
            dProduct = dal.Product.Get(id);
            BO.ProductItem pItem = new BO.ProductItem();
            pItem.Id = dProduct.ID;
            pItem.Name = dProduct.Name;
            pItem.Price = dProduct.Price;
            pItem.Category = (BO.Enums.Category)dProduct.Category;
            if (dProduct.InStock > 0)
            {
                pItem.InStock = true;
            }
            else
            {
                pItem.InStock = false;
            }
            pItem.Amount = 0;
            foreach (BO.OrderItem orderItem in cart.OrderItems)
            {
                if (orderItem.Id == pItem.Id)
                    pItem.Amount += orderItem.Amount;
            }
            return pItem;
        }
        throw new NotFoundException();
    }

    /// <summary>
    /// adding a new bProduct
    /// </summary>
    /// <param name="bProduct"></param>
    /// <exception cref="Exception"></exception>
    public void Add(BO.Product bProduct)
    {
        if (bProduct.ID < 0)
            throw new Exception();
        if (bProduct.Name == null) 
            throw new Exception();
        if (bProduct.Price <= 0) 
            throw new Exception();
        if (bProduct.InStock < 0)
            throw new Exception();

        DO.Product dProduct = new DO.Product();
        dProduct.ID = (int)bProduct.ID;
        dProduct.Name = bProduct.Name;
        dProduct.Price = bProduct.Price;
        dProduct.Category = (DO.Category)bProduct.Category;
        dProduct.InStock = bProduct.InStock;

        dal.Product.Add(dProduct);
    }


    /// <summary>
    /// deleting bProduct from the list
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="Exception"></exception>
    public void Delete(int id)
    {
        int i = 0;
        List<DO.Product> dProducts = new List<DO.Product>();
        dProducts = (List<DO.Product>)dal.Product.GetAll();
        foreach (DO.Product dProduct in dProducts)
        {
            if (id == dProduct.ID)
            {
                break;
            }
            i++;
        }
        if (i == dProducts.Count)
        {
            throw new NotFoundException();
        }
        dal.Product.Delete(dal.Product.Get(id));
    }


    /// <summary>
    /// updating some bProduct from the list
    /// </summary>
    /// <param name="bProduct"></param>
    /// <exception cref="Exception"></exception>
    public void Update(BO.Product bProduct)
    {
        if (bProduct.ID < 0)
            throw new Exception();
        if (bProduct.Name == null)
            throw new Exception();
        if (bProduct.Price <= 0)
            throw new Exception();
        if (bProduct.InStock < 0)
            throw new Exception();

        DO.Product dProduct = new DO.Product();
        //exceptions
        try
        {
            dProduct.ID = (int)bProduct.ID;
            dProduct.Name = bProduct.Name;
            dProduct.Price = bProduct.Price;
            dProduct.Category = (DO.Category)bProduct.Category;
            dProduct.InStock = bProduct.InStock;
            dal.Product.Update(dProduct);
        }
        catch (Exception)
        {
            throw new NotFoundException();
        }

    }

    /// <summary>
    /// Catalog Request From Costumer
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public IEnumerable<BO.ProductItem> CatalogRequest()
    {
        List<DO.Product> products = new List<DO.Product>();
        products = (List<DO.Product>)dal.Product.GetAll();

        List<BO.ProductItem> productItems = new List<BO.ProductItem>();

        foreach(var dProduct in products)
        {
            try
            {
                BO.ProductItem productItem = new BO.ProductItem();
                productItem.Id = dProduct.ID;
                productItem.Name = dProduct.Name;
                productItem.Price = dProduct.Price;
                productItem.Category = (BO.Enums.Category)dProduct.Category;
                productItem.InStock = dProduct.InStock > 0;
                productItems.Add(productItem);
            }
            catch
            {
                throw new Exception();
            }
        }
        return productItems;
    }

    /// <summary>
    /// Requests Details Of Products From The Costumer
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public ProductItem RequestDetailsFromCostumer(int id)
    {
        List<DO.Product> Dproducts = new List<DO.Product>();
        Dproducts = (List<DO.Product>)dal.Product.GetAll();

        BO.ProductItem BproductItem = new BO.ProductItem();

        try
        {
            foreach (var dProduct in Dproducts)
            {
                if (dProduct.ID == id)
                {
                    BproductItem.Name = dProduct.Name;
                    BproductItem.Price = dProduct.Price;
                    BproductItem.Category = (BO.Enums.Category)dProduct.Category;
                    BproductItem.Amount = dProduct.InStock;
                    return BproductItem;
                }
            }
            throw new NotFoundException();
        }
        catch
        {
            throw new NotFoundException();
        }

    }

}
