namespace GrpcRiraServer.Data
{
    public static class DbContextExternsions
    {
        public static void InitializeDatabase(this IServiceProvider serviceProvider)
        {
            var scope = serviceProvider.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
            db.Database.EnsureCreated();
        }

        public static void UseInMemoryDb(this IServiceCollection services)
        {
            services.AddDbContext<DatabaseContext>();
            var db = new DatabaseContext();
            db.Database.EnsureCreated();
            services.AddSingleton<DatabaseContext>(db);
        }
    }
}
