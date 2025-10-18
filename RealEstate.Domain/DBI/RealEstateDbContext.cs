using Microsoft.EntityFrameworkCore;
using RealEstate.Domain.DBI.SeedData;
using RealEstate.Domain.Models;
using TestSupport.EfHelpers;

namespace RealEstate.Domain.DBI
{
    public class RealEstateDbContext(DbContextOptions<RealEstateDbContext> options) : DbContext(options)
    {
        private readonly IConfiguration _configuration;
        public virtual DbSet<Category> Categories => Set<Category>();
        public virtual DbSet<User> Users => Set<User>();
        public virtual DbSet<Property> Properties => Set<Property>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasData(new CategoryDataInitialiser().Data);
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _configuration["ConnectionStrings:local"];
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=RealEstate");
            //optionsBuilder.UseSqlServer(@"Server=tcp:realestatedbserver.database.windows.net,1433;Initial Catalog=realestate002;Persist Security Info=False;User ID=quynh;Password=Planday#1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }

        public void EnsureClean()
        {
            Database.EnsureClean();
        }

    }
}
