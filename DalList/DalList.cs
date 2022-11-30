using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;

sealed internal class DalList : IDal
{
    public static IDal Instence { get; } = new DalList();
    private DalList() { }
    public IOrder order => DalOrder();
    public IProduct product => DalProduct();
    public IOrderItem orderItem => DalOrderitem();
}
