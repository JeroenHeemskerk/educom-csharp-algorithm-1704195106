using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Org
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Please input Number");
            int number = Convert.ToInt32(Console.ReadLine());
            List<int> thisList = MakeList(number);
            ShowList("List", thisList);
            Stopwatch stopWatch = new Stopwatch();
            ShiftHighestSort newSort = new ShiftHighestSort();
            stopWatch.Start();
            List<int> sortedList = newSort.Sort(thisList);
            ShowList("sortedList", sortedList);
            stopWatch.Stop();
            Console.Write(stopWatch.ElapsedMilliseconds);
            stopWatch.Reset();
            stopWatch.Start();
            RotateSort<int> rotate = new RotateSort<int>();
            List<int> rotatedList = rotate.Sort(thisList, Comparer<int>.Default);
            ShowList("rotatedList", rotatedList);
            Console.Write(stopWatch.ElapsedMilliseconds);
            stopWatch.Stop();
        }

        static List<int> MakeList(int N)
        {
            var rand = new Random();
            var newList = new List<int>();

            for (int i = 0; i < N; i++)
            {
                newList.Add(rand.Next(-99, 99));
            }

            return newList;
        }
        public static void ShowList(string label, List<int> theList)
        {
            int count = theList.Count;
            if (count > 200)
            {
                count = 200; // Do not show more than 300 numbers
            }
            Console.WriteLine();
            Console.Write(label);
            Console.Write(':');
            for (int index = 0; index < count; index++)
            {
                if (index % 20 == 0) // when index can be divided by 20 exactly, start a new line
                {
                    Console.WriteLine();
                }
                Console.Write(string.Format("{0,3}, ", theList[index]));  // Show each number right aligned within 3 characters, with a comma and a space
            }
            Console.WriteLine();
        }
    }
}
