using Domain.Interfaces.Services.Base;
using Domain.Models;
using Domain.Dtos.Request;
using Domain.Dtos.Response;
using System.Collections.Generic;


namespace Domain.Interfaces.Services
{
    public interface IDailyIncomeService: IBaseService<IncomeProcessing> 
    {
        bool TodayAlreadyProcessed();
        void ProcessDailyIncome();
    }
}