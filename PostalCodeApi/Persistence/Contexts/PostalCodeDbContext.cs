using Microsoft.EntityFrameworkCore;
using PostalCodeApi.Domain.Models;
using PostalCodeApi.Entities;
using PostalCodeApi.Helpers;

namespace PostalCodeApi.Persistence.Contexts
{
    /// <summary>
    /// DBContext of the postal code database
    /// </summary>
    public class PostalCodeDbContext
        : DbContext
    {
        private const string DefaultPassword = "1234Aze@";

        public PostalCodeDbContext(DbContextOptions<PostalCodeDbContext> options) :
            base(options)
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

            PasswordHelper.CreatePasswordHash(DefaultPassword, out var passwordHash, out var passwordSalt);

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Username = "admin",
                    Role = Role.Admin,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt
                },
                new User
                {
                    Id = 2,
                    Username = "user",
                    Role = Role.User,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt
                }
            );
        }
    }
}