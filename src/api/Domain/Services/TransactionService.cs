using System;
using Domain.Services.Base;
using Domain.Interfaces.Services;
using Domain.Models;
using Domain.Models.Enums;
using Domain.Interfaces.Repositories;
using System.Collections.Generic;
using Domain.Exceptions;
using Domain.Messages;

namespace Domain.Services
{
    public class TransactionService: BaseService<Transaction>, ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly ITransactionOperationRepository _transactionOperationRepository;

        public TransactionService(ITransactionRepository transactionRepository, ITransactionOperationRepository transactionOperationRepository)
            : base(transactionRepository)
        {
            this._transactionRepository = transactionRepository;
            this._transactionOperationRepository = transactionOperationRepository;
        }

        public virtual Transaction CreateTransaction(Account account, Operation operation, decimal value) {

            TransactionOperation transactionOperation = this.FindTransactionOperationIfExists(operation);

            Transaction transaction = new Transaction {
                AccountId = account.Id, 
                TransactionOperation = transactionOperation,
                Value = value,
                CreatedAt = DateTime.Now,
                PreviousBalance = account.Balance
            };
            
            _transactionRepository.Add(transaction);

            return transaction;
        }

        public virtual IList<Transaction> GetTransactionsByAccount(int accountId){
            return _transactionRepository.GetTransactionsByAccount(accountId);
        }

        private TransactionOperation FindTransactionOperationIfExists(Operation operation) {
            TransactionOperation transactionOperation = this._transactionOperationRepository.GetByOperation(operation);

            if (transactionOperation == null)
                throw new NotFoundException(MessagesUtil.TransactionOperationNotFound);

            return transactionOperation;
        }
        
    }
}