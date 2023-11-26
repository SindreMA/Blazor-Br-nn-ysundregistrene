using BrregAPI.Modals.Database;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BrregAPI.Modals
{
    public class BrregContext : IdentityDbContext<User>
    {
        public BrregContext() { }
        public BrregContext(DbContextOptions<DbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {            
            builder.UseNpgsql(Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING"));
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Firma>().HasKey(x => x.Organisasjonsnummer);
            base.OnModelCreating(builder);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Firma> Firmaer { get; set; }
        public DbSet<Adresse> Adresser { get; set; }
        public DbSet<Person> Personer { get; set; }
    }
}