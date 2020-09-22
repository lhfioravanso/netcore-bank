using Xunit;
using System;
using Moq;
using Moq.AutoMock;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Services;
using Domain.Dtos.Request;
using Domain.Dtos.Response;
using Domain.Models;
using Domain.Exceptions;
using System.Collections.Generic;
using Domain.Models.Enums;

namespace Test.Domain.Services
{
    public class TransactionServiceTest
    {
        [Fact]
        public void ShouldThrowTransactionOperationNotFound () {
            var mocker = new AutoMocker();
            var transactionService = mocker.CreateInstance<TransactionService>();

            Account acc = new Account();

            TransactionOperation to = new TransactionOperation { Id = 1 };
            mocker.GetMock<ITransactionOperationRepository>().Setup(c => c.GetByOperation(Operation.Withdraw))
                .Returns(to);

            Assert.Throws<NotFoundException>(() => transactionService.CreateTransaction(acc, Operation.Deposit, 1));
        }


        [Fact]
        public void ShouldCreateWithdrawTransaction () {
            Operation operation = Operation.Withdraw;

            var mocker = new AutoMocker();
            var transactionService = mocker.CreateInstance<TransactionService>();

            Account acc = new Account();

            TransactionOperation to = new TransactionOperation { Id = 1, Operation = operation };
            mocker.GetMock<ITransactionOperationRepository>().Setup(c => c.GetByOperation(operation))
                .Returns(to);

            Transaction result = transactionService.CreateTransaction(acc, operation, 1);
            
            Assert.Equal(result.Value, 1);
            Assert.Equal(result.TransactionOperation.Operation, operation);
        }

        [Fact]
        public void ShouldCreatePaymentTransaction () {
            Operation operation = Operation.Payment;

            var mocker = new AutoMocker();
            var transactionService = mocker.CreateInstance<TransactionService>();

            Account acc = new Account();

            TransactionOperation to = new TransactionOperation { Id = 1, Operation = operation };
            mocker.GetMock<ITransactionOperationRepository>().Setup(c => c.GetByOperation(operation))
                .Returns(to);

            Transaction result = transactionService.CreateTransaction(acc, operation, 1);
            
            Assert.Equal(result.Value, 1);
            Assert.Equal(result.TransactionOperation.Operation, operation);
        }

        [Fact]
        public void ShouldCreateDepositTransaction () {
            Operation operation = Operation.Deposit;

            var mocker = new AutoMocker();
            var transactionService = mocker.CreateInstance<TransactionService>();

            Account acc = new Account();

            TransactionOperation to = new TransactionOperation { Id = 1, Operation = operation };
            mocker.GetMock<ITransactionOperationRepository>().Setup(c => c.GetByOperation(operation))
                .Returns(to);

            Transaction result = transactionService.CreateTransaction(acc, operation, 1);
            
            Assert.Equal(result.Value, 1);
            Assert.Equal(result.TransactionOperation.Operation, operation);
        }
    }
}