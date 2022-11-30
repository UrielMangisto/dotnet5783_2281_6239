using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

 namespace DalApi;

/// <summary>
/// abstract of iorder,iorderItemm and iprodoct
/// </summary>
/// <typeparam name="T"></typeparam>
public interface ICrud<T>
{
T Add(T entity);
T Delete(T entity);
T Update(T entity);
T Get(int entity);
IEnumerable<T> GetAll();
T GetByID(T entity);
}
