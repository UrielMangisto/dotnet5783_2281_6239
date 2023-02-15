using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class OrderForList
{
    /// <summary>
    /// the ID of the order
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// the name of the costumer
    /// </summary>
    public string? CostomerName { get; set; }
    /// <summary>
    /// the status of the order
    /// </summary>
    public Enums.OrderStatus? Status { get; set; }
    /// <summary>
    /// the amount of the items
    /// </summary>
    public int AmountOfItems { get; set; }
    /// <summary>
    /// the total price
    /// </summary>
    public double TotalPrice { get; set; }

    public override string ToString() => $@"
    Id : {Id} 
    Costomer Name : {CostomerName}
    Status: {Status}
    Amount Of Items: {AmountOfItems}
    Total Price: {TotalPrice}
    ";

}
