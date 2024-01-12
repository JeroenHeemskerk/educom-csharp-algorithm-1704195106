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

        public MoveRating getRandomMove()
        {
            Random rand = new Random();
            IEnumerable<MoveRating> Ratings = getAllMoves();
            int moveNr = rand.Next(0, Ratings.Count() - 1);
            return Ratings.ElementAt(moveNr);
        }

        public IEnumerable<MoveRating> getAllMoves()
        {
            List<Move> moves = moveCrud.readAllMoves();
            IEnumerable<MoveRating> averageRatings = moveCrud.readAllMovesWithRatings();
            return averageRatings;
        }

        public bool tryToUpdateMove(int Id, Move move)
        {
            if (moveCrud.readMoveByName(move.name) is not null)
            {
                moveCrud.updateMove(Id, move.name, move.description, move.sweatrate);
                return true;
            }
            return false;
        }

        public MoveRating getLastMove()
        {
            IEnumerable<MoveRating> Ratings = getAllMoves();
            int moveNr = Ratings.Count() - 1;
            return Ratings.ElementAt(moveNr);
        }

        public MoveRating getMoveById(int Id)
        {
            IEnumerable<MoveRating> Ratings = getAllMoves();
            int moveNr = Ratings.Count() - 1;
            return Ratings.ElementAt(Id - 1);
        }

        public void AddMovesIfEmpty()
        {
            IEnumerable<MoveRating> Ratings = getAllMoves();
            if (Ratings.Count() == 0)
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

        public bool TryToMakeRating(MoveRating newRating)
        {
            if (moveCrud.readMoveByName(newRating.Move.name) == newRating.Move)
            {
                moveCrud.updateMove(newRating.Move.Id, rating: newRating);
                moveCrud.createRating(newRating);
                return true;
            }
            return false;
        }
    }
}
