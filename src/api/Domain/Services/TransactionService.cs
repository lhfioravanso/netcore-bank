using System;
using Domain.Services.Base;
using Domain.Interfaces.Services;
using Domain.Models;
using Domain.Models.Enums;
using Domain.Interfaces.Repositories;

namespace Domain.Services
{
    public class TransactionService: BaseService<Transaction>, ITransactionService
    {
        public readonly ITransactionRepository _transactionRepository;

        public TransactionService(ITransactionRepository TransactionRepository)
            : base(TransactionRepository)
        {
            _transactionRepository = TransactionRepository;
        }

        public virtual Transaction CreateTransaction(int accountId, Operation operation, decimal value) {

            TransactionOperation transactionOperation = new TransactionOperation {
                Operation = operation,
                Type = getOperationType(operation)
            };

            Transaction transaction = new Transaction {
                AccountId = accountId, 
                TransactionOperation = transactionOperation,
                Value = value,
                CreatedAt = DateTime.Now
            };
            
            _transactionRepository.Add(transaction);

            return transaction;
        }

        private OperationType getOperationType(Operation operation) {
            switch (operation)
            {
                case Operation.Deposit:
                    return OperationType.Credit;
                case Operation.Payment:
                case Operation.Withdraw:
                    return OperationType.Debit;
                default:
                    throw new Exception("Invalid Operation.");
            }
        }
    }
}