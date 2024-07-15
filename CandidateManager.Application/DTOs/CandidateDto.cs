namespace CandidateManager.Application.DTOs;

public class CandidateDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public DateTime? CallTime { get; set; }
    public string LinkedInUrl { get; set; }
    public string GitHubUrl { get; set; }
    public string Comment { get; set; }
}
