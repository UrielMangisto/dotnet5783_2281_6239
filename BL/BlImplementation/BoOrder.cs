using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using BO;
using DalApi;
/// <summary>
/// The implementation of the Order
/// </summary>
namespace BlImplementation;

public class BoOrder : BlApi.IOrder
{
    DalApi.IDal? dal = DalApi.Factory.Get();

    BO.OrderForList changeToBo1(DO.Order? dOrder)
    {
        int TotalAmount = 0;
        double TotalPrice = 0;
        BO.OrderForList b = new BO.OrderForList();
        foreach (var it in dal.OrderItem.GetItemsByOrder(dOrder?.ID ?? throw new DO.mayBeNullException()))
        {
            TotalAmount += it?.Amount ?? throw new DO.mayBeNullException();
            TotalPrice += (it?.Price ?? throw new DO.mayBeNullException()) * (it?.Amount ?? throw new DO.mayBeNullException());
        }
        b.AmountOfItems = TotalAmount;

        b.TotalPrice = TotalPrice;

        b.Id = dOrder?.ID ?? throw new DO.mayBeNullException();

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


    public Order DetailsOfOrderForManager(int id)
    {
        try
        {
            if (id >= 0)
            {
                DO.Order? Dorder = new DO.Order();
                BO.Order Border = new BO.Order();

                Dorder = dal.Order.Get(id) ?? throw new DO.mayBeNullException();

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
                    item2.Id = item?.ID ?? throw new DO.mayBeNullException();
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

    public Order DetailsOfOrderForCustomer(int id)
    {
        return DetailsOfOrderForManager(id);
    }

    public Order ShippingUpdate(int id)
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
                item2.Id = item?.ID ?? throw new DO.mayBeNullException();
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

    public Order UpdateDelivery(int id)
    {
        try
        {
            DO.Order? Dorder = new DO.Order?();
            Dorder = dal.Order.Get(id);
            if (Dorder?.DeliveryDate != null || Dorder?.ShipDate == null || Dorder?.OrderDate == null)
            {
                throw new DO.mayBeNullException();//NEED TO CHACK   
            }
            DO.Order updateOrder = Dorder ?? throw new DO.mayBeNullException();
            updateOrder.DeliveryDate = DateTime.Now;
            dal.Order.Update(updateOrder); ;
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
                item2.Id = item?.ID ?? throw new DO.mayBeNullException();
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
            orderTracking.TrackList.Add((Dorder?.OrderDate, " the Order was created"));
            if (Dorder?.ShipDate != null)
            {
                orderTracking.TrackList.Add((Dorder?.ShipDate, " the Order was shipped"));

                if (Dorder?.DeliveryDate != null)
                {
                    orderTracking.TrackList.Add((Dorder?.DeliveryDate, " the Order was deliveried"));
                }
            }
        }
        return orderTracking;

    }
    public void changeamountformnanager(int newAmount, OrderItem orderItem, Order order)
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

}

