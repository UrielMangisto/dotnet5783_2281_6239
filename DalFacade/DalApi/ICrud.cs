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
    int Add(T entity);
    void Delete(T entity);
    void Update(T entity);
    T? Get(int entity);
    IEnumerable<T?> GetAll();
}
