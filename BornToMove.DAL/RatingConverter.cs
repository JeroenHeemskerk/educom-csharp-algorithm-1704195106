using System;
using System.Runtime.ExceptionServices;

namespace BornToMove.DAL
{

	public class RatingConverter : IComparer<MoveRating>
	{
        public int Compare(MoveRating? x, MoveRating? y)
        {
			return y.Rating.CompareTo(x.Rating);
        }
	}
}
