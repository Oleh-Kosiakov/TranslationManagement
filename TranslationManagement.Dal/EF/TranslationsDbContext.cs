using Microsoft.EntityFrameworkCore;
using TranslationManagement.Domain.Entities;

namespace TranslationManagement.Dal.EF;

public class TranslationsDbContext : DbContext
{
    public DbSet<TranslationJob> TranslationJobsSet { get; set; }

    public DbSet<Translator> TranslatorSet { get; set; }

    public TranslationsDbContext(DbContextOptions<TranslationsDbContext> dbContextOptions) : base(dbContextOptions)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TranslationJob>(entity =>
        {
            entity.ToTable("TranslationJobs");

            entity.Property(e => e.Id)
                .HasColumnName("Id")
                .HasColumnType("INTEGER");

            entity.Property(e => e.CustomerName)
                .HasColumnName("CustomerName")
                .HasColumnType("TEXT");

            entity.Property(e => e.Status)
                .HasColumnName("Status")
                .HasColumnType("TEXT");

            entity.Property(e => e.OriginalContent)
                .HasColumnName("OriginalContent")
                .HasColumnType("TEXT");

            entity.Property(e => e.TranslatedContent)
                .HasColumnName("TranslatedContent")
                .HasColumnType("TEXT");

            entity.Property(e => e.Price)
                .HasColumnName("Price")
                .HasColumnType("REAL");
        });

        modelBuilder.Entity<Translator>(entity =>
        {
            entity.ToTable("Translators");

            entity.Property(e => e.Id)
                .HasColumnName("Id")
                .HasColumnType("INTEGER");

            entity.Property(e => e.Name)
                .HasColumnName("Name")
                .HasColumnType("TEXT");

            entity.Property(e => e.HourlyRate)
                .HasColumnName("HourlyRate")
                .HasColumnType("TEXT");

            entity.Property(e => e.Status)
                .HasColumnName("Status")
                .HasColumnType("TEXT");

            entity.Property(e => e.CreditCardNumber)
                .HasColumnName("CreditCardNumber")
                .HasColumnType("TEXT");
        });
    }
}