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
1 to order
2 to order item
3 to product
                                            ");
            choice = int.Parse(Console.ReadLine());

            switch(choice)
            {
                case (int)MainChoise.order:
                    Console.WriteLine(
                        @$"
press 1 to add order
2 to delete order
3 to update order
4 to get order
5 to get all order
                                            ");
                    choice = int.Parse(Console.ReadLine());
                    switch(choice)
                    {
                        case (int)OrderChoice.addOrder:
                            order = orderInput(order);

                            dalOrder.Add(order);
                            break;
                        case (int)OrderChoice.deleteOrder:
                            Console.WriteLine("enter id order");
                            order = orderInput(order);
                            dalOrder.Delete(order);
                            break;
                        case (int)OrderChoice.updateOrder:
                            order = orderInput(order);
                            dalOrder.Update(order);
                            break;
                        case (int)OrderChoice.getOrder:
                            Console.WriteLine("enter id order");
                            dalOrder.Get(int.Parse(Console.ReadLine()));
                            break;
                        case (int)OrderChoice.getAllOrder:
                            dalOrder.GetAll();
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
1 to add orderItem
2 to delete orderItem
3 to update orderItem
4 to get orderItem
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
                            Console.WriteLine("enter order item id");
                            item = orderItempInput(item);
                            dalItem.Delete(item);
                            break;
                        case (int)orderItemChoise.updateOrderItem:
                            item=orderItempInput(item);
                            dalItem.Update(item);
                            break;
                        case (int)orderItemChoise.getOrderItem:
                            Console.WriteLine("enter order item id");
                            dalItem.getItemsByOrder(int.Parse(Console.ReadLine()));
                            break;
                        case (int)orderItemChoise.getSpecificItem:
                            Console.WriteLine("enter product id");
                            temp=int.Parse(Console.ReadLine());
                            Console.WriteLine("enter order id");
                            dalItem.specificItemGet(temp,int.Parse(Console.ReadLine()));
                            break;
                        case (int)orderItemChoise.getItemsByOrder:
                            order= orderInput(order);
                            dalItem.getItemsByOrder(int.Parse(Console.ReadLine()));
                            break;
                        case (int)orderItemChoise.getAllItems:
                            dalItem.GetAll(); 

                            break;
                        default:

                            break;
                    }
                    break;
                case (int)MainChoise.product:
                    Console.WriteLine(
                        @$"
press 
1 to add product
2 to delete product
3 to update product
4 to get product
5 to get all product
                                            ");
                    choice = int.Parse(Console.ReadLine());
                    switch (choice)
                    {
                        case (int)ProductChoice.addProduct:
                            product = productInput(product);
                            dalProduct.Add(product);

                            break;
                        case (int)ProductChoice.deleteProduct:
                            Console.WriteLine("enter product id:");
                            product = productInput(product);
                            dalProduct.Delete(product);
                            
                            break;
                        case (int)ProductChoice.updateProduct:
                            product= productInput(product);
                            dalProduct.Update(product);

                            break;
                        case (int)ProductChoice.getProduct:
                            Console.WriteLine("enter product id:");
                            id = int.Parse(Console.ReadLine());
                            dalProduct.Get(id);
                            break;
                        case (int)ProductChoice.getAllProduct:
                            Product[] products = (Product[])dalProduct.GetAll();
                            foreach(Product p in products)
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
        Console.WriteLine("enter product id");
        item.ProductID = int.Parse(Console.ReadLine());

        Console.WriteLine("enter order id");
        item.OrderID = int.Parse(Console.ReadLine());

        Console.WriteLine("enter order item price");
        item.Price = int.Parse(Console.ReadLine());

        Console.WriteLine("enter order item amount");
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

        Console.WriteLine("enter product name");
        product.Name = (Console.ReadLine());

        Console.WriteLine("enter category");
        int.TryParse(Console.ReadLine(), out cat);
        product.Category = (Category)cat;

        Console.WriteLine("enter product price");
        product.Price = int.Parse(Console.ReadLine());

        Console.WriteLine("how many products are in stock?");
        product.InStock = int.Parse(Console.ReadLine());

        return product;
    }
}
