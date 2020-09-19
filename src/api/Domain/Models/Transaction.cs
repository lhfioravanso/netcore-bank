using Domain.Models.Base;
using System;

namespace Domain.Models
{
    public class Transaction: BaseModel
    {
        public int AccountId { get; set; }
        public TransactionOperation TransactionOperation { get; set; }
        public decimal Value { get; set; }
        public DateTime CreatedAt { get; set; } 
    }
}