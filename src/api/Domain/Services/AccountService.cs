using System;
using Domain.Services.Base;
using Domain.Interfaces.Services;
using Domain.Models;
using Domain.Models.Enums;
using Domain.Interfaces.Repositories;
using Domain.Dtos.Request;
using Domain.Dtos.Response;

namespace Domain.Services
{
    public class AccountService: BaseService<Account>, IAccountService
    {
        public readonly IAccountRepository _accountRepository;
        public readonly ITransactionService _transactionService;

        public AccountService(IAccountRepository AccountRepository, ITransactionService TransactionService)
            : base(AccountRepository)
        {
            _accountRepository = AccountRepository;
            _transactionService = TransactionService;
        }

        public virtual CreateAccountResponseDto CreateAccount(CreateAccountRequestDto dto) {
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
            Account account = findAccountIfExists(AccountId);
            return new AccountResponseDto { 
                Id = account.Id,
                Bank = account.Bank,
                Agency = account.Agency,
                Number = account.Number,
                Balance = account.Balance,
                CreatedAt = account.CreatedAt
            };
        }

        public virtual TransactionResponseDto Deposit(int AccountId, TransactionRequestDto dto) {
            Account account = findAccountIfExists(AccountId);
            Transaction transaction = this._transactionService.CreateTransaction(account.Id, Operation.Deposit, dto.Value);    

            updateAccountBalance(account, transaction);

            return new TransactionResponseDto { Success = true };
        }
        public virtual TransactionResponseDto Withdraw(int AccountId, TransactionRequestDto dto) {
            Account account = findAccountIfExists(AccountId);
            validateDebitFromAccount(account, dto.Value);
            Transaction transaction = this._transactionService.CreateTransaction(account.Id, Operation.Withdraw, dto.Value);

            updateAccountBalance(account, transaction);

            return new TransactionResponseDto { Success = true };
        }
        public virtual TransactionResponseDto Payment(int AccountId, TransactionRequestDto dto) {
            Account account = findAccountIfExists(AccountId);
            validateDebitFromAccount(account, dto.Value);
            Transaction transaction = this._transactionService.CreateTransaction(account.Id, Operation.Payment, dto.Value);
            
            updateAccountBalance(account, transaction);

            return new TransactionResponseDto { Success = true };
        }

        private void updateAccountBalance(Account account, Transaction transaction) {

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
                    throw new Exception("Invalid Operation Type!");
            }

            this._accountRepository.Update(account);
        }
        private void validateDebitFromAccount(Account account, decimal debitValue) {
            if (account.Balance < debitValue)
                throw new Exception("Insufficient balance.");
        }
        private Account findAccountIfExists(int id) {
            Account account = this._accountRepository.GetById(id);

            if (account == null)
                throw new Exception("Account not found.");

            return account;
        }
        
    }
}