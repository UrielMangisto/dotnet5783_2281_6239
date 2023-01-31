using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace BlApi;

/// <summary>
/// The implementation of the logic Product entity
/// </summary>
public interface IProduct
{
    /// <summary>
    /// Returns Product List
    /// </summary>
    /// <returns></returns>
    public IEnumerable<ProductForList?> GetProductList();
    /// <summary>
    /// Get Product List By Term
    /// </summary>
    /// <param name="selector"></param>
    /// <returns></returns>
    public IEnumerable<ProductForList?> GetProductsByTerm(Func<BO.ProductForList?, bool>? selector = null);
    /// <summary>
    /// Product Details For Manager
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Product ProductDetailsForManager(int id);

    /// <summary>
    /// Product Details For Costumer
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cart"></param>
    /// <returns></returns>
    public ProductItem ProductDetailsForCostumer(int id, Cart cart);

    /// <summary>
    /// Adding a Product to the list
    /// </summary>
    /// <param name="product"></param>
    public void Add(Product product);

    /// <summary>
    /// Deleting the Product from the list
    /// </summary>
    /// <param name="id"></param>
    public void Delete(int id);

    /// <summary>
    /// Updating a Product from the list by received Product
    /// </summary>
    /// <param name="product"></param>
    public void Update(Product product);


    /// <summary>
    /// Catalog Request From Costumer
    /// </summary>
    /// <returns></returns>
    public IEnumerable<ProductItem?> CatalogRequest();

    /// <summary>
    /// Get Items List By Term
    /// </summary>
    /// <param name="selector"></param>
    /// <returns></returns>
    public IEnumerable<ProductItem?> GetItemsByTerm(Func<BO.ProductItem?, bool>? selector = null);
    /// <summary>
    /// Request Details From Costumer
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public ProductItem RequestDetailsFromCostumer(int id);
    ProductForList GetProductForList(int productId);
    public IEnumerable<ProductItem?> catalogGrouping(IEnumerable<ProductItem?> productItems);
}