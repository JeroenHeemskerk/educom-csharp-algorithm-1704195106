using System;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;

namespace Organizer
{
	public class ShiftHighestSort
    {
        private List<int> array = new List<int>();

        public List<int> Sort(List<int> input)
        {

            array = new List<int>(input);
            for (int i = 0; i < array.Count - 2; i++)
            {
                for (int j = 0; j < array.Count - i - 1; j++)
                {
                    if(Compare(j))
                    {
                        int first = array[j];
                        int second = array[j + 1];
                        array[j] = second;
                        array[j + 1] = first;
                    }
                }
            }
            return array;
        }

        private bool Compare(int j)
        {
            if (array[j] > array[j + 1])
            {
                return true;
            }
            return false;
        }    
    }
}
