using Google.Protobuf.WellKnownTypes;
using GrpcRiraClient.Models;
using GrpcRiraClient.Protos;
using GrpcRiraClient.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GrpcRiraClient.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly GrpcRiraClientService _userClient;
        private readonly ILogger<UsersController> _logger;
        public UsersController(GrpcRiraClientService userClient, ILogger<UsersController> logger)
        {
            _userClient = userClient;
            _logger = logger;
        }
        // GET: api/<UsersController>
        [HttpGet]
        public async Task<IEnumerable<Models.User>> Get()
        {
            try
            {
                var results = await _userClient.GetUsersAsync();
                var users = results.Users.Select(u => new Models.User(u)).ToArray();
                return users;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error on GetUsersAsync");
                throw;
            }
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public async Task<Models.User?> Get(string id)
        {
            try
            {
                var response = await _userClient.FindUserAsync(new Protos.UserIdentifier() { Id = id });
                if (response == null)
                {
                    Response.StatusCode = (int)HttpStatusCode.NotFound;
                    return null;
                }
                var result = new Models.User(response);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error on GetUserAsync [ {id}]");
                throw;
            }
        }

        // POST api/<UsersController>
        [HttpPost]
        public async Task<Models.User?> Post([FromBody] Models.UserInfo value)
        {
            if (value == null)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return null;
            }
            var request = new CreateUserRequest()
            {
                FirstName = value.FirstName,
                LastName = value.LastName,
                NationalCode = value.NationalCode,
                Birthday = value.Birthday.ToUniversalTime().ToTimestamp(),
            };
            try
            {
                var response = await _userClient.CreateUserAsync(request);
                if (response == null)
                {
                    Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    return null;
                }

                var result = new Models.User(response);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error on CreateUser");
                throw;
            }
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public async Task<Models.User?> Put(string id, [FromBody] Models.User value)
        {
            if (value == null)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return null;
            }

            var request = new UpdateUserRequest()
            {
                Id = value.Id,
                FirstName = value.FirstName,
                LastName = value.LastName,
                NationalCode = value.NationalCode,
                Birthday = value.Birthday.ToUniversalTime().ToTimestamp(),
            };

            try
            {
                var response = await _userClient.UpdateUserAsync(request);
                var result = new Models.User(response);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error on Update user [{request.Id}]");
                throw;
            }
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public async Task<Models.User?> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return null;
            }

            try
            {
                var response = await _userClient.DeleteUserAsync(new UserIdentifier() { Id = id });
                if (response == null)
                {
                    Response.StatusCode = (int)HttpStatusCode.NotFound;
                    return null;
                }

                var result = new Models.User()
                {
                    Id = response.Id,
                };

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error on DeleteUser [{id}]");
                throw;
            }
        }
    }
}
