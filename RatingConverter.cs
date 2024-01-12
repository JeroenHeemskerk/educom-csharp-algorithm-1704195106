using System;

namespace BornToMove.DAL
{

	public class RatingConverter : IComparer<MoveRating>
	{
		public int difference(MoveRating first, MoveRating second)
		{
			return first.Rating.CompareTo(second.Rating);
		}
	}
}
