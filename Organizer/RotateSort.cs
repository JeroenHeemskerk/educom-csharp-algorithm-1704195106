using System;
using System.Collections.Generic;

namespace Org
{
	public class RotateSort<T>
	{
        public IComparer<T> Comparer { get; set; }

        private List<T> array = new List<T>();

        public List<T> Sort(List<T> input, IComparer<T> comparer)
        {
            this.array = new List<T>(input);
            this.Comparer = comparer;
            this.array = SortFunction(array);
            return this.array;
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
        private List<T> SortFunction(List<T> array)
        {
            if (array.Count < 2){
                return array;
            }
            T pivot = array[0];
            var sortedArray = new List<T>();
            var lower = new List<T>();
            var higher = new List<T>();
            for (int i = 1; i < array.Count; i++)
            {
                if (Comparer.Compare(pivot, array[i]) > 0)
                {
                    lower.Add(array[i]);
                } else
                {
                    higher.Add(array[i]);
                }
            }
            foreach (T value in SortFunction(lower))
            {
                sortedArray.Add(value);
            }
            sortedArray.Add(pivot);
            foreach (T value in SortFunction(higher))
            {
                sortedArray.Add(value);
            }
            return sortedArray;
        }
    }
}
