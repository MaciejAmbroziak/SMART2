using Microsoft.EntityFrameworkCore;

namespace SMART2.Domain
{
    public class DomainDbContext : DbContext
    {

        public DbSet<EquipmentContract> EquipmentContracts { get; set; }
        public DbSet<ProcessEquipment> ProcessEquipments { get; set; }
        public DbSet<ProductionFacility> ProductionFacilities { get; set; }

        public DomainDbContext(DbContextOptions<DomainDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ProcessEquipment>()
                .HasIndex(a => a.Code)
                .IsUnique();
            builder.Entity<ProductionFacility>()
                .HasIndex(a => a.Code)
                .IsUnique();
            builder.Entity<EquipmentContract>()
                .HasMany(a => a.ProcessEquipment)
                .WithMany(a => a.EquipmentContracts)
                .UsingEntity("ProcessEquipmentEquipmentContracts");
        }
    }
}
