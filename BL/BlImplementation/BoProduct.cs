using System.Runtime.CompilerServices;
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
    [MethodImpl(MethodImplOptions.Synchronized)]

    BO.ProductForList changeToBo1(DO.Product? dalProduct)
    {
        BO.ProductForList blProduct = new BO.ProductForList();
        blProduct.Id = dalProduct?.productID ?? throw new DO.mayBeNullException();
        blProduct.Name = dalProduct?.Name ?? throw new DO.mayBeNullException();
        blProduct.Price = dalProduct?.Price ?? throw new DO.mayBeNullException();
        blProduct.Category = (BO.Enums.Category)(dalProduct?.Category ?? throw new DO.mayBeNullException());
        return blProduct;
    }
    [MethodImpl(MethodImplOptions.Synchronized)]
    BO.ProductItem changeToBo2(DO.Product? dProduct)
    {
        BO.ProductItem productItem = new BO.ProductItem();
        productItem.Id = dProduct?.productID ?? throw new BO.mayBeNullException();
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
     [MethodImpl(MethodImplOptions.Synchronized)]

    public IEnumerable<BO.ProductForList?> GetProductList()
    {

        List<DO.Product?> dalProducts = new List<DO.Product?>();
        dalProducts = dal.Product.GetAll().ToList();

        try
        {

            var blProducts = dalProducts.Select(Product => changeToBo1(Product));
            return blProducts;

        }
        catch (DO.NotFoundException)
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
    [MethodImpl(MethodImplOptions.Synchronized)]

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
            catch (DO.NotFoundException)
            {
                throw new BO.NotFoundException();
            }
        }
        else
        {
            var blProducts = dalProducts.Select(Product => changeToBo1(Product));
            return blProducts;
        }
    }
    [MethodImpl(MethodImplOptions.Synchronized)]

    public IEnumerable<ProductItem?> GetItemsByTerm(Func<BO.ProductItem?, bool>? selector = null)
    {
        List<DO.Product?> dalProducts = new List<DO.Product?>();
        dalProducts = dal.Product.GetAll().ToList();


        if (selector != null)
        {
            try
            {
                var blProducts = dalProducts.Select(Product => changeToBo2(Product)).Where(selector);
                return blProducts;

            }
            catch (DO.NotFoundException)
            {
                throw new BO.NotFoundException();
            }
        }
        else
        {
            var blProducts = dalProducts.Select(Product => changeToBo2(Product));
            return blProducts;

        }

    }
    /// <summary>
    /// returns dProduct for manager
    /// </summary>
    /// <param name="id"></param>

    /// <returns></returns>
    [MethodImpl(MethodImplOptions.Synchronized)]

    public BO.Product ProductDetailsForManager(int id)
    {
        try
        {
            if (id >= 0)
            {
                DO.Product? DProduct = new DO.Product?();
                BO.Product BProduct = new BO.Product();

                DProduct = dal.Product.Get(id);

                BProduct.ID = DProduct?.productID ?? throw new BO.mayBeNullException();
                BProduct.Name = DProduct?.Name;
                BProduct.Price = DProduct?.Price ?? throw new BO.mayBeNullException();
                BProduct.Category = (BO.Enums.Category1)(DProduct?.Category ?? throw new BO.mayBeNullException());
                BProduct.InStock = DProduct?.InStock ?? throw new BO.mayBeNullException();
                return BProduct;
            }
            else
            {
                throw new DO.RequestProductFaildException();
            }
        }
        catch (DO.RequestProductFaildException)
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
    [MethodImpl(MethodImplOptions.Synchronized)]

    public ProductItem ProductDetailsForCostumer(int id, Cart cart)
    {
        try
        {
            if (cart.OrderItems == null)
            {
                throw new DO.NotvalidException();
            }
            if (id >= 0)
            {
                DO.Product? dProduct = new DO.Product?();
                dProduct = dal.Product.Get(id);
                BO.ProductItem pItem = new BO.ProductItem();
                pItem.Id = dProduct?.productID ?? throw new DO.mayBeNullException();
                pItem.Name = dProduct?.Name ?? throw new DO.mayBeNullException();
                pItem.Price = dProduct?.Price ?? throw new DO.mayBeNullException();
                pItem.Category = (BO.Enums.Category)(dProduct?.Category ?? throw new DO.mayBeNullException());
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
            throw new DO.RequestProductFaildException();
        }
        catch (DO.NotvalidException)
        {
            throw new BO.NotvalidException();
        }
        catch (DO.mayBeNullException)
        {
            throw new BO.mayBeNullException();
        }

    }


    /// <summary>
    /// adding a new bProduct
    /// </summary>
    /// <param name="bProduct"></param>
    /// <exception cref="Exception"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]

    public void Add(BO.Product bProduct)
    {
        try
        {
            if (bProduct.ID <= 0)
                throw new DO.InCorrectDataException();
            if (bProduct.Name == null)
                throw new DO.InCorrectDataException();
            if (bProduct.Price <= 0)
                throw new DO.InCorrectDataException();
            if (bProduct.InStock < 0)
                throw new DO.InCorrectDataException();

            DO.Product dProduct = new DO.Product();
            dProduct.productID = (int)bProduct.ID;
            dProduct.Name = bProduct.Name;
            dProduct.Price = bProduct.Price;
            dProduct.Category = (DO.Category)bProduct.Category;
            dProduct.InStock = bProduct.InStock;

            dal.Product.Add(dProduct);
        }
        catch (DO.AlreadyExistException)
        {
            throw new BO.AlreadyExistException();
        }
        catch (DO.InCorrectDataException)
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
    [MethodImpl(MethodImplOptions.Synchronized)]

    public void Delete(int id)
    {
        try
        {
            int i = 0;
            List<DO.Product?> dProducts = new List<DO.Product?>();
            dProducts = dal.Product.GetAll().ToList();
            foreach (DO.Product? dProduct in dProducts)
            {
                if (id == dProduct?.productID)
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
                    throw new DO.ProductExistInOrderException();
            }
            if (i == dProducts.Count)
            {
                throw new DO.NotFoundException();
            }
            dal.Product.Delete(dal.Product.Get(id) ?? throw new DO.mayBeNullException());
        }
        catch (DO.NotFoundException)
        {
            throw new BO.NotFoundException();
        }
        catch (DO.InCorrectDataException)
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
    [MethodImpl(MethodImplOptions.Synchronized)]

    public void Update(BO.Product bProduct)
    {
        try
        {
            if (bProduct.ID < 0)
                throw new DO.InCorrectDataException();
            if (bProduct.Name == null)
                throw new DO.InCorrectDataException();
            if (bProduct.Price <= 0)
                throw new DO.InCorrectDataException();
            if (bProduct.InStock < 0)
                throw new DO.InCorrectDataException();

            DO.Product dProduct = new DO.Product();
            //exceptions

            dProduct.productID = (int)bProduct.ID;
            dProduct.Name = bProduct.Name;
            dProduct.Price = bProduct.Price;
            dProduct.Category = (DO.Category)bProduct.Category;
            dProduct.InStock = bProduct.InStock;
            dal.Product.Update(dProduct);
        }
        catch (DO.InCorrectDataException)
        {
            throw new BO.InCorrectDataException();
        }


    }

    /// <summary>
    /// Catalog Request From Costumer
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]

    public IEnumerable<ProductItem?> CatalogRequest()
    {
        List<DO.Product?> products = new List<DO.Product?>();
        products = dal.Product.GetAll().ToList();
        //var productItems = products.Select(Product => changeToBo2(Product));
        products.OrderBy(p => p?.Name).ThenBy(p => p?.productID);
        var productItems = from p in products
                           select changeToBo2(p);
        return (IEnumerable<ProductItem?>)productItems;
    }
    [MethodImpl(MethodImplOptions.Synchronized)]

    public IEnumerable<ProductItem?> catalogGrouping(IEnumerable<ProductItem?> productItems)
    {
        List<ProductItem?> productItems1 = new List<ProductItem?>();
        var catalogGroup = from prductitem in productItems
                           group prductitem by prductitem?.Category into groupproducts
                           select groupproducts;
        foreach(var mycategori in catalogGroup)
        {
            foreach(var oneItem in mycategori)
            {
                productItems1.Add(oneItem);
            }
        }
        return productItems1;
    }




    /// <summary>
    /// Requests Details Of Products From The Costumer
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]

    public ProductItem RequestDetailsFromCostumer(int id)
    {
        try
        {
            List<DO.Product?> Dproducts = new List<DO.Product?>();
            Dproducts = dal.Product.GetAll().ToList();

            BO.ProductItem BproductItem = new BO.ProductItem();


            foreach (var dProduct in Dproducts)
            {
                if (dProduct?.productID == id)
                {
                    BproductItem.Name = dProduct?.Name;
                    BproductItem.Price = dProduct?.Price ?? throw new DO.mayBeNullException();
                    BproductItem.Category = (BO.Enums.Category)(dProduct?.Category ?? throw new DO.mayBeNullException());
                    BproductItem.Amount = dProduct?.InStock ?? throw new DO.mayBeNullException();
                    return BproductItem;
                }
            }
            throw new BO.NotFoundException();
        }
        catch(DO.mayBeNullException)
        {
            throw new BO.mayBeNullException();
        }
        catch(DO.NotFoundException)
        {
            throw new BO.NotFoundException();
        }
    }
    [MethodImpl(MethodImplOptions.Synchronized)]

    public ProductForList GetProductForList(int productId)
   => changeToBo1(dal.Product.Get(productId));
}
