using Domain.Dtos.Response.Base;
using System;

namespace Domain.Dtos.Request
{
    public class AccountTransactionResponseDto: BaseResponseDto
    {
        public DateTime CreatedAt { get; set; }
        public decimal Value { get; set; }
        public string TransactionOperation { get; set; }
    }
}