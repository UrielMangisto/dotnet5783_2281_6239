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
    /// 
    /// </summary>
    /// <param name="C"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    public Cart Add(Cart C, int id);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="C"></param>
    /// <param name="ID"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    public Cart Update(Cart C, int ID, int amount);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="C"></param>
    public void Confirmation(Cart C);
}