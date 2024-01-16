using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using BornToMove;

namespace WebApp.Models
{
    public class BlogsContext : DbContext
    {
        private const string MSSQL_CONNECTION_STRING = "Server=(localdb)\\mssqllocaldb;Database=blogs;Trusted_Connection=true;TrustServerCertificate=true;";
        private const string MYSQL_CONNECTION_STRING = "Server=localhost; User ID=webshop_rogier; Password=webshoprogier; Database=blogs";

        public DbSet<Move> Moves { get; set; }
        public DbSet<MoveRating> BlogsComment { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(MSSQL_CONNECTION_STRING); // MSSQL
                                                                    //optionsBuilder.UseMySql(MYSQL_CONNECTION_STRING, ServerVersion.AutoDetect(MYSQL_CONNECTION_STRING)); // MYSQL

            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
            base.OnConfiguring(optionsBuilder);
        }
    }

}
