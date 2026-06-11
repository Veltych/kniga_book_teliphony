using kniga_book_teliphony.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kniga_book_teliphony
{
    //public class ApplicationContext
    //{
    //    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //}
    public class ApplicationContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; } // Замените Contact на вашу модель
        public ApplicationContext(DbContextOptions options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Настройка подключения к БД (например, SQLite или SQL Server)
            optionsBuilder.UseSqlServer("Data Source=DBSRV\\ROG2025;Initial Catalog=PhoneBookDB;Integrated Security=True;Trust Server Certificate=True");
            // Или для SQL Server:
            // optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=PhoneBookDB;Trusted_Connection=True;");
        }
    }
}
