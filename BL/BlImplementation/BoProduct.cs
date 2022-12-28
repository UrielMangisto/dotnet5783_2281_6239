using BlApi;

using DalApi;
using BO;
using DO;
using Dal;

namespace BlImplementation;
/// <summary>
/// the implementation of the dProduct
/// </summary>
/// 

public class BoProduct : BlApi.IProduct
{
    BO.ProductForList changeToBo1(DO.Product? dalProduct)
    {
        BO.ProductForList blProduct = new BO.ProductForList();
        blProduct.Id = dalProduct?.ID ?? throw new BO.mayBeNullException();
        blProduct.Name = dalProduct?.Name ?? throw new BO.mayBeNullException();
        blProduct.Price = dalProduct?.Price ?? throw new BO.mayBeNullException();
        blProduct.Category = (BO.Enums.Category)(dalProduct?.Category ?? throw new BO.mayBeNullException());
        return blProduct;
    }
    BO.ProductItem changeToBo2(DO.Product? dProduct)
    {
        BO.ProductItem productItem = new BO.ProductItem();
        productItem.Id = dProduct?.ID ?? throw new BO.mayBeNullException();
        productItem.Name = dProduct?.Name;
        productItem.Price = dProduct?.Price ?? throw new BO.mayBeNullException();
        productItem.Category = (BO.Enums.Category)(dProduct?.Category ?? throw new BO.mayBeNullException());
        productItem.InStock = dProduct?.InStock > 0;
        return productItem;
    }
    DalApi.IDal? dal = DalApi.Factory.Get();

    /// <summary>
    /// returns list of the dProducts
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public IEnumerable<BO.ProductForList?> GetProductList()
    {
        
        List<DO.Product?> dalProducts = new List<DO.Product?>();
        dalProducts = dal.Product.GetAll().ToList();
      
        try
        {
            
            var blProducts = dalProducts.Select(Product => changeToBo1(Product));
            return blProducts;

        }
        catch
        {
            throw new BO.NotFoundException();
        }
        
    }
    /// <summary>
    /// Get Product List By Term
    /// </summary>
    /// <param name="selector"></param>
    /// <returns></returns>
    /// <exception cref="BO.NotFoundException"></exception>
    /// 
    public IEnumerable<ProductForList?> GetProductsByTerm(Func<BO.ProductForList?, bool>? selector = null)
    {
        List<DO.Product?> dalProducts = new List<DO.Product?>();
        dalProducts = dal.Product.GetAll().ToList();

       
        if (selector != null)
        {
            try
            {
                var blProducts = dalProducts.Select(Product => changeToBo1(Product)).Where(selector);
                return blProducts;

            }
            catch
            {
                throw new BO.NotFoundException();
            }
        }
        else
        {
             var blProducts = dalProducts.Select (Product => changeToBo1(Product));
            return blProducts;
            
        }
        
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
                DO.Product? DProduct = new DO.Product?();
                BO.Product BProduct = new BO.Product();

                DProduct = dal.Product.Get(id);

                BProduct.ID = DProduct?.ID ?? throw new BO.mayBeNullException();
                BProduct.Name = DProduct?.Name;
                BProduct.Price = DProduct?.Price ?? throw new BO.mayBeNullException();
                BProduct.Category = (BO.Enums.Category1)(DProduct?.Category ?? throw new BO.mayBeNullException());
                BProduct.InStock = DProduct?.InStock ?? throw new BO.mayBeNullException();
                return BProduct;
            }
            else
            {
                throw new BO.RequestProductFaildException();
            }
        }
        catch(Exception)
        {
            throw new BO.RequestProductFaildException();
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
            DO.Product? dProduct = new DO.Product?();
            dProduct = dal.Product.Get(id);
            BO.ProductItem pItem = new BO.ProductItem();
            pItem.Id = dProduct?.ID ?? throw new BO.mayBeNullException();
            pItem.Name = dProduct?.Name ?? throw new BO.mayBeNullException();
            pItem.Price = dProduct?.Price ?? throw new BO.mayBeNullException();
            pItem.Category = (BO.Enums.Category)(dProduct?.Category ?? throw new BO.mayBeNullException());
            if (dProduct?.InStock > 0)
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
        throw new BO.RequestProductFaildException();
    }

    /// <summary>
    /// adding a new bProduct
    /// </summary>
    /// <param name="bProduct"></param>
    /// <exception cref="Exception"></exception>
    public void Add(BO.Product bProduct)
    {
        try
        {
            if (bProduct.ID < 0)
                throw new BO.InCorrectDataException();
            if (bProduct.Name == null)
                throw new BO.InCorrectDataException();
            if (bProduct.Price <= 0)
                throw new BO.InCorrectDataException();
            if (bProduct.InStock < 0)
                throw new BO.InCorrectDataException();

            DO.Product dProduct = new DO.Product();
            dProduct.ID = (int)bProduct.ID;
            dProduct.Name = bProduct.Name;
            dProduct.Price = bProduct.Price;
            dProduct.Category = (DO.Category)bProduct.Category;
            dProduct.InStock = bProduct.InStock;

            dal.Product.Add(dProduct);
        }
        catch(BO.AlreadyExistException)
        {
            throw new BO.AlreadyExistException();
        }
        catch(BO.InCorrectDataException)
        {
            throw new BO.InCorrectDataException();
        }
        catch
        {
            throw new Exception();
        }
    }


    /// <summary>
    /// deleting bProduct from the list
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="Exception"></exception>
    public void Delete(int id)
    {
        try
        {
            int i = 0;
            List<DO.Product?> dProducts = new List<DO.Product?>();
            dProducts = dal.Product.GetAll().ToList();
            foreach (DO.Product? dProduct in dProducts)
            {
                if (id == dProduct?.ID)
                {
                    break;
                }
                i++;
            }
            List<DO.OrderItem?> DoOrderitems = new List<DO.OrderItem?>();
            DoOrderitems = dal.OrderItem.GetAll().ToList();
            foreach (var dor in DoOrderitems)
            {
                if (id == dor?.ProductID)
                    throw new BO.ProductExistInOrderException();
            }
            if (i == dProducts.Count)
            {
                throw new BO.NotFoundException();
            }
            dal.Product.Delete(dal.Product.Get(id) ?? throw new BO.mayBeNullException());
        }
        catch (BO.NotFoundException)
        {
            throw new BO.NotFoundException();
        }
        catch (BO.InCorrectDataException)
        {
            throw new BO.InCorrectDataException();
        }
        catch
        {
            throw new Exception();
        }
    }


    /// <summary>
    /// updating some bProduct from the list
    /// </summary>
    /// <param name="bProduct"></param>
    /// <exception cref="Exception"></exception>
    public void Update(BO.Product bProduct)
    {
        if (bProduct.ID < 0)
            throw new BO.InCorrectDataException();
        if (bProduct.Name == null)
            throw new BO.InCorrectDataException();
        if (bProduct.Price <= 0)
            throw new BO.InCorrectDataException();
        if (bProduct.InStock < 0)
            throw new BO.InCorrectDataException();

        DO.Product dProduct = new DO.Product();
        //exceptions
        
         dProduct.ID = (int)bProduct.ID;
         dProduct.Name = bProduct.Name;
         dProduct.Price = bProduct.Price;
         dProduct.Category = (DO.Category)bProduct.Category;
         dProduct.InStock = bProduct.InStock;
         dal.Product.Update(dProduct);
       

    }

    /// <summary>
    /// Catalog Request From Costumer
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public IEnumerable<ProductItem?> CatalogRequest()
    {
        List<DO.Product?> products = new List<DO.Product?>();
        products = dal.Product.GetAll().ToList();
        //var productItems = products.Select(Product => changeToBo2(Product));
        products.OrderBy(p => p?.Name).ThenBy(p => p?.ID);
        var productItems = from p in products
                           
                           select changeToBo2(p);
                           /*
                                   List<BO.ProductItem> productItems = new List<BO.ProductItem>();

                                   foreach(var dProduct in products)
                                   {
                                       BO.ProductItem productItem = new BO.ProductItem();
                                       productItem.Id = dProduct?.ID ?? throw new BO.mayBeNullException();
                                       productItem.Name = dProduct?.Name;
                                       productItem.Price = dProduct?.Price ?? throw new BO.mayBeNullException();
                                       productItem.Category = (BO.Enums.Category)(dProduct?.Category ?? throw new BO.mayBeNullException());
                                       productItem.InStock = dProduct?.InStock > 0;
                                       productItems.Add(productItem);
                                   }
                           */
        return (IEnumerable<ProductItem?>)productItems;
    }

    /// <summary>
    /// Requests Details Of Products From The Costumer
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public ProductItem RequestDetailsFromCostumer(int id)
    {
        List<DO.Product?> Dproducts = new List<DO.Product?>();
        Dproducts = dal.Product.GetAll().ToList();

        BO.ProductItem BproductItem = new BO.ProductItem();

        
        foreach (var dProduct in Dproducts)
        {
            if (dProduct?.ID == id)
            {
                BproductItem.Name = dProduct?.Name;
                BproductItem.Price = dProduct?.Price ?? throw new BO.mayBeNullException();
                BproductItem.Category = (BO.Enums.Category)(dProduct?.Category ?? throw new BO.mayBeNullException());
                BproductItem.Amount = dProduct?.InStock ?? throw new BO.mayBeNullException();
                return BproductItem;
            }
        }
        throw new BO.NotFoundException();
    }
        

}
