using FinalLab.Models;
using Microsoft.EntityFrameworkCore; //import Ef
using System.Collections.Generic;



namespace FinalLab.Database
{
    public class DataDbContext  : DbContext
    {
        public DataDbContext(DbContextOptions<DataDbContext> options) : base(options) { }

        public DbSet<employees> employees { get; set; }

        public DbSet<positions> positions { get; set; }
    }
}
