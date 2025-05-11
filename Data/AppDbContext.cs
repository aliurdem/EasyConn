using EasyConnect.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace EasyConnect.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public DbSet<BusinessProfile> BusinessProfiles { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<WorkingHour> WorkingHours { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Categories 
            modelBuilder.Entity<Category>().HasData(
      new Category { Id = 1, Name = "Kuaför" },
      new Category { Id = 2, Name = "Güzellik Merkezi" },
      new Category { Id = 3, Name = "Masaj Salonu" }
  );

            /* User ↔ BusinessProfile : 1‑1 */
            modelBuilder.Entity<User>()
                .HasOne(u => u.BusinessProfile)
                .WithOne(p => p.User)
                .HasForeignKey<BusinessProfile>(p => p.UserId);

            /* BusinessProfile ↔ Staff : 1‑n */
            modelBuilder.Entity<Staff>()
                .HasOne(s => s.BusinessProfile)
                .WithMany(b => b.Staffs)
                .HasForeignKey(s => s.BusinessProfileId)
                .OnDelete(DeleteBehavior.Restrict);

            /* Category ↔ BusinessProfile : 1-n */
            modelBuilder.Entity<BusinessProfile>()
                .HasOne(s => s.Category)
                .WithMany(b => b.Businesses)
                .HasForeignKey(s => s.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // <-- BURAYA EKLE -->
            modelBuilder.Entity<BusinessProfile>()
                .Property(b => b.CategoryId)
                .HasDefaultValue(1);

            /* BusinessProfile ↔ Service : n‑n */
            modelBuilder.Entity<BusinessProfile>()
                .HasMany(b => b.Services)
                .WithMany(s => s.BusinessProfiles)
                .UsingEntity<Dictionary<string, object>>(
                    "BusinessProfileService",                    // ara tablo adı
                    j => j.HasOne<Service>()
                          .WithMany()
                          .HasForeignKey("ServiceId")
                          .OnDelete(DeleteBehavior.Restrict),
                    j => j.HasOne<BusinessProfile>()
                          .WithMany()
                          .HasForeignKey("BusinessProfileId")
                          .OnDelete(DeleteBehavior.Restrict),
                    j =>
                    {
                        j.HasKey("BusinessProfileId", "ServiceId");
                        j.ToTable("BusinessProfileServices");    // opsiyonel farklı ad
                    });

            /* Service.Price formatı */
            modelBuilder.Entity<Service>()
                .Property(s => s.Price)
                .HasColumnType("decimal(18,2)");

            /* BusinessProfile ↔ WorkingHour : 1‑n */
            modelBuilder.Entity<WorkingHour>()
                .HasOne(w => w.BusinessProfile)
                .WithMany() // Eğer BusinessProfile tarafında ICollection<WorkingHour> varsa buraya .WithMany(b => b.WorkingHours) yaz
                .HasForeignKey(w => w.BusinessProfileId)
                .OnDelete(DeleteBehavior.Cascade);

            /* Gerekli alanlar zorunlu */
            modelBuilder.Entity<WorkingHour>()
                .Property(w => w.DayOfWeek)
                .IsRequired();

            modelBuilder.Entity<WorkingHour>()
                .Property(w => w.StartTime)
                .IsRequired();

            modelBuilder.Entity<WorkingHour>()
                .Property(w => w.EndTime)
                .IsRequired();

            /* Aynı iş yerinde aynı gün ikinci kayıt olmasın */
            modelBuilder.Entity<WorkingHour>()
                .HasIndex(w => new { w.BusinessProfileId, w.DayOfWeek })
                .IsUnique();


            modelBuilder.Entity<Holiday>()
    .HasOne(h => h.BusinessProfile)
    .WithMany(b => b.Holidays)
    .HasForeignKey(h => h.BusinessProfileId)
    .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Holiday>()
                .HasIndex(h => new { h.BusinessProfileId, h.Date })
                .IsUnique(); // Aynı gün ikinci kez tatil tanımlanmasın



            // Appointment 

            modelBuilder.Entity<Appointment>()
            .HasOne(a => a.User)
            .WithMany()
            .HasForeignKey(a => a.UserId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.BusinessProfile)
                .WithMany()
                .HasForeignKey(a => a.BusinessProfileId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Staff)
                .WithMany()
                .HasForeignKey(a => a.StaffId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Service)
                .WithMany()
                .HasForeignKey(a => a.ServiceId)
                .OnDelete(DeleteBehavior.Restrict);


        }

    }
}
