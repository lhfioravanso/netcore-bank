using System;
using Domain.Services.Base;
using Domain.Interfaces.Services;
using Domain.Models;
using Domain.Models.Enums;
using Domain.Interfaces.Repositories;
using Domain.Dtos.Request;
using Domain.Dtos.Response;
using Domain.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Services
{
    public class AccountService: BaseService<Account>, IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionService _transactionService;
        private readonly IUserService _userService;

        public AccountService(IAccountRepository accountRepository, ITransactionService transactionService, IUserService userService)
            : base(accountRepository)
        {
            _accountRepository = accountRepository;
            _transactionService = transactionService;
            _userService = userService;
        }

        public virtual CreateAccountResponseDto CreateAccount(CreateAccountRequestDto dto) {

            ValidateIfUserExists(dto.UserId);

            Account account = new Account {
                UserId = dto.UserId,
                Bank = dto.Bank,
                Agency = dto.Agency,
                Number = dto.Number,
                Balance = 0,
                CreatedAt = DateTime.Now
            };

            this._accountRepository.Add(account);

            return new CreateAccountResponseDto { 
                Id = account.Id,
                CreatedAt = account.CreatedAt
            };
        }

        public virtual AccountResponseDto GetAccount(int AccountId) {
            Account account = this.FindAccountIfExists(AccountId);
            return new AccountResponseDto { 
                Id = account.Id,
                Bank = account.Bank,
                Agency = account.Agency,
                Number = account.Number,
                Balance = account.Balance,
                CreatedAt = account.CreatedAt
            };
        }

        public virtual IList<AccountTransactionResponseDto> GetAccountTransactions(int AccountId) {
            this.FindAccountIfExists(AccountId);
            IList<Transaction> transactions = this._transactionService.GetTransactionsByAccount(AccountId);            

            IList<AccountTransactionResponseDto> response = transactions.Select(t => new AccountTransactionResponseDto() {
                Id = t.Id,
                Value = t.Value,
                TransactionOperation = t.TransactionOperation.Operation.ToString(),
                CreatedAt = t.CreatedAt,
            }).ToList();

            return response;
        }

        public virtual CreateTransactionResponseDto Deposit(int AccountId, CreateTransactionRequestDto dto) {
            Account account = this.FindAccountIfExists(AccountId);
            Transaction transaction = this._transactionService.CreateTransaction(account.Id, Operation.Deposit, dto.Value);    

            this.UpdateAccountBalance(account, transaction);

            return new CreateTransactionResponseDto { Success = true };
        }
        public virtual CreateTransactionResponseDto Withdraw(int AccountId, CreateTransactionRequestDto dto) {
            Account account = this.FindAccountIfExists(AccountId);
            this.ValidateDebitFromAccount(account, dto.Value);
            Transaction transaction = this._transactionService.CreateTransaction(account.Id, Operation.Withdraw, dto.Value);

            this.UpdateAccountBalance(account, transaction);

            return new CreateTransactionResponseDto { Success = true };
        }
        public virtual CreateTransactionResponseDto Payment(int AccountId, CreateTransactionRequestDto dto) {
            Account account = this.FindAccountIfExists(AccountId);
            this.ValidateDebitFromAccount(account, dto.Value);
            Transaction transaction = this._transactionService.CreateTransaction(account.Id, Operation.Payment, dto.Value);
            
            this.UpdateAccountBalance(account, transaction);

            return new CreateTransactionResponseDto { Success = true };
        }

        private void UpdateAccountBalance(Account account, Transaction transaction) {

            switch (transaction.TransactionOperation.Type)
            {   
                case OperationType.Debit: {
                    account.Balance -= transaction.Value;
                    break;
                }
                case OperationType.Credit: {
                    account.Balance += transaction.Value;
                    break;
                }
                default:
                    throw new NotImplementedException("Invalid Operation Type!");
            }

            this._accountRepository.Update(account);
        }
        private void ValidateDebitFromAccount(Account account, decimal debitValue) {
            if (account.Balance < debitValue)
                throw new BusinessException("Insufficient balance.");
        }
        private Account FindAccountIfExists(int id) {
            Account account = this._accountRepository.GetById(id);

            if (account == null)
                throw new NotFoundException("Account not found.");

            return account;
        }
        private void ValidateIfUserExists(int id) {
            User user = this._userService.GetById(id);

            if (user == null)
                throw new NotFoundException("User not found.");
        }
        
    }
}