namespace Blog.Infrastructure.Security.DTOs
{
    public record Token
    {
        public string? AccessToken { get; set; }
        public bool Authenticated { get; set; }
        public string? Message { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? Expiration { get; set; }
    }
}
