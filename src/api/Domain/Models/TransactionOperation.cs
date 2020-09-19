using Domain.Models.Enums;
using Domain.Models.Base;

namespace Domain.Models
{
    public class TransactionOperation: BaseModel
    {
        public Operation Operation { get; set; }
        public OperationType Type { get; set; }
        
    }
}