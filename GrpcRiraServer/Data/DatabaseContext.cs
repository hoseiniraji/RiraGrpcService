using GrpcRiraServer.Models;
using Microsoft.EntityFrameworkCore;

namespace GrpcRiraServer.Data
{
    public class DatabaseContext : Microsoft.EntityFrameworkCore.DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseInMemoryDatabase("RiraGrpcServerDb");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var mockUsers = new List<Models.User>();
            for (int i = 0; i < 20; i++)
            {
                mockUsers.Add(new User()
                {
                    Id = Guid.NewGuid().ToString("N"),
                    FirstName = Faker.NameFaker.FirstName(),
                    LastName = Faker.NameFaker.LastName(),
                    Birthday = Faker.DateTimeFaker.BirthDay(20, 50),
                    NationalCode = $"0{Faker.StringFaker.Numeric(9)}",
                });
            }
            modelBuilder.Entity<User>().HasData(mockUsers);
        }

        public DbSet<User> Users { get; set; }
    }
}
