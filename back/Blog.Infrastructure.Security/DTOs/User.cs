namespace Blog.Infrastructure.Security.DTOs
{
    public record User
    {
        public Guid UserId { get; set; }
        public string? Password { get; set; }
    }
}
