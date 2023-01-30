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

        public int Add(Order entity)
        {
            List<DO.Order?> ListOrder = XmlTools.LoadListFromXMLSerializer<DO.Order>(OrderPath);

            if (ListOrder.FirstOrDefault(orderItem => orderItem?.ID == entity.ID) != null)
                throw new Exception("id already exist");

            entity.ID = int.Parse(config.Element("OrderID")!.Value) + 1;
            ListOrder.Add(entity);

            XmlTools.SaveListToXMLSerializer(ListOrder, OrderPath);

            return entity.ID;
        }

        public void Delete(Order entity)
        {
            List<DO.Order?> ListOrder = XmlTools.LoadListFromXMLSerializer<DO.Order>(OrderPath);

            if (ListOrder.Any(order => order?.ID == entity.ID))
                ListOrder.Remove(Get(entity.ID));
            else
                throw new DO.NotFoundException("Order does not exist");
            XmlTools.SaveListToXMLSerializer(ListOrder, OrderPath);
        }

        public Order? Get(int entity)
        {
            List<DO.Order?> ListOrder = XmlTools.LoadListFromXMLSerializer<DO.Order>(OrderPath);

            var ord = ListOrder.FirstOrDefault(x => x?.ID == entity);

            if (ord == null)
                throw new DO.NotFoundException("Order Id not found");
            else
                return (DO.Order)ord;
        }

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

        public IEnumerable<Order?> GetAll(Func<Order?, bool>? selector = null)
        {
            List<DO.Order?> Orders = XmlTools.LoadListFromXMLSerializer<DO.Order>(OrderPath);

            if (selector == null)
                return Orders.Select(x => x).OrderBy(x => x?.ID);
            else
                return Orders.Where(selector).OrderBy(x => x?.ID);
        }

        public void Update(Order entity)
        {
            List<DO.Order?> Orders = XmlTools.LoadListFromXMLSerializer<DO.Order>(OrderPath);
            bool found = false;
            var foundOrder = Orders.FirstOrDefault(ord => ord?.ID == entity.ID);
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
