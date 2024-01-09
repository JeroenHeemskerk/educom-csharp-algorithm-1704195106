using System;

namespace BornToMove
{
	public class MoveRating
	{
        public int Id { get; set; } = 0;
        public Move Move { set; get; }
        public double Rating { set; get; }
        public double Vote { set; get; }

        /// <summary>
        /// ONLY FOR ENITTY FRAMEWORK!
        /// </summary>
        private MoveRating() { }            
        
        public MoveRating(Move Move, double Rating, double Vote)
        {
            this.Move = Move;
            this.Rating = Rating;
            this.Vote = Vote;
        }
	}
}


