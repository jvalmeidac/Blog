namespace Blog.Domain.CrossCutting.Entities
{
    public record Role : IEntity
    {
        public Guid Id { get; set; }
        public string? Code { get; set; }
    }
}
