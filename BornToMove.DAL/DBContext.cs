using Microsoft.EntityFrameworkCore;
using BornToMove;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Reflection.Metadata;
using Microsoft.Identity.Client;
using System;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace BornToMove.DAL
{
    public class MoveContext : DbContext
    {

        private const string MSSQL_CONNECTION_STRING = "Server=(localdb)\\mssqllocaldb;Database=born2move;Trusted_Connection=true;TrustServerCertificate=true;";

        public DbSet<Move> Moves { get; set; }
        public DbSet<MoveRating> MoveRating { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer(MSSQL_CONNECTION_STRING);

            base.OnConfiguring(builder);
        }
    }
}
