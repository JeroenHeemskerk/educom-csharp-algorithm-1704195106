using BornToMove.DAL;

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

        public (Move, double, double) getRandomMove()
        {
            Random rand = new Random();
            (List<Move> allMoves, IEnumerable<double> allRatings, IEnumerable<double> allVotes) = getAllMoves();
            int moveNr = rand.Next(0, allMoves.Count - 1);
            return (allMoves[moveNr], allRatings.ElementAt(moveNr), allVotes.ElementAt(moveNr));
        }

        public (List<Move>, IEnumerable<double>, IEnumerable<double>) getAllMoves()
        {
            List<Move> moves = this.moveCrud.readAllMoves();
            IEnumerable<double> ratings = getAllRatings(moves.Select(moves => moves.name));
            IEnumerable<double> votes = getAllVotes(moves.Select(moves => moves.name));
            return (moves, ratings, votes);
        }

        public bool tryToUpdateMove(String oldName, Move move)
        {
            if (this.moveCrud.readMoveByName(move.name) is not null)
            {
                this.moveCrud.updateMove(oldName, move.name, move.description, move.sweatrate);
                return true;
            }
            return false;
        }

        public (Move, double, double) getLastMove()
        {
            (List<Move> allMoves, IEnumerable<double> allRatings, IEnumerable<double> allVotes) = getAllMoves();
            return (allMoves[allMoves.Count - 1], allRatings.ElementAt(allMoves.Count - 1), allVotes.ElementAt(allMoves.Count - 1));
        }

        public (Move, double, double) getMoveById(int Id)
        {
            (List<Move> allMoves, IEnumerable<double> allRatings, IEnumerable<double> allVotes) = getAllMoves();
            return (allMoves[Id - 1], allRatings.ElementAt(Id - 1), allVotes.ElementAt(Id - 1));
        }

        public void AddMovesIfEmpty()
        {
            (List<Move> allMoves, IEnumerable<double> allRatings, IEnumerable<double> allVotes) = getAllMoves();
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


        public List<double> getAllRatings(IEnumerable<String> names)
        {
            List<double> ratings = new List<double>();
            foreach (String name in names)
            {
                ratings.Add(moveCrud.readRatingByMove(name));
            }
            return ratings;
        }

        public List<double> getAllVotes(IEnumerable<String> names)
        {
            List<double> votes = new List<double>();
            foreach (String name in names)
            {
                votes.Add(moveCrud.readVoteByMove(name));
            }
            return votes;
        }
    }
}
