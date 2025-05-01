using AiPlayground.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AiPlayground.DataAccess.Configuration;

public class PromptConfiguration : IEntityTypeConfiguration<Prompt>
{
    public void Configure(EntityTypeBuilder<Prompt> builder)
    {
        builder.ToTable("Prompt")
            .HasKey(p => p.Id);

        builder.Property(p => p.UserMessage)
            .IsRequired()
            .HasMaxLength(10000);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.SystemMsg)
            .IsRequired()
            .HasMaxLength(10000);

        builder.Property(p => p.ExpectedResponse)
            .IsRequired()
            .HasMaxLength(1000);

        builder.HasOne(p => p.Scope)
            .WithMany(s => s.Prompts)
            .HasForeignKey(p => p.ScopeId)
            .HasConstraintName("FK_Prompt_Scope");
    }
}