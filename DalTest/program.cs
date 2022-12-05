using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
namespace Dal;
public class program
{
    public static void Main(String[] args)
    {
        int choice;
        int id;
        int temp;
        Order order = new Order();
        DalOrder dalOrder = new DalOrder();

        OrderItem item = new OrderItem();
        DalOrderitem dalItem = new DalOrderitem();

        Product product = new Product();
        DalProduct dalProduct = new DalProduct();
        do
        {
            Console.WriteLine(
                @$"
press 0 to exit
1 to Order
2 to Order item
3 to Product
                                            ");
            choice = int.Parse(Console.ReadLine());

            switch(choice)
            {
                case (int)MainChoise.order:
                    Console.WriteLine(
                        @$"
press 1 to add Order
2 to delete Order
3 to update Order
4 to get Order
5 to get all Order
                                            ");
                    choice = int.Parse(Console.ReadLine());
                    switch(choice)
                    {
                        case (int)OrderChoice.addOrder:
                            order = orderInput(order);

                            dalOrder.Add(order);
                            break;
                        case (int)OrderChoice.deleteOrder:
                            Console.WriteLine("enter id Order");
                            order = orderInput(order);
                            dalOrder.Delete(order);
                            break;
                        case (int)OrderChoice.updateOrder:
                            order = orderInput(order);
                            dalOrder.Update(order);
                            break;
                        case (int)OrderChoice.getOrder:
                            Console.WriteLine("enter id Order");
                            Console.WriteLine(dalOrder.Get(int.Parse(Console.ReadLine())));
                            break;
                        case (int)OrderChoice.getAllOrder:
                            foreach(var ordr in dalOrder.GetAll())
                            {
                                Console.WriteLine(ordr);
                            }
                            break;
                            default:
                            Console.WriteLine("ERROR");
                            break;
                    }
                    break;
                case (int)MainChoise.orderItem:
                    Console.WriteLine(
                        @$"
press 
1 to add OrderItem
2 to delete OrderItem
3 to update OrderItem
4 to get OrderItem
5 to get Specific Item
6 to get Items By Order
7 to get all items
                                            ");
                    choice = int.Parse(Console.ReadLine());
                    switch (choice)
                    {
                        case (int)orderItemChoise.addOrderItem:
                            item = orderItempInput(item);
                            dalItem.Add(item);
                            break;
                        case (int)orderItemChoise.deleteOrderItem:
                            Console.WriteLine("enter Order item id");
                            item = orderItempInput(item);
                            dalItem.Delete(item);
                            break;
                        case (int)orderItemChoise.updateOrderItem:
                            item=orderItempInput(item);
                            dalItem.Update(item);
                            break;
                        case (int)orderItemChoise.getOrderItem:
                            Console.WriteLine("enter Order item id");
                            Console.WriteLine(dalItem.getItemsByOrder(int.Parse(Console.ReadLine())));
                            break;
                        case (int)orderItemChoise.getSpecificItem:
                            Console.WriteLine("enter Product id");
                            temp=int.Parse(Console.ReadLine());
                            Console.WriteLine("enter Order id");
                            Console.WriteLine(dalItem.specificItemGet(temp,int.Parse(Console.ReadLine())));
                            break;
                        case (int)orderItemChoise.getItemsByOrder:
                            order= orderInput(order);
                            Console.WriteLine(dalItem.getItemsByOrder(int.Parse(Console.ReadLine())));
                            break;
                        case (int)orderItemChoise.getAllItems:
                            foreach( var itm in dalItem.GetAll())
                            {
                                Console.WriteLine(itm);
                            }

                            break;
                        default:

                            break;
                    }
                    break;
                case (int)MainChoise.product:
                    Console.WriteLine(
                        @$"
press 
1 to add Product
2 to delete Product
3 to update Product
4 to get Product
5 to get all Product
                                            ");
                    choice = int.Parse(Console.ReadLine());
                    switch (choice)
                    {
                        case (int)ProductChoice.addProduct:
                            product = productInput(product);
                            dalProduct.Add(product);

                            break;
                        case (int)ProductChoice.deleteProduct:
                            Console.WriteLine("enter Product id:");
                            product = productInput(product);
                            dalProduct.Delete(product);
                            
                            break;
                        case (int)ProductChoice.updateProduct:
                            product= productInput(product);
                            dalProduct.Update(product);

                            break;
                        case (int)ProductChoice.getProduct:
                            Console.WriteLine("enter Product id:");
                            id = int.Parse(Console.ReadLine());
                            Console.WriteLine(dalProduct.Get(id));
                            break;
                        case (int)ProductChoice.getAllProduct:                          
                            foreach(Product p in dalProduct.GetAll())
                            {
                                Console.WriteLine(p);
                            }
                            break;

                        default:
                            break;
                    }
                    break;

                default:
                    break;
            }
        }
        while (choice != 0);
    }

    private static OrderItem orderItempInput(OrderItem item)
    {
        Console.WriteLine("enter Product id");
        item.ProductID = int.Parse(Console.ReadLine());

        Console.WriteLine("enter Order id");
        item.OrderID = int.Parse(Console.ReadLine());

        Console.WriteLine("enter Order item price");
        item.Price = int.Parse(Console.ReadLine());

        Console.WriteLine("enter Order item amount");
        item.Amount = int.Parse(Console.ReadLine());

        return item;
    }

    private static Order orderInput(Order order)
    {
        Console.WriteLine("enter your name");
        order.CostumerName = Console.ReadLine();

        Console.WriteLine("enter your address");
        order.CostumerAddress = Console.ReadLine();

        Console.WriteLine("enter your email");
        order.CostumerEmail = Console.ReadLine();

        return order;
    }

    private static Product productInput(Product product)
    {
        int cat;

        Console.WriteLine("enter Product name");
        product.Name = (Console.ReadLine());

        Console.WriteLine("enter category");
        int.TryParse(Console.ReadLine(), out cat);
        product.Category = (Category)cat;

        Console.WriteLine("enter Product price");
        product.Price = int.Parse(Console.ReadLine());

        Console.WriteLine("how many products are in stock?");
        product.InStock = int.Parse(Console.ReadLine());

        return product;
    }
}
