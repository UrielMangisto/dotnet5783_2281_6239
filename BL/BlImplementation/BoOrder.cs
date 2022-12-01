using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using BO;
/// <summary>
/// the implementation of the order
/// </summary>
namespace BlImplementation;

internal class BoOrder : IOrder
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public IEnumerable<OrderForList> GetOrders()
    {
        throw new NotImplementedException();
    }

    public Order DetailsOfOrder(int id)
    {
        throw new NotImplementedException();
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
