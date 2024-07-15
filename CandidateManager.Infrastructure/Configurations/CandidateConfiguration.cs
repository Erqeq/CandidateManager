using CandidateManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CandidateManager.Infrastructure.Configurations;

public class CandidateConfiguration : IEntityTypeConfiguration<Candidate>
{
    public void Configure(EntityTypeBuilder<Candidate> builder)
    {
        throw new NotImplementedException();
    }
}
