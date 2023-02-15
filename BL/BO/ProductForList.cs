using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class ProductForList
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

    public override string ToString() => $@"
    Product Id - {Id} 
    Name: {Name} 
    Category: {Category}
    Price: {Price}
    ";

}
