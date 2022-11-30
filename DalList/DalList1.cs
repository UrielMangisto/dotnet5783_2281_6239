using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
namespace Dal;

sealed public class DalList1 : IDal
{
    //public static IDal Instence { get; } = new DalList();
    //private DalList() { }
    public IOrder order => new DalOrder();
    public IProduct product => new DalProduct();
    public IOrderItem orderItem => new DalOrderitem();
}
