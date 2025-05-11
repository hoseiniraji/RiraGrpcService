using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcRiraServer.Data;
using GrpcRiraServer.Protos;
using Microsoft.EntityFrameworkCore;

namespace GrpcRiraServer.Services
{
    public class UserService(DatabaseContext context) : UserProtoService.UserProtoServiceBase
    {
        private readonly DatabaseContext _context = context;

        public override async Task<User> CreateUser(CreateUserRequest request, ServerCallContext context)
        {
            var user = new Models.User()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                NationalCode = request.NationalCode,
                Birthday = request.Birthday.ToDateTime(),
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            var result = user.ToProtoModel();

            return result;
        }

        public override async Task<User> DeleteUser(UserIdentifier request, ServerCallContext context)
        {
            var entity = await _context.Users.FindAsync(request.Id);
            if (entity != null)
            {
                _context.Users.Remove(entity);
                await _context.SaveChangesAsync();

                return entity.ToProtoModel();
            }

            return new User()
            {
                Id = null
            };
        }

        public override async Task<User> GetUser(UserIdentifier request, ServerCallContext context)
        {
            var entity = await _context.Users.FindAsync(request.Id);
            return entity?.ToProtoModel() ?? throw new RpcException(new Status(StatusCode.NotFound, "UserNotFound"));
        }

        public override async Task<userListResponse> GetUsers(Empty request, ServerCallContext context)
        {
            var users = await _context.Users.ToListAsync();
            var result = new Google.Protobuf.Collections.RepeatedField<User>();
            result.AddRange(users.Select(u => u.ToProtoModel()).ToList());

            var response = new userListResponse();
            response.Users.AddRange(result);

            return response;
        }

        public override async Task<User> UpdateUser(UpdateUserRequest request, ServerCallContext context)
        {
            var entity = await _context.Users.FindAsync(request.Id);
            if (entity != null)
            {
                entity.FirstName = request.FirstName;
                entity.LastName = request.LastName;
                entity.NationalCode = request.NationalCode;
                entity.Birthday = request.Birthday.ToDateTime();
                await _context.SaveChangesAsync();

                return entity.ToProtoModel();
            }

            throw new RpcException(new Status(StatusCode.NotFound, "UserNotFound"));
        }
    }
}
