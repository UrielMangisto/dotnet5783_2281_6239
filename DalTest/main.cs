using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace Dal;

internal class program
{
    public static void main(String[] args)
    {
        int choice;
        do
        {
            Console.WriteLine(@$"press 0 to exit
                                       1 to order
                                       2 to order item
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

                            break;
                        case (int)Enums.OrderChoice.deleteOrder:
                            
                            break;
                        case (int)Enums.OrderChoice.updateOrder:

                            break;
                        case (int)Enums.OrderChoice.getOrder:

                            break;
                        case (int)Enums.OrderChoice.getAllOrder:

                            break;
                            default:    

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

                            break;
                        case (int)Enums.orderItemChoise.deleteOrderItem:

                            break;
                        case (int)Enums.orderItemChoise.updateOrderItem:

                            break;
                        case (int)Enums.orderItemChoise.getOrderItem:

                            break;
                        case (int)Enums.orderItemChoise.getSpecificItem:

                            break;
                        case (int)Enums.orderItemChoise.getItemsByOrder:

                            break;
                        case (int)Enums.orderItemChoise.getAllItems:

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
}
