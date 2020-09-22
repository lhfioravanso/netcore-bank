using Infra.Repositories.Base;
using Infra.Context;
using Domain.Models;
using Domain.Interfaces.Repositories;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;

namespace Infra.Repositories
{
    public class DailyIncomeRepository: BaseRepository<IncomeProcessing>, IDailyIncomeRepository {
        public DailyIncomeRepository(ApiDbContext context): base(context) { }

        public virtual IncomeProcessing FindOneByDate(DateTime date) {
            return this._context.IncomeProcessings.FirstOrDefault(i => i.ProcessedDate.Date.Equals(date.Date));
        }
    }

}