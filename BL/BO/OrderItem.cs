using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class OrderItem
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string? ItemName { get; set; }
    public double Price { get; set; }
    public int Amount { get; set; }
    public double TotalPrice { get; set; }

    public override string? ToString() => $@"
    Id: {Id} 
    Costomer Name: {ItemName}
    Product Id: {ProductId}
    Price: {Price}
    Amount: {Amount}
    TotalPrice: {TotalPrice}
    ";

}
