using Domain.Models.Base;
using System;

namespace Domain.Models {
    public class User: BaseModel {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}