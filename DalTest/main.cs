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
                                       1 to addOrder
                                       2 to deleteOrder
                                       3 to updateOrder
                                       4 to getOrder
                                       5 to getAllOrder
                                            ");
                    choice = int.Parse(Console.ReadLine());
                    switch(choice)
                    {
                        case (int)Enums.OrderChoice.addOrder:

                            break;
                        case (int)Enums.OrderChoice.deleteOrder:

                            break;
                        case (int)Enums.OrderChoice.getOrder:

                            break;
                        case (int)Enums.OrderChoice.updateOrder:

                            break;
                        case (int)Enums.OrderChoice.getAllOrder:

                            break;
                    }
             break;



            }
        }
        while (choice != 0);
    }
}
