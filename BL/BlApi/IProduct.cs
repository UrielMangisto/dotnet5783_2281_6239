using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace BlApi;

/// <summary>
/// The implementation of the logic product entity
/// </summary>
public interface IProduct
{
    /// <summary>
    /// Returns Product List
    /// </summary>
    /// <returns></returns>
    public IEnumerable<ProductForList> GetProductList();

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
    /// Adding a product to the list
    /// </summary>
    /// <param name="product"></param>
    public void Add(Product product);

    /// <summary>
    /// Deleting the product from the list
    /// </summary>
    /// <param name="id"></param>
    public void Delete(int id);

    /// <summary>
    /// Updating a product from the list by received product
    /// </summary>
    /// <param name="product"></param>
    public void Update(Product product);


    /// <summary>
    /// Catalog Request From Costumer
    /// </summary>
    /// <returns></returns>
    public IEnumerable<ProductItem> CatalogRequest();

    public ProductItem RequestDetailsFromCostumer(int id);
}