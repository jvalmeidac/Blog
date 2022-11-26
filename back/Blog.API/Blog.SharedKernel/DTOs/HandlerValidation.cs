namespace Blog.SharedKernel.DTOs
{
    public record ValidatedResult<T> where T : class
    {
        public bool BadRequest { get; set; }
        public bool NotFound { get; set; }
        public bool Success => (!BadRequest && !NotFound) || !ErrorMessages.Any();

        public List<string> ErrorMessages { get; set; } = new List<string>();
        public T? Data { get; set; }
    }
}
