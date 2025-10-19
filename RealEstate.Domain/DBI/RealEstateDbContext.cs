using Microsoft.EntityFrameworkCore;
using RealEstate.Domain.DBI.SeedData;
using RealEstate.Domain.Models;
using TestSupport.EfHelpers;

namespace RealEstate.Domain.DBI
{
    public class RealEstateDbContext(DbContextOptions<RealEstateDbContext> options) : DbContext(options)
    {
        //private readonly IConfiguration _configuration;
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

        public void EnsureClean()
        {
            Database.EnsureClean();
        }

    }
}
