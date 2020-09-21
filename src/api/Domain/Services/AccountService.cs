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
using Domain.Messages;
using System.Text;

namespace Domain.Services
{
    public class AccountService: BaseService<Account>, IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionService _transactionService;
        private readonly IUserService _userService;

        private const string BANK_NUMBER = "001";
        private const string AGENCY_NUMBER = "1234";

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
                OperationType = t.TransactionOperation.Type.ToString(),
                CreatedAt = t.CreatedAt,
                PreviousBalance = t.PreviousBalance
            }).ToList();

            return response;
        }

        public virtual CreateTransactionResponseDto Deposit(int accountId, CreateTransactionRequestDto dto) {
            return this.MakeTransaction(accountId, dto.Value, Operation.Deposit, false);
        }
        public virtual CreateTransactionResponseDto Withdraw(int accountId, CreateTransactionRequestDto dto) {
            return this.MakeTransaction(accountId, dto.Value, Operation.Withdraw, true);
        }
        public virtual CreateTransactionResponseDto Payment(int accountId, CreateTransactionRequestDto dto) {
            return this.MakeTransaction(accountId, dto.Value, Operation.Payment, true);
        }

        public int CreateFirstAccount(int userId) {
            Random random = new Random();
            CreateAccountResponseDto account = this.CreateAccount(new CreateAccountRequestDto {
                UserId = userId,
                Agency = AGENCY_NUMBER,
                Bank = BANK_NUMBER,
                Number = random.Next(10000000, 99999999).ToString()
            });

            return account.Id;
        }

        private CreateTransactionResponseDto MakeTransaction(int accountId, decimal value, Operation operation, bool isDebit) {
            Account account = this.FindAccountIfExists(accountId);
            this.ValidateTransaction(account, value, isDebit);
            Transaction transaction = this._transactionService.CreateTransaction(account, operation, value);
            decimal newBalance = this.UpdateAccountBalance(account, transaction);

            return new CreateTransactionResponseDto { Success = true, Balance = newBalance };
        }

        private decimal UpdateAccountBalance(Account account, Transaction transaction) {

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
                    throw new NotImplementedException(MessagesUtil.InvalidOperationType);
            }

            this._accountRepository.Update(account);
            return account.Balance;
        }
        private void ValidateTransaction(Account account, decimal value, bool isDebit) {
            if (value <= 0)
                throw new BusinessException(MessagesUtil.TransactionValueMustBeHigherThanZero);

            if (isDebit)
                this.ValidateDebitFromAccount(account, value);
        }
        private void ValidateDebitFromAccount(Account account, decimal debitValue) {
            if (account.Balance < debitValue)
                throw new BusinessException(MessagesUtil.InsufficientBalance);
        }
        private Account FindAccountIfExists(int id) {
            Account account = this._accountRepository.GetById(id);

            if (account == null)
                throw new NotFoundException(MessagesUtil.AccountNotFound);

            return account;
        }
        private void ValidateIfUserExists(int id) {
            User user = this._userService.GetById(id);

            if (user == null)
                throw new NotFoundException(MessagesUtil.UserNotFound);
        }
        
    }
}