namespace Blog.Domain.CrossCutting.Entities
{
    public record UserAccount : IEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Username { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public Guid RoleId { get; set; }

        public Role? Role { get; set; }
    }
}
