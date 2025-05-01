using AiPlayground.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AiPlayground.DataAccess.Configuration;

public class ModelConfiguration : IEntityTypeConfiguration<Model>
{
    public void Configure(EntityTypeBuilder<Model> builder)
    {
        builder.ToTable("Model")
            .HasKey(m => m.Id);

        builder.Property(m => m.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasOne(m => m.Platform)
            .WithMany(p => p.Models)
            .HasForeignKey(m => m.PlatformId)
            .HasConstraintName("FK_Model_Platform");
        
        builder.HasData(new List<Model>
        {
            new Model
            {
                Id = 1,
                Name = "gpt-4o",
                PlatformId = 1,
            },
            new Model
            {
                Id = 2,
                Name = "gpt-4o-mini",
                PlatformId = 1,
            },
            new Model
            {
                Id = 3,
                Name = "Gemini 1.5",
                PlatformId = 3,
            },
            new Model
            {
                Id = 4,
                Name = "DeepSeek-R1",
                PlatformId = 2,
            },
            new Model
            {
                Id = 5,
                Name = "DeepSeek-V3",
                PlatformId = 2,
            },
        });
    }
}