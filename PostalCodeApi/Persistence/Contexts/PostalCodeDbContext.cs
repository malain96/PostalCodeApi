using Microsoft.EntityFrameworkCore;
using PostalCodeApi.Domain.Models;

namespace PostalCodeApi.Persistence.Contexts
{
    public class PostalCodeDbContext
        : DbContext
    {
        public PostalCodeDbContext(DbContextOptions<PostalCodeDbContext> options) : base(options)
        {
        }

        // Define all tables
        public DbSet<PostalCode> PostalCodes { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<PostalCodeCity> PostalCodeCities { get; set; }
        public DbSet<User> Users { get; set; }

        /// <summary>
        ///     Define tables' relations and keys
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define the join table's primary key
            modelBuilder.Entity<PostalCodeCity>()
                .HasKey(pcc => new {pcc.PostalCodeId, pcc.CityId});

            // Define the join table's foreign keys 
            modelBuilder.Entity<PostalCodeCity>()
                .HasOne(pcc => pcc.PostalCode)
                .WithMany(pc => pc.PostalCodeCities)
                .HasForeignKey(pcc => pcc.PostalCodeId);

            modelBuilder.Entity<PostalCodeCity>()
                .HasOne(pcc => pcc.City)
                .WithMany(c => c.PostalCodeCities)
                .HasForeignKey(pcc => pcc.CityId);
        }
    }
}