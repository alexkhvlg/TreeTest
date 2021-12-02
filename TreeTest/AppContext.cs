using Microsoft.EntityFrameworkCore;

namespace TreeTest
{
    public class AppContext : DbContext
    {
        private readonly string _connectionString;

        public DbSet<Company> Companies { get; set; }
        public AppContext(string ConnectionString) : base()
        {
            _connectionString = ConnectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>()
                .HasOne<Company>()
                .WithMany(x => x.Childs)
                .HasForeignKey(x => x.ParentId);
        }
    }
}
