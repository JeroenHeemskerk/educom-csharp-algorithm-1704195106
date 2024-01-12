using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace BornToMove.DAL {
    public class MoveCrud
    {
        public MoveCrud() { context = new MoveContext(); }
        public MoveContext context { get; set; }
        public void createMove(Move newMove)
        {
            context.Moves.Add(newMove);
            context.SaveChanges();
        }

        public void updateMove(int Id,
            String newName = null, String newDescription = null, int newSweatrate = 0, MoveRating rating = null)
        {
            Move? oldMove = context.Moves.FirstOrDefault(Move => Move.Id == Id);
            if (oldMove != null)
            {
                if (newName is not null)
                {
                    oldMove.name = newName;
                }
                if (newDescription is not null)
                {
                    oldMove.description = newDescription;
                }
                if (newSweatrate != 0)
                {
                    oldMove.sweatrate = newSweatrate;
                }
                if (rating is not null)
                {
                    oldMove.Ratings.Add(rating);
                }
                context.SaveChanges();
            }
        }

        public void delete(int Id)
        {
            Move? move = context.Moves.FirstOrDefault(Move => Move.Id == Id);
            if (move is not null)
            {
                context.Moves.Remove(move);
            }
            context.SaveChanges();
        }

        public Move? readMoveByName(String name)
        {
            Move? move = context.Moves.FirstOrDefault(Move => Move.name == name);
            return move;
        }

        public List<Move> readAllMoves()
        {
            List<Move> allMoves = context.Moves.ToList();
            return allMoves;
        }

        public void createRating(MoveRating rating)
        {
            Move? assocMove = context.Moves.FirstOrDefault(Move => Move.Id == rating.Move.Id);
            if (assocMove != null) {
                assocMove.Ratings.Add(rating);
                context.MoveRating.Add(rating);
            }
        }

        public void addRatingToMove(MoveRating rating, String moveName)
        {
            Move? move = context.Moves.FirstOrDefault(Move => Move.name == rating.Move.name);
            if (move is not null)
            {
                move.Ratings.Add(rating);
                context.SaveChanges();
            }
        }

        public double readRatingByMove(String name)
        {
            double rating = 0;
            IEnumerable<MoveRating> ratingsByMove = context.MoveRating.Where(rating => rating.Move.name == name);
            if (ratingsByMove.Any())
            {
                rating = ratingsByMove.Select(rating => rating.Rating).Average();
            }
            return rating;
        }

        public double readVoteByMove(String name)
        {
            double vote = 0;
            IEnumerable<MoveRating> votesByMove = context.MoveRating.Where(rating => rating.Move.name == name);
            if (votesByMove.Any())
            {
                vote = votesByMove.Select(rating => rating.Vote).Average();
            }
            return vote;
        }

        public IEnumerable<MoveRating> readAllMovesWithRatings()
        {
            var MovesWithRating = context.Moves.Include(m => m.Ratings);
            var averageRatings = MovesWithRating.Select(m => new BornToMove.MoveRating()
            {
                Move = m,
                Rating = m.Ratings.Select(rating => rating.Rating).DefaultIfEmpty().Average(),
                Vote = m.Ratings.Select(rating => rating.Vote).DefaultIfEmpty().Average()
            });

            
            return averageRatings;
        }


    }
}

