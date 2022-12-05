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
