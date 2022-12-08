using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using BO;
using Dal;
using DalApi;
/// <summary>
/// The implementation of the order
/// </summary>
namespace BlImplementation;

internal class BoOrder : BlApi.IOrder
{
    private IDal dal = new Dal.DalList();
    public IEnumerable<BO.OrderForList> GetOrderList()
    {
        List<DO.Order> dOrders  = new List<DO.Order>();
        dOrders = (List<DO.Order>)dal.Order.GetAll();

        List<BO.OrderForList> bOrdersForList = new List<BO.OrderForList>();

        foreach(var d in dOrders)
        {
            try
            {
                int TotalAmount = 0;
                double TotalPrice = 0;
                BO.OrderForList b = new BO.OrderForList();
                foreach (var it in dal.OrderItem.GetItemsByOrder(d.ID))
                {
                    TotalAmount += it.Amount;
                    TotalPrice += it.Price * it.Amount;
                }
                b.AmountOfItems = TotalAmount;

                b.TotalPrice = TotalPrice;

                b.Id = d.ID;

                b.CostomerName = d.CostumerName;

                if (d.DeliveryDate != null)
                    b.Status = Enums.OrderStatus.Delivered;
                else if (d.ShipDate != null)
                    b.Status = Enums.OrderStatus.Sent;
                else if (d.OrderDate != null)
                    b.Status = Enums.OrderStatus.Confirmed;
                else
                    b.Status = Enums.OrderStatus.NullStatus;
                bOrdersForList.Add(b);
            }
            catch
            {
                throw new Exception();
            }
        }
        return bOrdersForList;
    }

    public Order DetailsOfOrderForManager(int id)
    {
        try
        {
            if(id >= 0)
            {
                DO.Order Dorder = new DO.Order();
                BO.Order? Border = new BO.Order();

                Dorder = dal.Order.Get(id);

                Border.Id = id;
                Border.CostomerName = Dorder.CostumerName;
                Border.CostomerEmail = Dorder.CostumerEmail;
                Border.CostomerAdress = Dorder.CostumerAddress;
                Border.DeliveryDate = Dorder.DeliveryDate;
                Border.ShipDate = Dorder.ShipDate;
                Border.OrderDate = Dorder.OrderDate;
                Border.PaymentDate = Dorder.OrderDate;//???
                if (Dorder.DeliveryDate != null)
                    Border.Status = Enums.OrderStatus.Delivered;
                else if (Dorder.ShipDate != null)
                    Border.Status = Enums.OrderStatus.Sent;
                else if (Dorder.OrderDate != null)
                    Border.Status = Enums.OrderStatus.Confirmed;
                else
                    Border.Status = Enums.OrderStatus.NullStatus;

                List<DO.OrderItem> DorderItems = new List<DO.OrderItem>();
                DorderItems = (List<DO.OrderItem>)dal.OrderItem.GetItemsByOrder(id);
                foreach (DO.OrderItem item in DorderItems)
                {
                    BO.OrderItem item2 = new BO.OrderItem();
                    //לאתחל את האורדר אייטם2 הזה לפי אייטם 
                    item2.Id = item.ID;
                    item2.ProductId = item.ProductID;
                    item2.ItemName = dal.Product.Get(item2.ProductId).Name;
                    item2.Price = item.Price;
                    item2.Amount = item.Amount;
                    item2.TotalPrice = item.Price * item.Amount;
                    //לשים את האייטם2 הזה בתוך הרשימה של האייטמים שמסוג 'בו', יעני לעשות אדד
                    Border.OrderItems.Add(item2);
                }
                Border.TotalPrice = 0;
                foreach (var item in Border.OrderItems)
                {
                    Border.TotalPrice+= item.TotalPrice;
                }
                return Border;
            }
            else
            {
                throw new Exception();
            }
        }
        catch
        {
            throw new Exception();
        }
    }

    public Order DetailsOfOrderForCustomer(int id)
    {
        return DetailsOfOrderForManager(id);
    }

    public Order ShippingUpdate(int id)
    {
        try
        {
            DO.Order Dorder = new DO.Order();
            Dorder = dal.Order.Get(id);
            if(Dorder.ShipDate != null || Dorder.OrderDate == null)
            {
                throw new Exception();
            }
            Dorder.ShipDate = DateTime.Now;
            dal.Order.Update(Dorder);
            BO.Order Border = new BO.Order();
            Border.Id = id;
            Border.CostomerName = Dorder.CostumerName;
            Border.CostomerEmail = Dorder.CostumerEmail;
            Border.CostomerAdress = Dorder.CostumerAddress;
            Border.DeliveryDate = Dorder.DeliveryDate;
            Border.ShipDate = Dorder.ShipDate;
            Border.OrderDate = Dorder.OrderDate;
            Border.PaymentDate = Dorder.OrderDate;//???
            Border.Status = Enums.OrderStatus.Sent;

            List<DO.OrderItem> DorderItems = new List<DO.OrderItem>();
            DorderItems = (List<DO.OrderItem>)dal.OrderItem.GetItemsByOrder(id);
            foreach (DO.OrderItem item in DorderItems)
            {
                BO.OrderItem item2 = new BO.OrderItem();
                //לאתחל את האורדר אייטם2 הזה לפי אייטם 
                item2.Id = item.ID;
                item2.ProductId = item.ProductID;
                item2.ItemName = dal.Product.Get(item2.ProductId).Name;
                item2.Price = item.Price;
                item2.Amount = item.Amount;
                item2.TotalPrice = item.Price * item.Amount;
                //לשים את האייטם2 הזה בתוך הרשימה של האייטמים שמסוג 'בו', יעני לעשות אדד
                Border.OrderItems.Add(item2);
            }
            Border.TotalPrice = 0;
            foreach (var item in Border.OrderItems)
            {
                Border.TotalPrice += item.TotalPrice;
            }
            return Border;
        }
        catch 
        {
            throw new Exception();
        }
    }

    public Order UpdateDelivery(int id)
    {
        try
        {
            DO.Order Dorder = new DO.Order();
            Dorder = dal.Order.Get(id);
            if (Dorder.DeliveryDate != null||Dorder.ShipDate == null || Dorder.OrderDate == null)
            {
                throw new Exception();
            }
            Dorder.DeliveryDate = DateTime.Now;
            dal.Order.Update(Dorder);
            BO.Order Border = new BO.Order();
            Border.Id = id;
            Border.CostomerName = Dorder.CostumerName;
            Border.CostomerEmail = Dorder.CostumerEmail;
            Border.CostomerAdress = Dorder.CostumerAddress;
            Border.DeliveryDate = Dorder.DeliveryDate;
            Border.ShipDate = Dorder.ShipDate;
            Border.OrderDate = Dorder.OrderDate;
            Border.PaymentDate = Dorder.OrderDate;//???
            Border.Status = Enums.OrderStatus.Delivered;

            List<DO.OrderItem> DorderItems = new List<DO.OrderItem>();
            DorderItems = (List<DO.OrderItem>)dal.OrderItem.GetItemsByOrder(id);
            foreach (DO.OrderItem item in DorderItems)
            {
                BO.OrderItem item2 = new BO.OrderItem();
                //לאתחל את האורדר אייטם2 הזה לפי אייטם 
                item2.Id = item.ID;
                item2.ProductId = item.ProductID;
                item2.ItemName = dal.Product.Get(item2.ProductId).Name;
                item2.Price = item.Price;
                item2.Amount = item.Amount;
                item2.TotalPrice = item.Price * item.Amount;
                //לשים את האייטם2 הזה בתוך הרשימה של האייטמים שמסוג 'בו', יעני לעשות אדד
                Border.OrderItems.Add(item2);
            }
            Border.TotalPrice = 0;
            foreach (var item in Border.OrderItems)
            {
                Border.TotalPrice += item.TotalPrice;
            }
            return Border;
        }
        catch
        {
            throw new Exception();
        }
    }

    public OrderTracking Track(int id)
    {
        try
        {
            DO.Order Dorder = new DO.Order();
            Dorder = dal.Order.Get(id);
            BO.OrderTracking orderTracking = new BO.OrderTracking();
            orderTracking.Id = id;
            if (Dorder.DeliveryDate != null)
                orderTracking.Status = Enums.OrderStatus.Delivered;
            else if (Dorder.ShipDate != null)
                orderTracking.Status = Enums.OrderStatus.Sent;
            else if (Dorder.OrderDate != null)
                orderTracking.Status = Enums.OrderStatus.Confirmed;
            else
                orderTracking.Status = Enums.OrderStatus.NullStatus;
            if (Dorder.OrderDate != null)
            {
                orderTracking.TrackList.Add((Dorder.OrderDate, " the order was created"));
                if (Dorder.ShipDate != null)
                {
                    orderTracking.TrackList.Add((Dorder.ShipDate, " the order was shipped"));

                    if (Dorder.DeliveryDate != null)
                    {
                        orderTracking.TrackList.Add((Dorder.DeliveryDate, " the order was deliveried"));
                    }
                }
            }
            return orderTracking; 

        }
        catch
        {
            throw new Exception();
        }
    }
}
