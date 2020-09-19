using System;
using Domain.Services.Base;
using Domain.Interfaces.Services;
using Domain.Models;
using Domain.Models.Enums;
using Domain.Interfaces.Repositories;

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

        public virtual void Deposit() {
            Account account = findAccountIfExists(1);
            Transaction transaction = this._transactionService.CreateTransaction(account.Id, Operation.Deposit, 10);
            updateAccountBalance(account, transaction);
        }
        public virtual void Withdraw() {
            Account account = findAccountIfExists(1);
            validateDebitFromAccount(account, 10);
            Transaction transaction = this._transactionService.CreateTransaction(account.Id, Operation.Withdraw, 10);
            updateAccountBalance(account, transaction);
        }
        public virtual void Payment() {
            Account account = findAccountIfExists(1);
            validateDebitFromAccount(account, 10);
            Transaction transaction = this._transactionService.CreateTransaction(account.Id, Operation.Payment, 10);
            updateAccountBalance(account, transaction);
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