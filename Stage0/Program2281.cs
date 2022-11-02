using System;

namespace Stage0 // Note: actual namespace depends on the project name.
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Wellcome6239();
            Wellcome2281();
            Console.ReadKey();
        }

        static partial void Wellcome6239();
        private static void Wellcome2281()
        {

            string name;
            Console.Write("Enter your name: ");
            name = Console.ReadLine();
            Console.WriteLine("{0}, wellcome to my first console application", name);
        }
    }
}