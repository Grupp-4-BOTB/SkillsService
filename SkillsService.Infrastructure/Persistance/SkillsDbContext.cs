using Microsoft.EntityFrameworkCore;
using SkillsService.Domain.Entities;
using SkillsService.Domain.ValueObjects;

namespace SkillsService.Infrastructure.Persistance;

public class SkillsDbContext : DbContext
{
    public SkillsDbContext(DbContextOptions<SkillsDbContext> options) : base(options) { }

    public DbSet<SkillCatalog> SkillCatalog => Set<SkillCatalog>();

    public DbSet<UserSkill> UserSkills => Set<UserSkill>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SkillCatalog>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);
        });

        modelBuilder.Entity<UserSkill>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.OwnerId)
                .HasConversion(v => v.Value, v => new OwnerId(v))
                .IsRequired();

            entity.HasOne(e => e.SkillCatalog)
                .WithMany()
                .HasForeignKey(e => e.SkillCatalogId);

            entity.Property(e => e.CreatedAt)
                .IsRequired();
        });

        modelBuilder.Entity<SkillCatalog>().HasData(
            new { Id = Guid.Parse("a1b2c3d4-0001-0000-0000-000000000000"), Name = "Web Design" },
            new { Id = Guid.Parse("a1b2c3d4-0002-0000-0000-000000000000"), Name = "Graphic Design" },
            new { Id = Guid.Parse("a1b2c3d4-0003-0000-0000-000000000000"), Name = "UI Design" },
            new { Id = Guid.Parse("a1b2c3d4-0004-0000-0000-000000000000"), Name = "UX Design" },
            new { Id = Guid.Parse("a1b2c3d4-0005-0000-0000-000000000000"), Name = "Mobile App Design" },
            new { Id = Guid.Parse("a1b2c3d4-0006-0000-0000-000000000000"), Name = "Illustration" },
            new { Id = Guid.Parse("a1b2c3d4-0007-0000-0000-000000000000"), Name = "Animation" },
            new { Id = Guid.Parse("a1b2c3d4-0008-0000-0000-000000000000"), Name = "Branding" },
            new { Id = Guid.Parse("a1b2c3d4-0009-0000-0000-000000000000"), Name = "Product Design" }

        );
    }
}
