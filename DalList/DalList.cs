using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using DalApi;
namespace Dal
{
    sealed public class DalList : IDal
    {
        //public static IDal Instence { get; } = new DalList();
        //private DalList() { }
        public IOrder Order { get; }
        public IProduct Product { get; }
        public IOrderItem OrderItem { get; }

        public DalList()
        {
            Order = new DalOrder();
            Product = new DalProduct();
            OrderItem = new DalOrderitem();
        }
    }
}
