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
            modelBuilder.Entity<GuestEntity>().ToTable("Guests"); 


            modelBuilder.Entity<HotelEntity>()
                .HasOne(h => h.Manager)
                .WithOne(m => m.Hotel)
                .HasForeignKey<ManagerEntity>(m => m.HotelId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ReservationEntity>()
                .HasOne(r => r.Room)
                .WithMany()
                .HasForeignKey(r => r.RoomId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ReservationEntity>()
       .HasOne(r => r.Room)
       .WithMany(r => r.Reservations)
       .HasForeignKey(r => r.RoomId);

            modelBuilder.Entity<RoomEntity>()
            .HasMany(r => r.Reservations)
            .WithOne(r => r.Room)
            .HasForeignKey(r => r.RoomId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<GuestEntity>()
                .HasBaseType<UserEntity>();

        }
    }
}
