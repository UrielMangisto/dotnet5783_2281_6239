using DO;
using System.Xml.Linq;

namespace DO;
/// <summary>
/// Hear is the struct of the order item,
/// the ID number of itselfe, the product and the order, his name,
/// category, price and the amount.
/// </summary>
public struct OrderItem
{
    public int ID { get; set; }
    public int ProductID { get; set; }
    public int OrderID { get; set; }
    public double Price { get; set; }
    public int Amount { get; set; }
    public override string ToString() => $@"
    Product ID={ProductID}
    Order ID= {OrderID} 
    Price: {Price}
   	Amount  {Amount}
";

}
