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
                                       1 to
                                       2 to
                                       3 to
                                       4 to
                                       5 to
                                       6 to

                                            ");
            choice = int.Parse(Console.ReadLine());

            switch(choice)
            {
                
            }
        }
        while (choice == 0);
    }
}
