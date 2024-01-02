using System;
using System.Collections.Generic;

namespace Organizer
{
	public class RotateSort
	{

        private List<int> array = new List<int>();

        public List<int> Sort(List<int> input)
        {
            array = new List<int>(input);

            array = SortFunction(array);
            return array;
        }

        private List<int> Partitioning(List<int> array, bool higher)
        {
            int pivot = array[0];
            var newArray = new List<int>();
            if (higher) 
            {
                for (int i = 1; i < array.Count; i++)
                    if (array[i] > pivot)
                    {
                        newArray.Add(array[i]);
                    }
            }
            else
            {
                for (int i = 1; i < array.Count; i++)
                    if (array[i] <= pivot)
                    {
                        newArray.Add(array[i]);
                    }
            }
            return newArray;
        }

        /// 
        /// Partition the array in a group 'low' digits (e.a. lower than a choosen pivot) and a group 'high' digits
        /// </summary>
        /// <param name="low">De index within this.array to start with</param>
        /// <param name="high">De index within this.array to stop with</param>
        /// <returns>The index in the array of the first of the 'high' digits</returns>
        private List<int> SortFunction(List<int> array)
        {
            if (array.Count < 2){
                return array;
            }
            int pivot = array[0];
            var sortedArray = new List<int>();
            var lower = new List<int>();
            var higher = new List<int>();
            for (int i = 1; i < array.Count; i++)
            {
                if (array[i] < pivot)
                {
                    lower.Add(array[i]);
                } else
                {
                    higher.Add(array[i]);
                }
            }
            foreach (int value in SortFunction(lower))
            {
                sortedArray.Add(value);
            }
            sortedArray.Add(pivot);
            foreach (int value in SortFunction(higher))
            {
                sortedArray.Add(value);
            }
            return sortedArray;
        }
    }
}
