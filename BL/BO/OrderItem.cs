using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class OrderItem
{
    /// <summary>
    /// the ID of the item
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// the ID of the product
    /// </summary>
    public int ProductId { get; set; }
    /// <summary>
    /// the name of the item
    /// </summary>
    public string? ItemName { get; set; }
    /// <summary>
    /// the price of the item
    /// </summary>
    public double Price { get; set; }
    /// <summary>
    /// the amount of the item
    /// </summary>
    public int Amount { get; set; }
    /// <summary>
    /// the price of the amunt of the items
    /// </summary>
    public double TotalPrice { get; set; }

    public override string ToString() => $@"
    Id: {Id} 
    Costomer Name: {ItemName}
    Product Id: {ProductId}
    Price: {Price}
    Amount: {Amount}
    TotalPrice: {TotalPrice}
    ";

}
