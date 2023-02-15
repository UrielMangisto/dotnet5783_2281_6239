using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using BO;

namespace BlApi;
/// <summary>
/// The implementation of the logic cart entity
/// </summary>
public interface ICart
{
    /// <summary>
    /// add a cart 
    /// </summary>
    /// <param name="C"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    public Cart Add(Cart C, int id);
    /// <summary>
    /// update a cart
    /// </summary>
    /// <param name="C"></param>
    /// <param name="ID"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    public Cart Update(Cart C, int ID, int amount);
    /// <summary>
    /// transformit a cart to order 
    /// </summary>
    /// <param name="C"></param>
    public void Confirmation(Cart C);
    /// <summary>
    /// reutern the list of the items of the the cart 
    /// </summary>
    /// <param name="c"></param>
    /// <returns></returns>
    public IEnumerable<BO.OrderItem?> GetTheItems(BO.Cart c);
}