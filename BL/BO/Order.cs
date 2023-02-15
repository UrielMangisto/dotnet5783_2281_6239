using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class Order
{
    /// <summary>
    /// the ID of the order
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// the name of cosotumer
    /// </summary>
    public string? CostumerName { get; set; }
    /// <summary>
    /// the email of costumer
    /// </summary>
    public string? CostumerEmail { get; set; }
    /// <summary>
    /// the addres of costumer
    /// </summary>
    public string? CostumerAddress { get; set; }
    /// <summary>
    /// the status of the order
    /// </summary>
    public Enums.OrderStatus? Status { get; set; }
    /// <summary>
    /// the date the cart bocame order
    /// </summary>
    public DateTime? OrderDate { get; set; }
    /// <summary>
    /// the date the cart shiped
    /// </summary>
    public DateTime? ShipDate { get; set; }
    /// <summary>
    /// the date the cart deliverd
    /// </summary>
    public DateTime? DeliveryDate { get; set; }
    /// <summary>
    /// the list of orderItem
    /// </summary>
    public List<OrderItem?>? OrderItems { get; set; } = new List<OrderItem?>();
    /// <summary>
    /// the total price of order
    /// </summary>
    public double TotalPrice { get; set; }
    public override string ToString()
    {
        string str = $@"
    Id: {Id} 
    Costomer Name: {CostumerName}
    Costomer Email: {CostumerEmail}
    CostumerAddress: {CostumerAddress}
    Status: {Status}
    Order Date: {OrderDate}   
    Ship Date: {ShipDate}
    Delivery Date: {DeliveryDate}
    TotalPrice: {TotalPrice}
    Items:
    ";
        int i = 1;
        if (OrderItems != null)
        {
            foreach (var item in OrderItems)
            {
                str += $@" {i}:
            Id:{item.Id}
            Name:{item.ItemName}
            Price:{item.Price}
            ProductId: {item.ProductId}
            Amount: {item.Amount}
            TotalPrice: {item.TotalPrice}
            ";
                i++;
            }
        }
        return str;
    }
}
