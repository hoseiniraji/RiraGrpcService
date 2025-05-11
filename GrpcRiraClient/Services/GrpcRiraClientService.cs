using Grpc.Net.Client;
using GrpcRiraClient.Protos;

namespace GrpcRiraClient.Services
{
    public class GrpcRiraClientService : IDisposable
    {
        private readonly GrpcChannel channel;
        private readonly UserProtoService.UserProtoServiceClient Client;
        public GrpcRiraClientService(string serverUri)
        {
            channel = GrpcChannel.ForAddress(serverUri);
            Client = new UserProtoService.UserProtoServiceClient(channel);
        }

        public async Task<User> CreateUserAsync(CreateUserRequest request)
        {
            User response = await Client.CreateUserAsync(request);
            return response;
        }

        public async Task<User> FindUserAsync(UserIdentifier request)
        {
            var response = await Client.GetUserAsync(request);
            return response;
        }

        public async Task<userListResponse> GetUsersAsync()
        {
            var response = await Client.GetUsersAsync(new Google.Protobuf.WellKnownTypes.Empty());
            return response;
        }

        public async Task<User> UpdateUserAsync(UpdateUserRequest request)
        {
            User response = await Client.UpdateUserAsync(request);
            return response;
        }

        public async Task<User> DeleteUserAsync(UserIdentifier request)
        {
            var response = await Client.DeleteUserAsync(request);
            return response;
        }

        public void Dispose()
        {
            channel.Dispose();
            GC.SuppressFinalize(this);
        }
    }

    public static class GrpcRiraClientServiceExtension
    {
        public static IServiceCollection UseUserClient(this IServiceCollection services, IConfiguration configuration)
        {
            var server = configuration.GetValue<string>("GrpcServer");
            if (!string.IsNullOrEmpty(server))
            {
                var client = new GrpcRiraClientService(server);
                services.AddSingleton(client);
            }

            return services;
        }
    }
}
