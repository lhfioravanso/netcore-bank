using System;
using Domain.Services.Base;
using Domain.Interfaces.Services;
using Domain.Models;
using Domain.Interfaces.Repositories;
using System.Collections.Generic;
using Domain.Dtos.Request;

namespace Domain.Services
{
    public class DailyIncomeService: BaseService<IncomeProcessing>, IDailyIncomeService
    {
        private const decimal anualRate = 1.4M; // 1.4 % 
        private readonly IDailyIncomeRepository _dailyIncomeRepository;
        private readonly IAccountService _accountService;

        public DailyIncomeService(IDailyIncomeRepository dailyIncomeRepository, IAccountService accountService)
            : base(dailyIncomeRepository)
        {
            _dailyIncomeRepository = dailyIncomeRepository;
            _accountService = accountService;
        }

        public virtual bool TodayAlreadyProcessed() {
            IncomeProcessing incomeProcessing = this._dailyIncomeRepository.FindOneByDate(DateTime.Now);
 
            return incomeProcessing != null;
        }
        public virtual void ProcessDailyIncome() {
            if (!this.TodayAlreadyProcessed()) {
                IList<Account> accounts = this._accountService.GetAll();
                
                foreach (Account account in accounts)
                {
                    if (account.Balance > 0) {
                        decimal dailyIncome = this.calculateIncome(account.Balance);
                        this.saveAccountIncome(account, dailyIncome);
                    }
                }
                
                _dailyIncomeRepository.Add(new IncomeProcessing { 
                    ProcessedDate = DateTime.Now
                });
            }
        }

        private decimal calculateIncome(decimal balance) {
            int currentMonthDays = DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month);
            decimal monthlyRate = anualRate / 12;
            decimal dailyRate = ( monthlyRate / currentMonthDays ) / 100;
            
            return balance * dailyRate;
        }

        private void saveAccountIncome(Account account, decimal dailyIncome) {
            this._accountService.Income(account.Id, new CreateTransactionRequestDto { Value = dailyIncome });
        }
    }
}