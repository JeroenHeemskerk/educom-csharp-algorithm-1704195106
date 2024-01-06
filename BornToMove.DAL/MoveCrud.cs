using Microsoft.EntityFrameworkCore;
using BornToMove;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Reflection.Metadata;
using Microsoft.Identity.Client;

namespace BornToMove.DAL
{
    public class MoveContext : DbContext
    {

        private const string MSSQL_CONNECTION_STRING = "Server=(localdb)\\mssqllocaldb;Database=born2move;Trusted_Connection=true;TrustServerCertificate=true;";

        public DbSet<Move> Moves { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer(MSSQL_CONNECTION_STRING);

            base.OnConfiguring(builder);
        }
    }

    public class MoveCrud
    { 
        public MoveCrud() { context = new MoveContext(); }
        public MoveContext context { get; set; }
        public void createMove(Move newMove)
        {
            context.Moves.Add(newMove);
            context.SaveChanges();
        }

        public void updateMove(String oldname, 
            String newName = null, String newDescription = null, int newSweatrate = 0) {
            Move? oldMove = context.Moves.FirstOrDefault(Move => Move.name == oldname);
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
                context.SaveChanges();
            }
        }

        public void delete(String name)
        {
            Move? move = context.Moves.FirstOrDefault(Move => Move.name == name);
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
    }
}
