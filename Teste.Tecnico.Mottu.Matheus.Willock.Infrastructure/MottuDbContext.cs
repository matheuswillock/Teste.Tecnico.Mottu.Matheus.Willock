using Microsoft.EntityFrameworkCore;
using Teste.Tecnico.Mottu.Matheus.Willock.Domain.Models;

namespace Teste.Tecnico.Mottu.Matheus.Willock.Infrastructure
{
    public class MottuDbContext : DbContext
    {
        public DbSet<DeliveryMan> DeliveryMen { get; set; }
        public DbSet<Motorcycle> Motorcycles { get; set; }
        public DbSet<UserAdmin> UsersAdmin { get; set; }
        public DbSet<DeliveryManDocument> Documents { get; set; }
        public DbSet<RentalPlan> RentalPlans { get; set; }
        public DbSet<Order> Orders { get; set; }

        public MottuDbContext(DbContextOptions<MottuDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DeliveryMan>().HasKey(x => x.Id);
            modelBuilder.Entity<Motorcycle>().HasKey(x => x.Id);
            modelBuilder.Entity<DeliveryManDocument>().HasKey(x => x.Id);
            modelBuilder.Entity<RentalPlan>().HasKey(x => x.Id);
            modelBuilder.Entity<Order>().HasKey(x => x.Id);
        }

        public MottuDbContext() : base(new DbContextOptionsBuilder<MottuDbContext>()
            .UseNpgsql("Host=localhost;Database=mottu_db_teste;Username=my_user;Password=my_pw;Port=5432")
            .Options)
        {
        }

    }
}
