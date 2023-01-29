using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using DO;

namespace Dal
{
    internal class DalOrderItem : IOrderItem
    {
        public int Add(OrderItem entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(OrderItem entity)
        {
            throw new NotImplementedException();
        }

        public OrderItem? Get(int entity)
        {
            throw new NotImplementedException();
        }

        public OrderItem? Get(Func<OrderItem?, bool>? selector)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OrderItem?> GetAll(Func<OrderItem?, bool>? selector = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OrderItem?> GetItemsByOrder(int orderId, Func<OrderItem?, bool>? selector = null)
        {
            throw new NotImplementedException();
        }

        public OrderItem? specificItemGet(int idOfProduct, int idOfOrder)
        {
            throw new NotImplementedException();
        }

        public void Update(OrderItem entity)
        {
            throw new NotImplementedException();
        }
    }
}
