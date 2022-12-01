using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class ProductForList
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public Enums.Category Category { get; set; }


    //to print the object
    public override string ToString() => $@"
    Product Id - {Id} 
    Name: {Name} 
    Category: {Category}
    Price: {Price}
    ";

}
