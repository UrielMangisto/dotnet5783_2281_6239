using DO;
using System.Xml.Linq;

namespace DO;
/// <summary>
/// Hear is the struct of the Order item,
/// the orderID number of itselfe, the Product and the Order, his name,
/// category, price and the amount.
/// </summary>
public struct OrderItem
{
    /// <summary>
    /// the ID of the Item
    /// </summary>
    public int orderItemID { get; set; }
    /// <summary>
    /// the ID of the product 
    /// </summary>
    public int ProductID { get; set; }
    /// <summary>
    /// the ID of the product 
    /// </summary>
    public int OrderID { get; set; }
    /// <summary>
    /// the price of the item
    /// </summary>
    public double Price { get; set; }
    /// <summary>
    /// the amount of the item
    /// </summary>
    public int Amount { get; set; }
    public override string ToString() => $@"
    Product orderID={ProductID}
    Order orderID= {OrderID} 
    Price: {Price}
   	Amount  {Amount}
";

}
