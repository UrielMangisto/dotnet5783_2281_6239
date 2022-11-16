using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using Dal;

public class program
{
    public static void Main(String[] args)
    {
        int choice;
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
2 to order it
3 to product
                                            ");
            choice = int.Parse(Console.ReadLine());

            switch(choice)
            {
                case (int)Enums.MainChoise.order:
                    Console.WriteLine(@$"press 
                                       1 to add order
                                       2 to delete order
                                       3 to update order
                                       4 to get order
                                       5 to get all order
                                            ");
                    choice = int.Parse(Console.ReadLine());
                    switch(choice)
                    {
                        case (int)Enums.OrderChoice.addOrder:
                            order = orderInput(order);

                            dalOrder.addNewOrder(order);
                            break;
                        case (int)Enums.OrderChoice.deleteOrder:
                            Console.WriteLine("enter id order");
                            dalOrder.deleteOrder(int.Parse(Console.ReadLine()));
                            break;
                        case (int)Enums.OrderChoice.updateOrder:
                            order = orderInput(order);
                            dalOrder.updateOrder(order);
                            break;
                        case (int)Enums.OrderChoice.getOrder:
                            Console.WriteLine("enter id order");
                            dalOrder.getOrder(int.Parse(Console.ReadLine()));
                            break;
                        case (int)Enums.OrderChoice.getAllOrder:
                            dalOrder.getAllOrders();
                            break;
                            default:
                            Console.WriteLine("ERROR");
                            break;
                    }
                    break;
                case (int)Enums.MainChoise.orderItem:
                    Console.WriteLine(@$"press 
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
                        case (int)Enums.orderItemChoise.addOrderItem:
                            item = orderItempInput(item);
                            dalItem.addItem(item);
                            break;
                        case (int)Enums.orderItemChoise.deleteOrderItem:
                            Console.WriteLine("enter order item id");
                            dalItem.deleteItem(int.Parse(Console.ReadLine()));
                            break;
                        case (int)Enums.orderItemChoise.updateOrderItem:
                            item=orderItempInput(item);
                            dalItem.updateItem(item);
                            break;
                        case (int)Enums.orderItemChoise.getOrderItem:
                            Console.WriteLine("enter order item id");
                            dalItem.getOrderItem(int.Parse(Console.ReadLine()));
                            break;
                        case (int)Enums.orderItemChoise.getSpecificItem:
                            Console.WriteLine("enter product id");
                            temp=int.Parse(Console.ReadLine());
                            Console.WriteLine("enter order id");
                            dalItem.specificItemGet(temp,int.Parse(Console.ReadLine()));
                            break;
                        case (int)Enums.orderItemChoise.getItemsByOrder:
                            order= orderInput(order);
                            dalItem.getItemsByOrder(order);
                            break;
                        case (int)Enums.orderItemChoise.getAllItems:
                            dalItem.getAllItems(); 

                            break;
                        default:

                            break;
                    }
                    break;
                case (int)Enums.MainChoise.product:
                    Console.WriteLine(@$"press 
                                       1 to add product
                                       2 to delete product
                                       3 to update product
                                       4 to get product
                                       5 to get all product
                                            ");
                    choice = int.Parse(Console.ReadLine());
                    switch (choice)
                    {
                        case (int)Enums.ProductChoice.addProduct:

                            break;
                        case (int)Enums.ProductChoice.deleteProduct:

                            break;
                        case (int)Enums.ProductChoice.updateProduct:

                            break;
                        case (int)Enums.ProductChoice.getProduct:

                            break;
                        case (int)Enums.ProductChoice.getAllProduct:

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
}
