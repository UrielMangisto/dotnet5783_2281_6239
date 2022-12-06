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
                DorderItems = (List<DO.OrderItem>)dal.OrderItem.GetAll();
                foreach (DO.OrderItem item in DorderItems)
                {
                    if(item.OrderID == id)
                    {
                        BO.OrderItem item2 = new BO.OrderItem();
                        //לאתחל את האורדר אייטם2 הזה לפי אייטם 
                    }
                }
            }
        }
        catch
        {
            throw new Exception();
        }
    }

    public Order ShippingUpdate(int id)
    {
        throw new NotImplementedException();
    }

    public OrderTracking Track(int id)
    {
        throw new NotImplementedException();
    }

    public Order UpdateDelivery(int id)
    {
        throw new NotImplementedException();
    }
}
