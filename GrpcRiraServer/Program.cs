//using GrpcRiraServer.Services;

using GrpcRiraServer.Data;
using GrpcRiraServer.Services;

namespace GrpcRiraServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.UseInMemoryDb();
            // Add services to the container.
            builder.Services.AddGrpc();

            var app = builder.Build();
            //app.Services.InitializeDatabase();

            // Configure the HTTP request pipeline.
            app.MapGrpcService<UserService>();
            app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

            app.Run();
        }
    }
}