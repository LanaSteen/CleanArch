using Microsoft.EntityFrameworkCore;
using MyApp.Core.Entities;

namespace MyApp.Infrastructure.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<EmployeeEntity> Employees { get; set; }
        public DbSet<HotelEntity> Hotels { get; set; }
        public DbSet<RoomEntity> Rooms { get; set; }
        public DbSet<ManagerEntity> Managers { get; set; }
        public DbSet<GuestEntity> Guests { get; set; }
        public DbSet<ReservationEntity> Reservations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HotelEntity>()
                .HasOne(h => h.Manager)
                .WithOne(m => m.Hotel)
                .HasForeignKey<ManagerEntity>(m => m.HotelId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<HotelEntity>()
                .HasMany(h => h.Rooms)
                .WithOne(r => r.Hotel)
                .HasForeignKey(r => r.HotelId);

            modelBuilder.Entity<ReservationEntity>()
                .HasOne(r => r.Room)
                .WithMany()
                .HasForeignKey(r => r.RoomId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ReservationEntity>()
                .HasOne(r => r.Guest)
                .WithMany(g => g.Reservations)
                .HasForeignKey(r => r.GuestId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<RoomEntity>()
              .Property(r => r.Price)
              .HasPrecision(18, 4);

            base.OnModelCreating(modelBuilder);
        }
    }
}
