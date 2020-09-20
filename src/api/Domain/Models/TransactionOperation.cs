using Domain.Models.Enums;
using Domain.Models.Base;
using System.Collections.Generic;

namespace Domain.Models
{
    public class TransactionOperation: BaseModel
    {
        public Operation Operation { get; set; }
        public OperationType Type { get; set; }
        public virtual IList<Transaction> Transactions { get; set; }
    }
}