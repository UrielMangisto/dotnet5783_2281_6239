using System.Runtime.CompilerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using BO;
using Dal;
using DalApi;
using DO;
using System.Data;
/// <summary>
/// The implementation of the Order
/// </summary>
namespace BlImplementation;

public class BoOrder : BlApi.IOrder
{
    DalApi.IDal? dal = DalApi.Factory.Get();

    [MethodImpl(MethodImplOptions.Synchronized)]
    BO.OrderForList changeToBo1(DO.Order? dOrder)
    {
        int TotalAmount = 0;
        double TotalPrice = 0;
        BO.OrderForList b = new BO.OrderForList();
        foreach (var it in dal.OrderItem.GetItemsByOrder(dOrder?.orderID ?? throw new DO.mayBeNullException()))
        {
            TotalAmount += it?.Amount ?? throw new DO.mayBeNullException();
            TotalPrice += (it?.Price ?? throw new DO.mayBeNullException()) * (it?.Amount ?? throw new DO.mayBeNullException());
        }
        b.AmountOfItems = TotalAmount;

        b.TotalPrice = TotalPrice;

        b.Id = dOrder?.orderID ?? throw new DO.mayBeNullException();

        b.CostomerName = dOrder?.CostumerName ?? throw new DO.mayBeNullException();

        if (dOrder?.DeliveryDate != null)
            b.Status = Enums.OrderStatus.Delivered;
        else if (dOrder?.ShipDate != null)
            b.Status = Enums.OrderStatus.Sent;
        else if (dOrder?.OrderDate != null)
            b.Status = Enums.OrderStatus.Confirmed;
        else
            b.Status = Enums.OrderStatus.NullStatus;
        return b;
    }
    [MethodImpl(MethodImplOptions.Synchronized)]

    public IEnumerable<BO.OrderForList?> GetOrderList()
    {
        List<DO.Order?> dOrders = new List<DO.Order?>();
        dOrders = dal.Order.GetAll().ToList();
        try
        {
            var bOrdersForList = from dp in dOrders
                                 let bp = changeToBo1(dp)
                                 select bp;
            return bOrdersForList;
            
        }
        catch(DO.NotFoundException)
        {
            throw new BO.NotFoundException();
        }
        catch(DO.mayBeNullException)
        {
            throw new BO.mayBeNullException();
        }
        
    }

    [MethodImpl(MethodImplOptions.Synchronized)]

    public BO.Order DetailsOfOrderForManager(int id)
    {
        try
        {
            if (id >= 0)
            {
                DO.Order? Dorder = new DO.Order();
                BO.Order Border = new BO.Order();

                Dorder = dal.Order.Get(id) ?? throw new DO.mayBeNullException();

                Border.Id = id;
                Border.CostumerName = Dorder?.CostumerName;
                Border.CostumerEmail = Dorder?.CostumerEmail;
                Border.CostumerAddress = Dorder?.CostumerAddress;
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
                Border.OrderItems = new List<BO.OrderItem?>();
                foreach (DO.OrderItem? item in DorderItems)
                {
                    BO.OrderItem item2 = new BO.OrderItem();
                    //לאתחל את האורדר אייטם2 הזה לפי אייטם 
                    item2.Id = item?.orderItemID ?? throw new DO.mayBeNullException();
                    item2.ProductId = item?.ProductID ?? throw new DO.mayBeNullException();
                    item2.ItemName = dal.Product.Get(item2.ProductId)?.Name;
                    item2.Price = item?.Price ?? throw new DO.mayBeNullException();
                    item2.Amount = item?.Amount ?? throw new DO.mayBeNullException();
                    item2.TotalPrice = (item?.Price * item?.Amount) ?? throw new DO.mayBeNullException();
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
                throw new DO.InCorrectDataException();
            }
        }
        catch(DO.mayBeNullException)
        {
            throw new BO.InCorrectDataException();
        }
        catch(DO.InCorrectDataException)
        {
            throw new BO.InCorrectDataException();
        }
    }
    [MethodImpl(MethodImplOptions.Synchronized)]

    public BO.Order DetailsOfOrderForCustomer(int id)
    {
        return DetailsOfOrderForManager(id);
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public BO.Order ShippingUpdate(int id)
    {
        try
        {
            DO.Order? Dorder = new DO.Order();
            Dorder = dal.Order.Get(id);
            if (Dorder?.ShipDate != null || Dorder?.OrderDate == null)
            {
                throw new DO.NotFoundException();
            }

            DO.Order updateOrder = Dorder ?? throw new DO.mayBeNullException();
            updateOrder.ShipDate = DateTime.Now;
            dal.Order.Update(updateOrder);
            BO.Order Border = new BO.Order();
            Border.OrderItems = new List<BO.OrderItem?>();
            Border.Id = id;
            Border.CostumerName = Dorder?.CostumerName;
            Border.CostumerEmail = Dorder?.CostumerEmail;
            Border.CostumerAddress = Dorder?.CostumerAddress;
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
                item2.Id = item?.orderItemID ?? throw new DO.mayBeNullException();
                item2.ProductId = item?.ProductID ?? throw new DO.mayBeNullException();
                item2.ItemName = dal.Product.Get(item2.ProductId)?.Name;
                item2.Price = item?.Price ?? throw new DO.mayBeNullException();
                item2.Amount = item?.Amount ?? throw new DO.mayBeNullException();
                item2.TotalPrice = (item?.Price * item?.Amount) ?? throw new DO.mayBeNullException();
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
        catch(DO.NotFoundException)
        {
            throw new BO.NotFoundException();
        } 
        catch(DO.mayBeNullException)
        {
            throw new BO.mayBeNullException();
        }
    }
    [MethodImpl(MethodImplOptions.Synchronized)]

    public BO.Order UpdateDelivery(int id)
    {
        try
        {
            DO.Order? Dorder = new DO.Order?();
            Dorder = dal.Order.Get(id);
            if (Dorder?.DeliveryDate != null || Dorder?.ShipDate == null || Dorder?.OrderDate == null)
            {
                throw new DO.mayBeNullException();   
            }
            DO.Order updateOrder = Dorder ?? throw new DO.mayBeNullException();
            updateOrder.DeliveryDate = DateTime.Now;
            dal.Order.Update(updateOrder); ;
            BO.Order Border = new BO.Order();
            Border.Id = id;
            Border.CostumerName = Dorder?.CostumerName;
            Border.CostumerEmail = Dorder?.CostumerEmail;
            Border.CostumerAddress = Dorder?.CostumerAddress;
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
                item2.Id = item?.orderItemID ?? throw new DO.mayBeNullException();
                item2.ProductId = item?.ProductID ?? throw new DO.mayBeNullException();
                item2.ItemName = dal.Product.Get(item2.ProductId)?.Name;
                item2.Price = item?.Price ?? throw new DO.mayBeNullException();
                item2.Amount = item?.Amount ?? throw new DO.mayBeNullException();
                item2.TotalPrice = (item?.Price * item?.Amount) ?? throw new DO.mayBeNullException();
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
        catch(DO.mayBeNullException)
        {
            throw new BO.mayBeNullException();
        }
        

    }
    [MethodImpl(MethodImplOptions.Synchronized)]

    public IEnumerable<OrderTracking> getTracList()
    {
        List<OrderTracking> list = new List<OrderTracking>();
        foreach(var order in dal.Order.GetAll())
        {
            list.Add(Track(order?.orderID ?? throw new DO.mayBeNullException()));
        }
        return list;


    }


    [MethodImpl(MethodImplOptions.Synchronized)]
    public OrderTracking Track(int id)
    {
        DO.Order? Dorder = new DO.Order();
        Dorder = dal.Order.Get(id);
        BO.OrderTracking orderTracking = new BO.OrderTracking();
        orderTracking.TrackList = new List<TrackLst>();
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
            TrackLst lst = new TrackLst();
            lst.Status = " the Order was created";
            lst.Date = Dorder?.OrderDate;

            orderTracking.TrackList.Add(lst);
            if (Dorder?.ShipDate != null)
            {
                lst.Status = " the Order was shipped";
                lst.Date = Dorder?.ShipDate;
                orderTracking.TrackList.Add(lst);

                if (Dorder?.DeliveryDate != null)
                {
                    lst.Status = " the Order was devliveried";
                    lst.Date = Dorder?.DeliveryDate;
                    orderTracking.TrackList.Add(lst);
                }
            }
        }
        return orderTracking;

    }
    [MethodImpl(MethodImplOptions.Synchronized)]

    public void changeamountformnanager(int newAmount, BO.OrderItem orderItem, BO.Order order)
    {
        try
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
                throw new DO.NotvalidException("Order alredy sent");
            }
        }
        catch(DO.NotvalidException)
        {
            throw new BO.NotvalidException();
        }   
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<TrackLst> getListOfTrack(OrderTracking? ot)
    {
        ot = ot ?? new OrderTracking();
        return ot.TrackList ?? throw new BO.mayBeNullException();
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<BO.OrderItem?> getItemListFromOrder(int orderId)
    {
        BO.Order bord = new BO.Order();
        bord = DetailsOfOrderForManager(orderId);
        return bord.OrderItems ?? throw new DO.mayBeNullException();
    }

    /// <summary>
    /// An helper function for stage 7, returns the next order to show...
    /// </summary>
    /// <returns></returns>
    /// <exception cref="mayBeNullException"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public int? GetOrderForHandle()
    {
        var list = dal?.Order.GetAll();
        if (list == null) return null;
        DO.Order orderToHandle = list.FirstOrDefault() ?? throw new DO.mayBeNullException();
        foreach (var order in dal.Order.GetAll())
        {
            if (order?.DeliveryDate == null)
            {
                if (orderToHandle.ShipDate < order?.ShipDate)
                {
                    orderToHandle = order?? throw new DO.mayBeNullException();
                }
                if (orderToHandle.OrderDate < order?.OrderDate)
                {
                    orderToHandle = order ?? throw new DO.mayBeNullException();
                }
            }
        }
        return orderToHandle.orderID;
    }
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Update(BO.Order? order)
    {
        try
        {
            DO.Order temp = new DO.Order();
            if (order.Status != Enums.OrderStatus.Confirmed)
                throw new DO.NotvalidException();
            temp.orderID = order.Id;
            temp.CostumerName = order.CostumerName;
            temp.CostumerAddress = order.CostumerAddress;
            temp.CostumerEmail = order.CostumerEmail;
            temp.OrderDate = order.OrderDate;
            temp.ShipDate = null;
            temp.DeliveryDate = null;
            DO.OrderItem testItem = new DO.OrderItem();
            DO.Product dproduct = new();
            foreach(BO.OrderItem? it in order?.OrderItems)
            {
                dproduct = (DO.Product)dal.Product.Get(it.ProductId);
                testItem = (DO.OrderItem)dal.OrderItem.Get(it.Id);
                int diff = it?.Amount - testItem.Amount ?? throw new DO.mayBeNullException();
                if (it?.Amount == 0)
                {
                    order.OrderItems.Remove(it);
                    order.TotalPrice -= it.Price * it.Amount;
                    dproduct.InStock += diff;
                }
                else
                {
                    if (diff > 0)
                    {
                        if (dproduct.InStock < diff)
                            throw new DO.NotExistInStockException();//there are not enough products in the stock
                        order.TotalPrice += diff*it.Price;
                    }
                    else
                    {
                        order.TotalPrice += diff*it.Price;
                    }
                }
                DO.OrderItem dit = new DO.OrderItem();
                dit.OrderID = order.Id;
                dit.Price = it.Price;
                dit.ProductID = it.ProductId;
                dit.Amount = it.Amount;
                dit.orderItemID = it.Id;
                dal.OrderItem.Update(dit);
            }
            dal.Product.Update(dproduct);
            dal.Order.Update(temp);
        }
        catch(DO.NotvalidException)
        {
            throw new BO.NotvalidException();
        }


    }
}

