using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class ProductItem
{
    /// <summary>
    /// the ID of the product
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// the name of the product
    /// </summary>
    public string? Name { get; set; }
    /// <summary>
    /// the price of the product
    /// </summary>
    public double Price { get; set; }
    /// <summary>
    /// the category of the product
    /// </summary>
    public Enums.Category? Category { get; set; }
    /// <summary>
    /// the amount of the product
    /// </summary>
    public int Amount { get; set; }
    /// <summary>
    /// the amount of the product
    /// </summary>
    public bool InStock { get; set; }

    public override string ToString() => $@"
    Id - {Id} 
    Name: {Name}
    Category: {Category}
    Price: {Price}
    Amount: {Amount}
    Amount in stock: {InStock}
    ";

}
