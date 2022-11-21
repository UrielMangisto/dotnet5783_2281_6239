
namespace DO;
/// <summary>
/// Hear is the struct of the product of our store,
/// it's ID number, name, category,price and 
/// the current amount in the store.
/// </summary>
public struct Product
{
    public int ID { get; set; }
    public string Name { get; set; }
    public Category Category { get; set; }
    public double Price { get; set; }   
    public int InStock { get; set; }
    public override string ToString() => $@"
    Product ID={ID}: {Name}, 
    Category - {Category}
   	Price: {Price}
    Amount in stock: {InStock}
";

}
