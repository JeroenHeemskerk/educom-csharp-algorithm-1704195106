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

        public Move getRandomMove()
        {
            Random rand = new Random();
            List<Move> moves = this.moveCrud.readAllMoves();
            return moves[rand.Next(0, moves.Count - 1)];
        }

        public List<Move> getAllMoves()
        {
            return this.moveCrud.readAllMoves();
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

        public Move getLastMove()
        {
            List<Move> allMoves = getAllMoves();
            return allMoves[allMoves.Count - 1];
        }

        public Move getMoveById(int Id)
        {
            List<Move> allMoves = getAllMoves();
            return allMoves[Id - 1];
        }
    }
}
