using EbookWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Xml.Linq;

namespace EbookWebAPI
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<LinkEbook> LinkEbooks { get; set; }
        public DbSet<EmailCustomer> EmailCustomers { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=eBookDB")
                .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name },
                Microsoft.Extensions.Logging.LogLevel.Information).EnableSensitiveDataLogging();
        }       
    }
}
