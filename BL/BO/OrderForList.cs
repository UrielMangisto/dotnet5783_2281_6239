using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class OrderForList
{
    public int Id { get; set; }
    public string? CostomerName { get; set; }
    public Enums.OrderStatus? Status { get; set; }
    public int AmountOfItems { get; set; }
    public double TotalPrice { get; set; }

    public override string? ToString() => $@"
    Id : {Id} 
    Costomer Name : {CostomerName}
    Status: {Status}
    Amount Of Items: {AmountOfItems}
    Total Price: {TotalPrice}
    ";

}
