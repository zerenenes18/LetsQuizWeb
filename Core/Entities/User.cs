using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.AccessControl;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Core.Entities;
using Core.Utilities;

namespace LetsQuizCore.Entities
{
    public class User : IEntity
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        
        [JsonConverter(typeof(MaskedByteArrayConverter))]
        public byte[] PasswordHash { get; set; }
        [JsonConverter(typeof(MaskedByteArrayConverter))]
        public byte[] PasswordSalt { get; set; }
        public string? UserName { get; set; }
        public string? Role { get; set; }
        public bool  Status { get; set; }
        public bool EmailControl { get; set; }
    }
    
    
}
