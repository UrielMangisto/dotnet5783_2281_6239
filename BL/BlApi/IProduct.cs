using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace BlApi;

/// <summary>
/// 
/// </summary>
public interface IProduct
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public IEnumerable<ProductForList> GetProducts();
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Product ProductDetailsForManager(int id);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cart"></param>
    /// <returns></returns>
    public ProductItem ProductDetailsForCostumer(int id, Cart cart);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="product"></param>
    public void Add(Product product);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    public void Delete(int id);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="product"></param>
    public void Update(Product product);

    /// <summary>
    /// 
    /// </summary>
    public IEnumerable<ProductItem> KatalogRequest();

}