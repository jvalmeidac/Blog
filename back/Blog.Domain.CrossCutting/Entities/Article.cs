namespace Blog.Domain.CrossCutting
{
    public class Article : IEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Content { get; set; }
    }
}
