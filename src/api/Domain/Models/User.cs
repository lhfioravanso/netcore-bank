using Domain.Models.Base;
using System;
using System.Collections.Generic;

namespace Domain.Models {
    public class User: BaseModel {
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual IList<Account> Accounts { get; set; }
    }
}