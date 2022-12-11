
namespace DO;
/// <summary>
/// Hear is the struct of the order,
/// it's ID number, Costumer Name, Costumer Email,Costumer Adress,
/// Order Date, Ship Date and the Delivery Date.
/// </summary>
public struct Order
{
    public int ID { get; set; }
    public string? CostumerName { get; set; }
    public string? CostumerEmail { get; set; }
    public string? CostumerAddress { get; set; }  
    public DateTime? OrderDate { get; set; } 
    public DateTime? ShipDate { get; set; }  
    public DateTime? DeliveryDate { get; set; }
    public override string ToString() => $@"
    Order ID={ID}: {CostumerName}, 
    CostumerEmail - {CostumerEmail}
   	CostumerAdress {CostumerAddress}
   	OrderDate {OrderDate}
    ShipDate {ShipDate}
    DeliveryDate {DeliveryDate}

";

}
