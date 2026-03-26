using Entity; // Reference tới project Entity
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // ===================== DbSets =====================
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Field> Fields { get; set; }
        public DbSet<FieldTrans> FieldTrans { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceTrans> ServiceTrans { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectTrans> ProjectTrans { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Block> Blocks { get; set; }
        public DbSet<BlockTrans> BlockTrans { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ==================== CONTACT ====================
            modelBuilder.Entity<Contact>(e =>
            {
                e.HasKey(c => c.ContactId);
                e.Property(c => c.Name).IsRequired().HasMaxLength(150);
                e.Property(c => c.Email).IsRequired().HasMaxLength(200);
                e.Property(c => c.Phone).HasMaxLength(20);
                e.Property(c => c.Message).IsRequired();
            });

            // ==================== USER ====================
            modelBuilder.Entity<User>(e =>
            {
                e.HasKey(u => u.UserId);
                e.HasIndex(u => u.Username).IsUnique();
                e.Property(u => u.Username).IsRequired().HasMaxLength(100);
                e.Property(u => u.Password).IsRequired().HasMaxLength(255);
                e.Property(u => u.Role).IsRequired().HasMaxLength(20).HasDefaultValue("user");
                e.Property(u => u.Status).IsRequired().HasMaxLength(20).HasDefaultValue("active");
            });

            // ==================== FIELD ====================
            modelBuilder.Entity<Field>(e =>
            {
                e.HasKey(f => f.FieldId);
                e.HasIndex(f => f.Uid).IsUnique();
                e.Property(f => f.Uid).IsRequired().HasMaxLength(100);
                e.Property(f => f.Status).IsRequired().HasMaxLength(20).HasDefaultValue("active");
                e.Property(f => f.Image).HasMaxLength(500);
            });

            // ==================== FIELD TRANS ====================
            modelBuilder.Entity<FieldTrans>(e =>
            {
                e.HasKey(ft => ft.FieldTransId);
                e.Property(ft => ft.LangCode).IsRequired().HasMaxLength(10);
                e.Property(ft => ft.Name).IsRequired().HasMaxLength(200);
                // Quan he: 1 Field -> nhieu FieldTrans
                e.HasOne(ft => ft.Field)
                 .WithMany(f => f.FieldTrans)
                 .HasForeignKey(ft => ft.FieldId)
                 .OnDelete(DeleteBehavior.Cascade);
            });

            // ==================== SERVICE ====================
            modelBuilder.Entity<Service>(e =>
            {
                e.HasKey(s => s.ServiceId);
                e.Property(s => s.Status).IsRequired().HasMaxLength(20).HasDefaultValue("active");
                e.Property(s => s.Image).HasMaxLength(500);
                e.Property(s => s.CatalogueUrl).HasMaxLength(500);
                // Quan he: 1 Field -> nhieu Service
                e.HasOne(s => s.Field)
                 .WithMany(f => f.Services)
                 .HasForeignKey(s => s.FieldId)
                 .OnDelete(DeleteBehavior.Restrict);
            });

            // ==================== SERVICE TRANS ====================
            modelBuilder.Entity<ServiceTrans>(e =>
            {
                e.HasKey(st => st.ServiceTransId);
                e.Property(st => st.LangCode).IsRequired().HasMaxLength(10);
                e.Property(st => st.Name).IsRequired().HasMaxLength(200);
                // Quan he: 1 Service -> nhieu ServiceTrans
                e.HasOne(st => st.Service)
                 .WithMany(s => s.ServiceTrans)
                 .HasForeignKey(st => st.ServiceId)
                 .OnDelete(DeleteBehavior.Cascade);
            });

            // ==================== PROJECT ====================
            modelBuilder.Entity<Project>(e =>
            {
                e.HasKey(p => p.ProjectId);
                e.Property(p => p.Status).IsRequired().HasMaxLength(20).HasDefaultValue("active");
                e.Property(p => p.Image).HasMaxLength(500);
                e.Property(p => p.CatalogueUrl).HasMaxLength(500);
                // Quan he: 1 Field -> nhieu Project
                e.HasOne(p => p.Field)
                 .WithMany(f => f.Projects)
                 .HasForeignKey(p => p.FieldId)
                 .OnDelete(DeleteBehavior.Restrict);
            });

            // ==================== PROJECT TRANS ====================
            modelBuilder.Entity<ProjectTrans>(e =>
            {
                e.HasKey(pt => pt.ProjectTransId);
                e.Property(pt => pt.LangCode).IsRequired().HasMaxLength(10);
                e.Property(pt => pt.Name).IsRequired().HasMaxLength(200);
                // Quan he: 1 Project -> nhieu ProjectTrans
                e.HasOne(pt => pt.Project)
                 .WithMany(p => p.ProjectTrans)
                 .HasForeignKey(pt => pt.ProjectId)
                 .OnDelete(DeleteBehavior.Cascade);
            });

            // ==================== FEATURE ====================
            modelBuilder.Entity<Feature>(e =>
            {
                e.HasKey(f => f.FeatureId);
                e.Property(f => f.LangCode).IsRequired().HasMaxLength(10);
                e.Property(f => f.Icon).HasMaxLength(100);
                // Quan he: 1 Project -> nhieu Feature
                e.HasOne(f => f.Project)
                 .WithMany(p => p.Features)
                 .HasForeignKey(f => f.ProjectId)
                 .OnDelete(DeleteBehavior.Cascade);
            });

            // ==================== BLOCK ====================
            modelBuilder.Entity<Block>(e =>
            {
                e.HasKey(b => b.BlockId);
                e.Property(b => b.OwnerType).IsRequired().HasMaxLength(20);
                e.Property(b => b.BlockType).IsRequired().HasMaxLength(50);
                e.Property(b => b.ImageUrl).HasMaxLength(500);
            });

            // ==================== BLOCK TRANS ====================
            modelBuilder.Entity<BlockTrans>(e =>
            {
                e.HasKey(bt => bt.BlockTransId);
                e.Property(bt => bt.LangCode).IsRequired().HasMaxLength(10);
                // Quan he: 1 Block -> nhieu BlockTrans
                e.HasOne(bt => bt.Block)
                 .WithMany(b => b.BlockTrans)
                 .HasForeignKey(bt => bt.BlockId)
                 .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}