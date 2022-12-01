using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace BlApi;
/// <summary>
/// 
/// </summary>
public interface IOrder
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public IEnumerable<OrderForList> GetOrders();
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Order DetailsOfOrder(int id);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Order ShippingUpdate(int id);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Order UpdateDelivery(int id);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public OrderTracking Track(int id);
}
