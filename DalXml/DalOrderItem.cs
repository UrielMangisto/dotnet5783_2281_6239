using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DalApi;
using DO;

namespace Dal
{
    internal class DalOrderItem : IOrderItem
    {
        const string OrderItemPath = "OrderItem";
        static XElement config = XmlTools.LoadConfig();
        public int Add(OrderItem entity)
        {
            List<DO.OrderItem?> listOrderItem = XmlTools.LoadListFromXMLSerializer<DO.OrderItem>(OrderItemPath);

            if (listOrderItem.FirstOrDefault(x => x?.orderItemID == entity.orderItemID) != null)
                throw new DO.AlreadyExistException("OrderItem Id is already exist");

            entity.orderItemID = int.Parse(config.Element("orderID")!.Value) + 1;
            listOrderItem.Add(entity);

            XmlTools.SaveListToXMLSerializer(listOrderItem, OrderItemPath);

            return entity.orderItemID;
        }

        public void Delete(OrderItem entity)
        {
            List<DO.OrderItem?> ListOrderItem = XmlTools.LoadListFromXMLSerializer<DO.OrderItem>(OrderItemPath);

            if (ListOrderItem.Any(x => x?.orderItemID == entity.orderItemID))
                ListOrderItem.Remove(Get(entity.orderItemID));
            else
                throw new DO.NotFoundException("Order item not exist");

            XmlTools.SaveListToXMLSerializer(ListOrderItem, OrderItemPath);
        }

        public OrderItem? Get(int entity)
        {
            List<DO.OrderItem?> ListOrderItem = XmlTools.LoadListFromXMLSerializer<DO.OrderItem>(OrderItemPath);

            var orderItem = ListOrderItem.FirstOrDefault(x => x?.orderItemID == entity);
            if (orderItem == null)
                throw new DO.NotFoundException("Order item id not isExist");
            else
                return (DO.OrderItem)orderItem;
        }

        public OrderItem? Get(Func<OrderItem?, bool>? selector)
        {
            List<DO.OrderItem?> ListOrderItem = XmlTools.LoadListFromXMLSerializer<DO.OrderItem>(OrderItemPath);

            if (selector != null)
            {
                var ordItem = ListOrderItem.FirstOrDefault(item => selector(item));
                if (ordItem != null)
                    return (DO.OrderItem)ordItem;
            }
            throw new DO.NotFoundException("No object is of the delegate");
        }

        public IEnumerable<OrderItem?> GetAll(Func<OrderItem?, bool>? selector = null)
        {
            List<DO.OrderItem?> ListOrderItem = XmlTools.LoadListFromXMLSerializer<DO.OrderItem>(OrderItemPath);

            var orderItems = ListOrderItem.Where(ordItem => selector == null || selector(ordItem)).ToList();
            return orderItems;
        }

        public IEnumerable<OrderItem?> GetItemsByOrder(int orderId, Func<OrderItem?, bool>? selector = null)
        {

            List<DO.OrderItem?> ListOrderItem = XmlTools.LoadListFromXMLSerializer<DO.OrderItem>(OrderItemPath);

            bool chacking(OrderItem? orderItem)
            {
                //if (orderItem.Value.orderID == orderID)
                if (orderItem.Value.OrderID == orderId)
                {
                    return true;
                }
                return false;
            }
            if (selector == null)
            {
                var specificItems = ListOrderItem.Where(chacking);
                if (specificItems == null)
                {
                    throw new NotFoundException();
                }
                return specificItems;
            }
            else
            {

                var specificItems = ListOrderItem.Where(chacking).Where(selector);
                if (specificItems == null)
                {
                    throw new NotFoundException();
                }
                return specificItems;
            }

        }

        public OrderItem? specificItemGet(int idOfProduct, int idOfOrder)
        {
            List<DO.OrderItem?> ListOrderItem = XmlTools.LoadListFromXMLSerializer<DO.OrderItem>(OrderItemPath);
            foreach (var p in ListOrderItem)
            {
                //if (idOfProduct == p?.orderID && idOfOrder == p?.OrderID)
                if (idOfProduct == p?.ProductID && idOfOrder == p?.OrderID)
                {
                    return p;
                }
            }
            throw new NotFoundException();
        }

        public void Update(OrderItem entity)
        {
            List<DO.OrderItem?> ListOrderItem = XmlTools.LoadListFromXMLSerializer<DO.OrderItem>(OrderItemPath);

            bool isExist = false;
            var foundOrderItem = ListOrderItem.FirstOrDefault(x => x?.orderItemID == entity.orderItemID);
            if (foundOrderItem != null)
            {
                isExist = true;
                int index = ListOrderItem.IndexOf(foundOrderItem);
                ListOrderItem[index] = entity;
            }
            if (isExist == false)
                throw new DO.NotFoundException("Order item id not exist");
            XmlTools.SaveListToXMLSerializer(ListOrderItem, OrderItemPath);
        }
    }
}
