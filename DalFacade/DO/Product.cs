
namespace DO;
/// <summary>
/// Hear is the struct of the Product of our store,
/// it's orderID number, name, category,price and 
/// the current amount in the store.
/// </summary>
public struct Product
{
    public int productID { get; set; }
    public string? Name { get; set; }
    public Category? Category { get; set; }
    public double Price { get; set; }   
    public int InStock { get; set; }
    public override string ToString() => $@"
    Product orderID={productID}: {Name}, 
    Category - {Category}
   	Price: {Price}
    Amount in stock: {InStock}
";

}
