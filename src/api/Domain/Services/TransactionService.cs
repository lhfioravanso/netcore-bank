using System;
using Domain.Services.Base;
using Domain.Interfaces.Services;
using Domain.Models;
using Domain.Models.Enums;
using Domain.Interfaces.Repositories;
using System.Collections.Generic;
using Domain.Exceptions;

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

        public virtual Transaction CreateTransaction(int accountId, Operation operation, decimal value) {

            TransactionOperation transactionOperation = this.FindTransactionOperationIfExists(operation);

            Transaction transaction = new Transaction {
                AccountId = accountId, 
                TransactionOperation = transactionOperation,
                Value = value,
                CreatedAt = DateTime.Now
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
                throw new NotFoundException("Transaction Operation not found, contact your system administrator.");

            return transactionOperation;
        }
        
    }
}