using System;
using Domain.Models.Base;

namespace Domain.Models
{
    public class IncomeProcessing: BaseModel
    {
        public DateTime ProcessedDate { get; set; }
    }
}