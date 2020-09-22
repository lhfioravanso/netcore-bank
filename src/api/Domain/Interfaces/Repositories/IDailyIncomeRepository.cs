using Domain.Interfaces.Repositories.Base;
using Domain.Models;
using System;

namespace Domain.Interfaces.Repositories
{
    public interface IDailyIncomeRepository: IBaseRepository<IncomeProcessing> {
        IncomeProcessing FindOneByDate(DateTime date); 
    }
}