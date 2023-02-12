﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class ProductItem
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public double Price { get; set; }
    public Enums.Category? Category { get; set; }
    public int Amount { get; set; }
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
