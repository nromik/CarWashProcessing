using CarWashProcessing.DataModel;
using Microsoft.EntityFrameworkCore;
using WebApplication2;

namespace CarWashProcessing
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
            modelBuilder.Entity<TaskType>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(50);
            });
        }
    }
}
