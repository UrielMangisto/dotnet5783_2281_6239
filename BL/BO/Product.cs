using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace BO;

public class Product
{
    /// <summary>
    /// the ID of the product
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    /// the name of the product
    /// </summary>
    public string? Name { get; set; }
    /// <summary>
    /// the category of the product
    /// </summary>
    public Enums.Category1? Category { get; set; }
    /// <summary>
    /// the price of the product
    /// </summary>
    public double Price { get; set; }
    /// <summary>
    /// the amount of the product in stock
    /// </summary>
    public int InStock { get; set; }
    public override string ToString() => $@"
    Product orderID={ID}: {Name}, 
    Category - {Category}
   	Price: {Price}
    Amount in stock: {InStock}
";
}
