using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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

        public DbSet<StaffMemberSpecialty> StaffMemberSpecialty => Set<StaffMemberSpecialty>();
        public DbSet<StaffMemberService> StaffMemberServices => Set<StaffMemberService>();

        public DbSet<CompanyService> CompaniesServices => Set<CompanyService>();
        public DbSet<CompanySpecialty> CompaniesSpeciality => Set<CompanySpecialty>();

        public DbSet<ReservationSpecService> ReservationSpecService => Set<ReservationSpecService>();


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

                entity.Property(x => x.Status)
                    .IsRequired();

                entity.HasOne(x => x.Company)
                    .WithMany(c => c.Reservations)
                    .HasForeignKey(x => x.CompanyId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(x => x.Specialty)
                    .WithMany(s => s.Reservations)
                    .HasForeignKey(x => x.SpecialtyId)
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

                /*                entity.HasOne(x => x.Company)
                                    .WithMany(c => c.Services)
                                    .HasForeignKey(x => x.CompanyId)
                                    .OnDelete(DeleteBehavior.Restrict);*/

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

                //entity.HasOne(x => x.Specialty)
                //    .WithMany(s => s.StaffMembers)
                //    .HasForeignKey(x => x.SpecialtyId)
                //    .OnDelete(DeleteBehavior.Restrict);

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

                //entity.HasMany(x => x.StaffMembers)
                //    .WithOne(sm => sm.Specialty)
                //    .HasForeignKey(sm => sm.SpecialtyId)
                //    .OnDelete(DeleteBehavior.Restrict);
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
                entity.HasKey(x => new { x.StaffMemberId, x.ServiceId,x.SpecialtyID });

                entity.HasOne(x => x.StaffMember)
                      .WithMany(sm => sm.StaffMemberServices)
                      .HasForeignKey(x => x.StaffMemberId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(x => x.Service)
                      .WithMany(s => s.StaffMemberServices)
                      .HasForeignKey(x => x.ServiceId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(x => x.Specialty)
                      .WithMany(s => s.StaffMemberServices)
                      .HasForeignKey(x => x.SpecialtyID)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<StaffMemberSpecialty>(entity =>
            {
                entity.HasKey(x => new { x.StaffMemberId, x.SpecialtyId });

                entity.HasOne(x => x.StaffMember)
                      .WithMany(sm => sm.StaffMemberSpecialties)
                      .HasForeignKey(x => x.StaffMemberId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(x => x.Specialty)
                      .WithMany(s => s.StaffMemberSpecialties)
                      .HasForeignKey(x => x.SpecialtyId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<CompanySpecialty>()
             .HasKey(cs => new { cs.CompanyId, cs.SpecialtyId });

            modelBuilder.Entity<CompanySpecialty>()
                .HasOne(cs => cs.Company)
                .WithMany(c => c.CompanySpecialties)
                .HasForeignKey(cs => cs.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CompanySpecialty>()
                .HasOne(cs => cs.Specialty)
                .WithMany(s => s.CompanySpecialties)
                .HasForeignKey(cs => cs.SpecialtyId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CompanyService>()
                .HasKey(cs => new { cs.CompanyId, cs.ServiceId });

            modelBuilder.Entity<CompanyService>()
                .HasOne(cs => cs.Company)
                .WithMany(c => c.CompanyServices)
                .HasForeignKey(cs => cs.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CompanyService>()
                .HasOne(cs => cs.Service)
                .WithMany(s => s.CompanyServices)
                .HasForeignKey(cs => cs.ServiceId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ReservationSpecService>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.HasOne(x => x.Reservation)
                      .WithMany(r => r.ReservationSpecServices)
                      .HasForeignKey(x => x.ReservationId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(x => x.Specialty)
                      .WithMany(s => s.ReservationSpecServices)
                      .HasForeignKey(x => x.SpecialtyId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(x => x.Service)
                      .WithMany(s => s.ReservationSpecServices)
                      .HasForeignKey(x => x.ServiceId)
                      .OnDelete(DeleteBehavior.Restrict);
            });


            modelBuilder.HasSequence<int>("Seq_ClientCode")
                .HasMin(100000)
                .HasMax(5000000)
                .StartsAt(110000)
                .IncrementsBy(1);

            modelBuilder.Entity<Customer>()
                .Property(o => o.ClientCode)
                .HasDefaultValueSql("NEXT VALUE FOR Seq_ClientCode");

            modelBuilder.HasSequence<int>("Seq_ReservationNumber")
                .HasMin(1)
                .HasMax(5000000)
                .StartsAt(1)
                .IncrementsBy(1);

            modelBuilder.Entity<Reservation.Domain.Entities.Reservation>()
                .Property(o => o.ReservationNumer)
                .HasDefaultValueSql("NEXT VALUE FOR Seq_ReservationNumber");
        }

    }
}
