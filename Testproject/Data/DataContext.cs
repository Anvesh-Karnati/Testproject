using Microsoft.EntityFrameworkCore;
using Testproject.Model;
using Testproject.Models;

namespace Testproject.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Details> Detail { get; set; }

        public DbSet<Address> Address { get; set; }
        public DbSet<AddDetails> AddDetails { get; set; }

    }
}
