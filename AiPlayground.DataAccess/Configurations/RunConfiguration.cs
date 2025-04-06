using AiPlayground.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AiPlayground.DataAccess.Configurations;

public class RunConfiguration : IEntityTypeConfiguration<Run>
{
    public void Configure(EntityTypeBuilder<Run> builder)
    {
        builder.ToTable("Run")
            .HasKey(r => r.Id);

        builder.Property(p => p.ActualResponse)
            .IsRequired()
            .HasMaxLength(10_000);
            
        builder.Property(p => p.Rating)
            .HasColumnType("decimal(5,2)");
            
        builder.Property(p => p.UserRating)
            .HasColumnType("decimal(5,2)");
            
        builder.Property(p => p.Temp)
            .HasColumnType("decimal(5,2)");
        
        builder.HasOne(r => r.Model)
            .WithOne(m => m.Run)
            .HasForeignKey<Run>(r => r.ModelId)
            .HasConstraintName("FK_Run_Model");
        
        builder.HasOne(r => r.Prompt)
            .WithOne(p => p.Run)
            .HasForeignKey<Run>(r => r.PromptId)
            .HasConstraintName("FK_Run_Prompt");
    }
}