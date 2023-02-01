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
    /// Returns Details Of Order For Manager
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Order DetailsOfOrderForManager(int id);
    /// <summary>
    /// Does a Shipping Update
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Order ShippingUpdate(int id);
    /// <summary>
    /// Updating Delivery
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Order UpdateDelivery(int id);
    /// <summary>
    /// Returns an Order Tracking
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public OrderTracking Track(int id);

    /// <summary>
    /// Returns Details Of Order For Customer
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Order DetailsOfOrderForCustomer(int id);
    /// <summary>
    /// Returns Track List
    /// </summary>
    /// <returns></returns>
    public IEnumerable<OrderTracking> getTracList();
    /// <summary>
    /// Returns List Of Track
    /// </summary>
    /// <param name="ot"></param>
    /// <returns></returns>
    public IEnumerable<TrackLst> getListOfTrack(OrderTracking? ot);
    /// <summary>
    /// Returns the list of the items of the order.
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    public IEnumerable<OrderItem?> getItemListFromOrder(int orderId);
}
