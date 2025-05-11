using System.ComponentModel.DataAnnotations;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;

namespace GrpcRiraClient.Models
{
    public class User : UserInfo
    {
        public User()
        {
            Id = string.Empty;
            FirstName = string.Empty;
            LastName = string.Empty;
            NationalCode = string.Empty;
        }

        public User(Protos.User user)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            NationalCode = user.NationalCode;
            Birthday = user.Birthday.ToDateTime();
        }

        public string Id { get; set; }


        public GrpcRiraClient.Protos.User ToProtoModel()
        {
            return new Protos.User
            {
                Id = Id,
                FirstName = FirstName,
                LastName = LastName,
                NationalCode = NationalCode,
                Birthday = Birthday.ToUniversalTime().ToTimestamp(),
            };
        }
    }
}

