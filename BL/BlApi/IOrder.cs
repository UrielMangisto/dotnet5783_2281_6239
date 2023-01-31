using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace BlApi;
/// <summary>
/// The implementation of the logic Order entity
/// </summary>
public interface IOrder
{
    /// <summary>
    /// Returns Order List
    /// </summary>
    /// <returns></returns>
    public IEnumerable<OrderForList?> GetOrderList();
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Order DetailsOfOrderForManager(int id);
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Order DetailsOfOrderForCustomer(int id);
    IEnumerable<OrderTracking> getTracList();
}
