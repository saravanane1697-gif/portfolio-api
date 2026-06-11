namespace PortfolioAPI.Models
{
    public class Profile
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Title { get; set; }

        public string AboutMe { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string LinkedInUrl { get; set; }

        public string GitHubUrl { get; set; }
        public string? ImageUrl { get; set; }
        public string? ResumeUrl { get; set; }
    }
}
