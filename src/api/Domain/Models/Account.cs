using Domain.Models.Base;
using System;
using System.Collections.Generic;

namespace Domain.Models {
    public class Account: BaseModel {
        public string Bank { get; set; }
        public string Agency { get; set; }
        public string Number { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal Balance { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public virtual IList<Transaction> Transactions { get; set; }
    }
}