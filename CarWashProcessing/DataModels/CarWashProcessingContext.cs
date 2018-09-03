using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CarWashProcessing.DataModels
{
    public partial class CarWashProcessingContext : DbContext
    {
        public CarWashProcessingContext()
        {
        }

        public CarWashProcessingContext(DbContextOptions<CarWashProcessingContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<OrderTask> OrderTask { get; set; }
        public virtual DbSet<OrderType> OrderType { get; set; }
        public virtual DbSet<TaskType> TaskType { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=localhost; Database=CarWashProcessing; Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Orders>(entity =>
            {
                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.StartTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<OrderTask>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.TaskId });
            });

            modelBuilder.Entity<OrderType>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Description).HasMaxLength(256);

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<TaskType>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(50);
            });
            OnModelCreatingViews(modelBuilder);
        }
    }
}
