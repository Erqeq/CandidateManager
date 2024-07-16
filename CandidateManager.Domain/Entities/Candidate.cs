using CandidateManager.Domain.Interfaces;

namespace CandidateManager.Domain.Entities;

public class Candidate
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber {  get; set; }
    public string Email { get; set; }
    public DateTime? CallTime { get; set; }
    public string LinkedInUrl { get; set; }
    public string GitHubUrl { get; set; }
    public string Comment { get; set; }
}
