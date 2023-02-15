using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
namespace DalApi;

/// <summary>
/// abstract of iorder,iorderItemm and iproduct
/// </summary>
/// <typeparam name="T"></typeparam>
public interface ICrud<T> where T: struct 
{
    /// <summary>
    /// add a T
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    int Add(T entity);
    /// <summary>
    /// delete a T
    /// </summary>
    /// <param name="entity"></param>
    void Delete(T entity);
    /// <summary>
    /// update a T
    /// </summary>
    /// <param name="entity"></param>
    void Update(T entity);
    /// <summary>
    /// return a T
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    T? Get(int entity);
    /// <summary>
    /// return a T by func 
    /// </summary>
    /// <param name="selector"></param>
    /// <returns></returns>
    T? Get(Func<T?, bool>? selector);
    /// <summary>
    /// return all the T
    /// </summary>
    /// <param name="selector"></param>
    /// <returns></returns>
    IEnumerable<T?> GetAll(Func<T?,bool>? selector =null);


}
