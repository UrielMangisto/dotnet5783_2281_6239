using System.Runtime.CompilerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DalApi;
using DO;

namespace Dal
{
    

    internal class DalOrder: IOrder
    {
        const string OrderPath = "Order";
        static XElement config = XmlTools.LoadConfig();
        [MethodImpl(MethodImplOptions.Synchronized)]
        public int Add(Order entity)
        {
            List<DO.Order?> ListOrder = XmlTools.LoadListFromXMLSerializer<DO.Order>(OrderPath);

            if (ListOrder.FirstOrDefault(orderItem => orderItem?.orderID == entity.orderID) != null)
                throw new Exception("id already exist");

            entity.orderID = int.Parse(config.Element("OrderID")!.Value) + 1;
            ListOrder.Add(entity);

            XmlTools.SaveListToXMLSerializer(ListOrder, OrderPath);

            return entity.orderID;
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Delete(Order entity)
        {
            List<DO.Order?> ListOrder = XmlTools.LoadListFromXMLSerializer<DO.Order>(OrderPath);

            if (ListOrder.Any(order => order?.orderID == entity.orderID))
                ListOrder.Remove(Get(entity.orderID));
            else
                throw new DO.NotFoundException("Order does not exist");
            XmlTools.SaveListToXMLSerializer(ListOrder, OrderPath);
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Order? Get(int entity)
        {
            List<DO.Order?> ListOrder = XmlTools.LoadListFromXMLSerializer<DO.Order>(OrderPath);

            var ord = ListOrder.FirstOrDefault(x => x?.orderID == entity);

            if (ord == null)
                throw new DO.NotFoundException("Order Id not found");
            else
                return (DO.Order)ord;
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Order? Get(Func<Order?, bool>? selector)
        {
            List<DO.Order?> ListOrder = XmlTools.LoadListFromXMLSerializer<DO.Order>(OrderPath);
            if (selector != null)
            {
                var ord = ListOrder.FirstOrDefault(x => selector(x));
                if (ord != null)
                    return (DO.Order)ord;
            }
            throw new DO.NotFoundException("No object is of the delegate");
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Order?> GetAll(Func<Order?, bool>? selector = null)
        {
            List<DO.Order?> Orders = XmlTools.LoadListFromXMLSerializer<DO.Order>(OrderPath);

            if (selector == null)
                return Orders.Select(x => x).OrderBy(x => x?.orderID);
            else
                return Orders.Where(selector).OrderBy(x => x?.orderID);
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Update(Order entity)
        {
            List<DO.Order?> Orders = XmlTools.LoadListFromXMLSerializer<DO.Order>(OrderPath);
            bool found = false;
            var foundOrder = Orders.FirstOrDefault(ord => ord?.orderID == entity.orderID);
            if (foundOrder != null)
            {
                found = true;
                int index = Orders.IndexOf(foundOrder);
                Orders[index] = entity;
            }
            if (found == false)
                throw new DO.NotFoundException("Order Id not found");
            XmlTools.SaveListToXMLSerializer(Orders, OrderPath);
        }

        
    }
}
