using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Reservation.Domain.Entities;

namespace Reservation.Persistence.Context
{
    public class ReservationDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {

        public ReservationDbContext(DbContextOptions<ReservationDbContext> options)
            : base(options)
        {

        }

        public DbSet<Company> Companies => Set<Company>();
        public DbSet<CompanyReview> CompanyReviews => Set<CompanyReview>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Reservation.Domain.Entities.Reservation> Reservations => Set<Reservation.Domain.Entities.Reservation>();
        public DbSet<Service> Services => Set<Service>();
        public DbSet<StaffMember> StaffMembers => Set<StaffMember>();
        public DbSet<Specialty> Specialties => Set<Specialty>();
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<StaffMemberService> StaffMemberServices => Set<StaffMemberService>();
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Company>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Name).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<CompanyReview>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Comment).IsRequired().HasMaxLength(1000);
                entity.Property(x => x.Rating).IsRequired();

                entity.HasOne(x => x.Company)
                    .WithMany(c => c.CompanyReviews)
                    .HasForeignKey(x => x.CompanyId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Reservation.Domain.Entities.Reservation>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.Property(x => x.Title)
                    .HasMaxLength(200);

                entity.Property(x => x.ReservationTime)
                    .IsRequired();

                //entity.Property(x => x.CustomerName)
                //    .IsRequired()
                //    .HasMaxLength(100);

                //entity.Property(x => x.CustomerPhone)
                //    .IsRequired()
                //    .HasMaxLength(20);

                entity.Property(x => x.Status)
                    .IsRequired();

                entity.HasOne(x => x.Company)
                    .WithMany(c => c.Reservations)
                    .HasForeignKey(x => x.CompanyId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(x => x.Service)
                    .WithMany(s => s.Reservations)
                    .HasForeignKey(x => x.ServiceId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(x => x.StaffMember)
                    .WithMany(sm => sm.Reservations)
                    .HasForeignKey(x => x.StaffMemberId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(x => x.Customer)
                    .WithMany(z => z.Reservations)
                    .HasForeignKey(x => x.CustomerId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Name).IsRequired().HasMaxLength(100);
                entity.Property(x => x.Price).IsRequired();

                entity.HasOne(x => x.Company)
                    .WithMany(c => c.Services)
                    .HasForeignKey(x => x.CompanyId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(x => x.Specialty)
               .WithMany(s => s.Services)
               .HasForeignKey(x => x.SpecialtyId)
               .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<StaffMember>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.Property(x => x.FullName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(x => x.Specialty)
                    .WithMany(s => s.StaffMembers)
                    .HasForeignKey(x => x.SpecialtyId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(x => x.Company)
                    .WithMany(c => c.StaffMembers)
                    .HasForeignKey(x => x.CompanyId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Specialty>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.Property(x => x.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasMany(x => x.StaffMembers)
                    .WithOne(sm => sm.Specialty)
                    .HasForeignKey(sm => sm.SpecialtyId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(x => x.Company)
                .WithMany(y => y.Specialties)
                .HasForeignKey(z => z.CompanyId);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.Property(x => x.FullName)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(x => x.PhoneNumber)
                      .IsRequired()
                      .HasMaxLength(20);

                entity.Property(x => x.Email)
                      .HasMaxLength(100);

                entity.Property(x => x.Note)
                      .HasMaxLength(500);
            });

            modelBuilder.Entity<StaffMemberService>(entity =>
            {
                entity.HasKey(x => new { x.StaffMemberId, x.ServiceId });

                entity.HasOne(x => x.StaffMember)
                      .WithMany(sm => sm.StaffMemberServices)
                      .HasForeignKey(x => x.StaffMemberId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(x => x.Service)
                      .WithMany(s => s.StaffMemberServices)
                      .HasForeignKey(x => x.ServiceId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }

    }
}
