using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class Order
{
    public int Id { get; set; }
    public string CostomerName { get; set; }
    public string CostomerEmail { get; set; }
    public string CostomerAdress { get; set; }
    public Enums.OrderStatus Status { get; set; }
    public DateTime? OrderDate { get; set; }
    public DateTime? ShipDate { get; set; }
    public DateTime? DeliveryDate { get; set; }
    public List<OrderItem> OrderItems { get; set; }
    public double TotalPrice { get; set; }
    public override string ToString()
    {
        string str = $@"
    Id: {Id} 
    Costomer Name: {CostomerName}
    Costomer Email: {CostomerEmail}
    CostomerAdress: {CostomerAdress}
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
