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

public class BoOrder : BlApi.IOrder
{
    private IDal dal = new Dal.DalList();
    public IEnumerable<BO.OrderForList?> GetOrderList()
    {
        List<DO.Order?> dOrders = new List<DO.Order?>();
        dOrders = dal.Order.GetAll().ToList();

        List<BO.OrderForList> bOrdersForList = new List<BO.OrderForList>();

        foreach (var d in dOrders)
        {
            try
            {
                int TotalAmount = 0;
                double TotalPrice = 0;
                BO.OrderForList b = new BO.OrderForList();
                foreach (var it in dal.OrderItem.GetItemsByOrder(d?.ID ?? throw new mayBeNullException()))
                {
                    TotalAmount += it?.Amount ?? throw new mayBeNullException();
                    TotalPrice += (it?.Price ?? throw new mayBeNullException()) * (it?.Amount ?? throw new mayBeNullException());
                }
                b.AmountOfItems = TotalAmount;

                b.TotalPrice = TotalPrice;

                b.Id = d?.ID ?? throw new mayBeNullException();

                b.CostomerName = d?.CostumerName ?? throw new mayBeNullException();

                if (d?.DeliveryDate != null)
                    b.Status = Enums.OrderStatus.Delivered;
                else if (d?.ShipDate != null)
                    b.Status = Enums.OrderStatus.Sent;
                else if (d?.OrderDate != null)
                    b.Status = Enums.OrderStatus.Confirmed;
                else
                    b.Status = Enums.OrderStatus.NullStatus;
                bOrdersForList.Add(b);
            }
            catch
            {
                throw new NotFoundException();
            }
        }
        return bOrdersForList;
    }

    public Order DetailsOfOrderForManager(int id)
    {
        try
        {
            if (id >= 0)
            {
                DO.Order? Dorder = new DO.Order();
                BO.Order Border = new BO.Order();

                Dorder = dal.Order.Get(id) ?? throw new mayBeNullException();

                Border.Id = id;
                Border.CostomerName = Dorder?.CostumerName;
                Border.CostomerEmail = Dorder?.CostumerEmail;
                Border.CostomerAdress = Dorder?.CostumerAddress;
                Border.DeliveryDate = Dorder?.DeliveryDate;
                Border.ShipDate = Dorder?.ShipDate;
                Border.OrderDate = Dorder?.OrderDate;
                if (Dorder?.DeliveryDate != null)
                    Border.Status = Enums.OrderStatus.Delivered;
                else if (Dorder?.ShipDate != null)
                    Border.Status = Enums.OrderStatus.Sent;
                else if (Dorder?.OrderDate != null)
                    Border.Status = Enums.OrderStatus.Confirmed;
                else
                    Border.Status = Enums.OrderStatus.NullStatus;

                List<DO.OrderItem?> DorderItems = new List<DO.OrderItem?>();
                DorderItems = dal.OrderItem.GetItemsByOrder(id).ToList();
                Border.OrderItems = new List<OrderItem?>();
                foreach (DO.OrderItem? item in DorderItems)
                {
                    BO.OrderItem item2 = new BO.OrderItem();
                    //לאתחל את האורדר אייטם2 הזה לפי אייטם 
                    item2.Id = item?.ID ?? throw new mayBeNullException();
                    item2.ProductId = item?.ProductID ?? throw new mayBeNullException();
                    item2.ItemName = dal.Product.Get(item2.ProductId)?.Name;
                    item2.Price = item?.Price ?? throw new mayBeNullException();
                    item2.Amount = item?.Amount ?? throw new mayBeNullException();
                    item2.TotalPrice = (item?.Price * item?.Amount) ?? throw new mayBeNullException();
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
            else
            {
                throw new InCorrectDataException();
            }
        }
        catch
        {
            throw new InCorrectDataException();
        }
    }

    public Order DetailsOfOrderForCustomer(int id)
    {
        return DetailsOfOrderForManager(id);
    }

    public Order ShippingUpdate(int id)
    {
        DO.Order? Dorder = new DO.Order();
        Dorder = dal.Order.Get(id);
        if (Dorder?.ShipDate != null || Dorder?.OrderDate == null)
        {
            throw new NotFoundException();
        }

        DO.Order updateOrder = Dorder ?? throw new mayBeNullException();
        updateOrder.ShipDate = DateTime.Now;
        dal.Order.Update(updateOrder);
        BO.Order Border = new BO.Order();
        Border.OrderItems = new List<OrderItem?>();
        Border.Id = id;
        Border.CostomerName = Dorder?.CostumerName;
        Border.CostomerEmail = Dorder?.CostumerEmail;
        Border.CostomerAdress = Dorder?.CostumerAddress;
        Border.DeliveryDate = Dorder?.DeliveryDate;
        Border.ShipDate = Dorder?.ShipDate;
        Border.OrderDate = Dorder?.OrderDate;
        Border.Status = Enums.OrderStatus.Sent;

        List<DO.OrderItem?> DorderItems = new List<DO.OrderItem?>();
        DorderItems = dal.OrderItem.GetItemsByOrder(id).ToList();
        foreach (DO.OrderItem? item in DorderItems)
        {
            BO.OrderItem item2 = new BO.OrderItem();
            //לאתחל את האורדר אייטם2 הזה לפי אייטם 
            item2.Id = item?.ID ?? throw new mayBeNullException();
            item2.ProductId = item?.ProductID ?? throw new mayBeNullException();
            item2.ItemName = dal.Product.Get(item2.ProductId)?.Name;
            item2.Price = item?.Price ?? throw new mayBeNullException();
            item2.Amount = item?.Amount ?? throw new mayBeNullException();
            item2.TotalPrice = (item?.Price * item?.Amount) ?? throw new mayBeNullException();
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

    public Order UpdateDelivery(int id)
    {
        DO.Order? Dorder = new DO.Order?();
        Dorder = dal.Order.Get(id);
        if (Dorder?.DeliveryDate != null || Dorder?.ShipDate == null || Dorder?.OrderDate == null)
        {
            throw new mayBeNullException();//NEED TO CHACK   
        }
        DO.Order updateOrder = Dorder ?? throw new mayBeNullException();
        updateOrder.DeliveryDate = DateTime.Now;
        dal.Order.Update(updateOrder);;
        BO.Order Border = new BO.Order();
        Border.Id = id;
        Border.CostomerName = Dorder?.CostumerName;
        Border.CostomerEmail = Dorder?.CostumerEmail;
        Border.CostomerAdress = Dorder?.CostumerAddress;
        Border.DeliveryDate = Dorder?.DeliveryDate;
        Border.ShipDate = Dorder?.ShipDate;
        Border.OrderDate = Dorder?.OrderDate;
        Border.Status = Enums.OrderStatus.Delivered;

        List<DO.OrderItem?> DorderItems = new List<DO.OrderItem?>();
        DorderItems = dal.OrderItem.GetItemsByOrder(id).ToList();
        foreach (DO.OrderItem? item in DorderItems)
        {
            BO.OrderItem item2 = new BO.OrderItem();
            //לאתחל את האורדר אייטם2 הזה לפי אייטם 
            item2.Id = item?.ID ?? throw new mayBeNullException();
            item2.ProductId = item?.ProductID ?? throw new mayBeNullException();
            item2.ItemName = dal.Product.Get(item2.ProductId)?.Name;
            item2.Price = item?.Price ?? throw new mayBeNullException();
            item2.Amount = item?.Amount ?? throw new mayBeNullException();
            item2.TotalPrice = (item?.Price * item?.Amount) ?? throw new mayBeNullException();
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

    public OrderTracking Track(int id)
    {
        DO.Order? Dorder = new DO.Order();
        Dorder = dal.Order.Get(id);
        BO.OrderTracking orderTracking = new BO.OrderTracking();
        orderTracking.TrackList = new List<(DateTime?, string?)>();
        orderTracking.Id = id;
        if (Dorder?.DeliveryDate != null)
            orderTracking.Status = Enums.OrderStatus.Delivered;
        else if (Dorder?.ShipDate != null)
            orderTracking.Status = Enums.OrderStatus.Sent;
        else if (Dorder?.OrderDate != null)
            orderTracking.Status = Enums.OrderStatus.Confirmed;
        else
            orderTracking.Status = Enums.OrderStatus.NullStatus;
        if (Dorder?.OrderDate != null)
        {
            orderTracking.TrackList.Add((Dorder?.OrderDate, " the order was created"));
            if (Dorder?.ShipDate != null)
            {
                orderTracking.TrackList.Add((Dorder?.ShipDate, " the order was shipped"));

                if (Dorder?.DeliveryDate != null)
                {
                    orderTracking.TrackList.Add((Dorder?.DeliveryDate, " the order was deliveried"));
                }
            }
        }
        return orderTracking;

    }
    public void changeamountformnanager(int newAmount, OrderItem orderItem, Order order)
    {
        if (order.Status != Enums.OrderStatus.Sent)
        {
            foreach (var p in order.OrderItems)
            {
                if (p.Id <= orderItem.Id)
                    p.Amount = newAmount;
            }
        }
        else
        {
            throw new NotvalidException("order alredy sent");
        }
    }

}

