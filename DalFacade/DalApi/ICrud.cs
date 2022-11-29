using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    internal interface ICrud<T>
    {
        T Add (T entity);
        T Delete (T entity);
        T Update(T entity);
        T Get (T entity);
        IEnumerable<T> GetAll ();
        T GetByID (T entity);
    }
}
