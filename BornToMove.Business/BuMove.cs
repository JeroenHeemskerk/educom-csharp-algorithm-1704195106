using BornToMove.DAL;
using System.Net.WebSockets;

namespace BornToMove.Business
{
    public class BuMove
    {
        MoveCrud moveCrud { set; get; }

        public BuMove() { moveCrud = new MoveCrud(); }

        public bool TryToMakeAMove(Move move)
        {
            if (this.moveCrud.readMoveByName(move.name) is null)
            {
                this.moveCrud.createMove(move);
                return true;
            }
            return false; 
        }

        public (Move, MoveRating) getRandomMove()
        {
            Random rand = new Random();
            (List<Move> allMoves, IEnumerable<MoveRating> Ratings) = getAllMoves();
            int moveNr = rand.Next(0, allMoves.Count - 1);
            Move thisMove = allMoves[moveNr]; 
            return (allMoves[moveNr], Ratings.ElementAt(moveNr));
        }

        public (List<Move>, IEnumerable<MoveRating>) getAllMoves()
        {
            List<Move> moves = moveCrud.readAllMoves();
            IEnumerable<MoveRating> averageRatings = moveCrud.readAllMovesWithRatings();
            return (moves, averageRatings);
        }

        public bool tryToUpdateMove(String oldName, Move move)
        {
            if (moveCrud.readMoveByName(move.name) is not null)
            {
                moveCrud.updateMove(oldName, move.name, move.description, move.sweatrate);
                return true;
            }
            return false;
        }

        public (Move, MoveRating) getLastMove()
        {
            (List<Move> allMoves, IEnumerable<MoveRating> Ratings) = getAllMoves();
            int moveNr = allMoves.Count - 1;
            Move thisMove = allMoves[moveNr];
            return (allMoves[moveNr], Ratings.ElementAt(moveNr));
        }

        public (Move, MoveRating) getMoveById(int Id)
        {
            (List<Move> allMoves, IEnumerable<MoveRating> Ratings) = getAllMoves();
            int moveNr = allMoves.Count - 1;
            Move thisMove = allMoves[moveNr];
            return (allMoves[Id - 1], Ratings.ElementAt(Id - 1));
        }

        public void AddMovesIfEmpty()
        {
            (List<Move> allMoves, IEnumerable<MoveRating> Ratings) = getAllMoves();
            if (allMoves.Count() == 0)
            {
                TryToMakeAMove(new Move("Push up", "Ga horizontaal liggen op teentoppen en handen. " +
                    "Laat het lijf langzaam zakken tot de neus de grond bijna raakt. " +
                    "Duw het lijf terug nu omhoog tot de ellebogen bijna gestrekt zijn. Vervolgens weer laten zakken. " +
                    "Doe dit 20 keer zonder tussenpauzes", 3));
                TryToMakeAMove(new Move("Planking", "Ga horizontaal liggen op teentoppen en onderarmen. " +
                    "Houdt deze positie 1 minuut vast", 3));
                TryToMakeAMove(new Move("Squat", "Ga staan met gestrekte armen. " +
                    "Zak door de knieën tot de billen de grond bijna raken. " +
                    "Ga weer volledig gestrekt staan. Herhaal dit 20 keer zonder tussenpauzes", 5));
            }
        }

        public bool TryToMakeRating(MoveRating newRating, String moveName)
        {
            if (moveCrud.readMoveByName(moveName) == newRating.Move)
            {
                moveCrud.updateMove(moveName, rating: newRating);
                moveCrud.createRating(newRating);
                return true;
            }
            return false;
        }
    }
}
