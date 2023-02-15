
namespace DO;
/// <summary>
/// Hear is the struct of the Order,
/// it's orderID number, Costumer Name, Costumer Email,Costumer Adress,
/// Order Date, Ship Date and the Delivery Date.
/// </summary>
public struct Order
{
    /// <summary>
    /// the ID of the order
    /// </summary>
    public int orderID { get; set; }
    /// <summary>
    /// the name of the costumer
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
    /// the date the cart became to order
    /// </summary>
    public DateTime? OrderDate { get; set; }
    /// <summary>
    /// the date the order shiped
    /// </summary>
    public DateTime? ShipDate { get; set; }
    /// <summary>
    /// the date the order deliverd
    /// </summary>
    public DateTime? DeliveryDate { get; set; }
    public override string ToString() => $@"
    Order orderID={orderID}: {CostumerName}, 
    CostumerEmail - {CostumerEmail}
   	CostumerAdress {CostumerAddress}
   	OrderDate {OrderDate}
    ShipDate {ShipDate}
    DeliveryDate {DeliveryDate}

";

}
