using Google.Protobuf.WellKnownTypes;
using System.ComponentModel.DataAnnotations;

namespace GrpcRiraServer.Models
{
    public class User
    {
        public User()
        {
            Id = Guid.NewGuid().ToString("N");
            FirstName = string.Empty;
            LastName = string.Empty;
            NationalCode = string.Empty;
        }
        public string Id { get; set; }
        [StringLength(32)]
        public string FirstName { get; set; }
        [StringLength(32)]
        public string LastName { get; set; }
        [StringLength(10)]
        public string NationalCode { get; set; }
        public DateTime Birthday { get; set; }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }

        public GrpcRiraServer.Protos.User ToProtoModel()
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
