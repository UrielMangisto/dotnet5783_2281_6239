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
                case (int)Enums.mainChoise.order:
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
                    }
                    break;
                case (int)Enums.mainChoise.orderItem:
                    Console.WriteLine(@$"press 
                                       1 to add orderItem
                                       2 to delete orderItem
                                       3 to update orderItem
                                       4 to get orderItem
                                       5 to get all orderItem
                                            ");
                    choice = int.Parse(Console.ReadLine());



            }
        }
        while (choice != 0);
    }
}
