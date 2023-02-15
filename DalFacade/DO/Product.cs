
namespace DO;
/// <summary>
/// Hear is the struct of the Product of our store,
/// it's ID number, name, category,price and 
/// the current amount in the store.
/// </summary>
public struct Product
{
    /// <summary>
    /// the ID of the the product
    /// </summary>
    public int productID { get; set; }
    /// <summary>
    /// the name of the product
    /// </summary>
    public string? Name { get; set; }
    /// <summary>
    /// the category of the book
    /// </summary>
    public Category? Category { get; set; }
    /// <summary>
    /// the price of the product
    /// </summary>
    public double Price { get; set; }
    /// <summary>
    /// the amount in the stock
    /// </summary>
    public int InStock { get; set; }
    public override string ToString() => $@"
    Product orderID={productID}: {Name}, 
    Category - {Category}
   	Price: {Price}
    Amount in stock: {InStock}
";

}
