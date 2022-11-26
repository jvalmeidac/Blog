namespace Blog.API
{
    public static class IoC
    {
        public static void Register(IServiceCollection services)
        {
            Application.IoC.Register(services);
            Infrastructure.Data.IoC.Register(services);
        }
    }
}
