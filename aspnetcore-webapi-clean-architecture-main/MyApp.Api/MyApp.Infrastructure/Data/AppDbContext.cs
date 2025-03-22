using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyApp.Core.Entities;

namespace MyApp.Infrastructure.Data
{
      public class AppDbContext : IdentityDbContext<UserEntity>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {
        }

        public DbSet<EmployeeEntity> Employees { get; set; }
        public DbSet<HotelEntity> Hotels { get; set; }
        public DbSet<RoomEntity> Rooms { get; set; }
        public DbSet<ManagerEntity> Managers { get; set; }
        public DbSet<GuestEntity> Guests { get; set; }
        public DbSet<ReservationEntity> Reservations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Hotel-Manager relationship
            modelBuilder.Entity<HotelEntity>()
                .HasOne(h => h.Manager)
                .WithOne(m => m.Hotel)
                .HasForeignKey<ManagerEntity>(m => m.HotelId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure Room-Reservation relationship
            modelBuilder.Entity<ReservationEntity>()
                .HasOne(r => r.Room)
                .WithMany()
                .HasForeignKey(r => r.RoomId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure Guest-Reservation relationship
            modelBuilder.Entity<ReservationEntity>()
                .HasOne(r => r.Guest)
                .WithMany(g => g.Reservations)
                .HasForeignKey(r => r.GuestId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure price precision for RoomEntity
            modelBuilder.Entity<RoomEntity>()
                .Property(r => r.Price)
                .HasPrecision(18, 4);

            // Ensure correct mapping for User-Guest relationship
            modelBuilder.Entity<GuestEntity>()
                .HasBaseType<UserEntity>();
        }
    }
}
