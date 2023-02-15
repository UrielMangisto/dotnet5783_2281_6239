using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using System.Xml.Linq;

namespace BO;

public class Cart
{
    /// <summary>
    /// the name of the cotumer
    /// </summary>
    public string? CostumerName { get; set; }
    /// <summary>
    /// the email of the costumer
    /// </summary>
    public string? CostumerEmail { get; set; }
    /// <summary>
    /// the address of the costumer 
    /// </summary>
    public string? CostumerAddress { get; set; }
    /// <summary>
    /// the list of the orderitem
    /// </summary>
    public List<OrderItem?>? OrderItems { get; set; }// = new List<OrderItem?>();
    /// <summary>
    /// the total price of the cart
    /// </summary>
    public double TotalPrice { get; set; }
    public override string ToString() { string str = $@"
    Costumer Name  :  {CostumerName}, 
    Costumer Email :  {CostumerEmail}
   	Costumer Address: {CostumerAddress}
    ";
        int i = 1;
        if (OrderItems != null)
        {
            foreach(var item in OrderItems)
            {
                str += $@" {i++}
            Id:{item.Id}
            Name:{item.ItemName}
            Price:{item.Price}
            ProductId:{item.ProductId}
            Amount:{item.Amount}
            TotalPrice:{item.TotalPrice}
            ";            
            }
        }
        return str;
    }
}
