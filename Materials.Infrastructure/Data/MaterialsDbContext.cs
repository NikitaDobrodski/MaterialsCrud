using Materials.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Materials.Infrastructure.Data;

public class MaterialsDbContext : DbContext
{
    public MaterialsDbContext(DbContextOptions<MaterialsDbContext> options)
        : base(options)
    {
    }

    public DbSet<Material> Materials => Set<Material>();
    public DbSet<Title> Titles => Set<Title>();
    public DbSet<Genre> Genres => Set<Genre>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // ---------------------------
        // Material
        // ---------------------------
        modelBuilder.Entity<Material>(entity =>
        {
            entity.HasKey(x => x.Id);

            entity.Property(x => x.MaterialID)
                .IsRequired();

            // Уникальный MaterialID (по заданию)
            entity.HasIndex(x => x.MaterialID)
                .IsUnique();

            entity.Property(x => x.Tkin)
                .IsRequired();

            entity.Property(x => x.DateEfir)
                .IsRequired();

            // ВАЖНО: SQLite не умеет decimal → конвертируем в double
            entity.Property(x => x.Rating)
                .HasPrecision(18, 4)
                .HasConversion<double>()
                .IsRequired();

            entity.HasOne(x => x.Title)
                .WithMany()
                .HasForeignKey(x => x.TitleId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // ---------------------------
        // Title (словарь названий)
        // ---------------------------
        modelBuilder.Entity<Title>(entity =>
        {
            entity.HasKey(x => x.Id);

            entity.Property(x => x.Name)
                .IsRequired();

            entity.HasIndex(x => x.Name)
                .IsUnique();
        });

        // ---------------------------
        // Genre (словарь жанров)
        // ---------------------------
        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(x => x.Id);

            entity.Property(x => x.Name)
                .IsRequired();

            entity.HasIndex(x => x.Name)
                .IsUnique();
        });

        // ---------------------------
        // MaterialGenre (many-to-many)
        // ---------------------------
        modelBuilder.Entity<MaterialGenre>(entity =>
        {
            entity.HasKey(x => new { x.MaterialId, x.GenreId });

            entity.HasOne(x => x.Material)
                .WithMany(x => x.Genres)
                .HasForeignKey(x => x.MaterialId);

            entity.HasOne(x => x.Genre)
                .WithMany()
                .HasForeignKey(x => x.GenreId);
        });

        // ---------------------------
        // TitleGenre (жанры по умолчанию для Title)
        // ---------------------------
        modelBuilder.Entity<TitleGenre>(entity =>
        {
            entity.HasKey(x => new { x.TitleId, x.GenreId });

            entity.HasOne(x => x.Title)
                .WithMany(x => x.Genres)
                .HasForeignKey(x => x.TitleId);

            entity.HasOne(x => x.Genre)
                .WithMany()
                .HasForeignKey(x => x.GenreId);
        });
    }
}