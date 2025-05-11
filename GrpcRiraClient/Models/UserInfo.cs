using System.ComponentModel.DataAnnotations;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;

namespace GrpcRiraClient.Models
{
    public class UserInfo
    {
        public UserInfo()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            NationalCode = string.Empty;
        }


        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalCode { get; set; }
        public DateTime Birthday { get; set; }

    }
}

