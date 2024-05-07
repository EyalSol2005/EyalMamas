using System;
using System.Collections.Generic;

namespace Part3
{
    class Program
    {
        public static void Main(string[] args)
        {
            MakeTest();
        }

        public static void MakeTest()
        {
            LinkedList l1 = new LinkedList();
            l1.Append(1);
            l1.Append(2);
            l1.Append(3);
            l1.Append(4);
            l1.Prepend(7);
            l1.Prepend(8);
            l1.Append(9);

            Console.WriteLine("The Linked List: ");
            Console.WriteLine(l1);

            l1.Sort();

            Console.WriteLine("Sorted Linked List: ");
            Console.WriteLine(l1);

            Console.WriteLine("Max Value: " + l1.GetMaxNode().Value);
            Console.WriteLine("Min Value: " + l1.GetMinNode().Value);

            l1.Pop();
            l1.Unqueue();
            l1.Unqueue();
            l1.Pop();

            Console.WriteLine("\nLinked List After Deleting: ");
            Console.WriteLine(l1);

            Console.WriteLine("Updated Max Value: " + l1.GetMaxNode().Value);
            Console.WriteLine("Updated Min Value: " + l1.GetMinNode().Value);


            var values = l1.ToList();
            Console.WriteLine("\nLinkedList into List:");

            foreach (var value in values)
            {
                Console.Write(value + ".");
            }

            Console.WriteLine("\n\n------------------------\nThe English Numerical Expression (With Bonus - Using Func):");

            long number = 548;
            NumericalExpressionEnglish n1 = new NumericalExpressionEnglish(number);
            Console.WriteLine(n1);
            Console.Write($"Sum of letters from 0 to {number} is: ");
            Console.WriteLine(NumericalExpressionEnglish.SumLetters(number));

        }
    }


    


}
