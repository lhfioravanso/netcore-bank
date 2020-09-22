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
    public class AccountServiceTest
    {

        [Fact]
        public void ShouldThrowUserNotFound () {
            var mocker = new AutoMocker();
            var accountService = mocker.CreateInstance<AccountService>();

            User user = new User { Id = 1 };
            mocker.GetMock<IUserService>().Setup(c => c.GetById(1))
                .Returns(user);

            CreateAccountRequestDto dto = new CreateAccountRequestDto { UserId = 2 }; 

            Assert.Throws<NotFoundException>(() => accountService.CreateAccount(dto));
        }

        [Fact]
        public void ShouldGetAccountById () {
            var mocker = new AutoMocker();
            var accountService = mocker.CreateInstance<AccountService>();

            Account accTest = new Account { Id = 1 };

            mocker.GetMock<IAccountRepository>().Setup(c => c.GetById(1))
                .Returns(accTest);

            var result = accountService.GetById(1);

            Assert.Equal(result.Id, 1);
        }

        [Fact]
        public void ShouldGetAllAccount () {
            var mocker = new AutoMocker();
            var accountService = mocker.CreateInstance<AccountService>();

            Account acc1 = new Account { Id = 1 };
            Account acc2 = new Account { Id = 2 };

            List<Account> listAcc = new List<Account>();
            listAcc.Add(acc1);
            listAcc.Add(acc2);

            mocker.GetMock<IAccountRepository>().Setup(c => c.GetAll())
                .Returns(listAcc);

            var result = accountService.GetAll();

            Assert.Equal(result[0].Id, 1);
            Assert.Equal(result[1].Id, 2);
        }

        [Fact]
        public void ShouldDepositWithSuccess () {
            decimal transactionValue = 100;
            var mocker = new AutoMocker();
            var accountService = mocker.CreateInstance<AccountService>();

            Account acc = new Account { Id = 1 };

            mocker.GetMock<IAccountRepository>().Setup(c => c.GetById(1))
                .Returns(acc);
            
            mocker.GetMock<ITransactionService>().Setup(c => c.CreateTransaction(acc, Operation.Deposit, transactionValue))
                .Returns(this.getCreditTransaction(transactionValue));
            
            CreateTransactionRequestDto dto = new CreateTransactionRequestDto { Value = transactionValue };
            var result = accountService.Deposit(1, dto);

            Assert.Equal(result.Balance, transactionValue);
        }

        [Fact]
        public void ShouldWithdrawWithSuccess () {
            decimal transactionValue = 100;
            var mocker = new AutoMocker();
            var accountService = mocker.CreateInstance<AccountService>();

            Account acc = new Account { Id = 1, Balance = 300 };

            mocker.GetMock<IAccountRepository>().Setup(c => c.GetById(1))
                .Returns(acc);
            
            mocker.GetMock<ITransactionService>().Setup(c => c.CreateTransaction(acc, Operation.Withdraw, transactionValue))
                .Returns(this.getDebitTransaction(transactionValue));
            
            CreateTransactionRequestDto dto = new CreateTransactionRequestDto { Value = transactionValue };
            var result = accountService.Withdraw(1, dto);

            Assert.Equal(result.Balance, 200);
        }

        [Fact]
        public void ShouldPayWithSuccess () {
            decimal transactionValue = 100;
            var mocker = new AutoMocker();
            var accountService = mocker.CreateInstance<AccountService>();

            Account acc = new Account { Id = 1, Balance = 300 };

            mocker.GetMock<IAccountRepository>().Setup(c => c.GetById(1))
                .Returns(acc);
            
            mocker.GetMock<ITransactionService>().Setup(c => c.CreateTransaction(acc, Operation.Payment, transactionValue))
                .Returns(this.getDebitTransaction(transactionValue));
            
            CreateTransactionRequestDto dto = new CreateTransactionRequestDto { Value = transactionValue };
            var result = accountService.Payment(1, dto);

            Assert.Equal(result.Balance, 200);
        }

        [Fact]
        public void ShouldThrowUnsufficientBalanceOnPayment() {
            decimal transactionValue = 500;
            var mocker = new AutoMocker();
            var accountService = mocker.CreateInstance<AccountService>();

            Account acc = new Account { Id = 1, Balance = 300 };

            mocker.GetMock<IAccountRepository>().Setup(c => c.GetById(1))
                .Returns(acc);
            
            mocker.GetMock<ITransactionService>().Setup(c => c.CreateTransaction(acc, Operation.Payment, transactionValue))
                .Returns(this.getDebitTransaction(transactionValue));
            
            CreateTransactionRequestDto dto = new CreateTransactionRequestDto { Value = transactionValue };

            Assert.Throws<BusinessException>(() => accountService.Payment(1, dto));
        }

        [Fact]
        public void ShouldThrowUnsufficientBalanceOnWithdraw() {
            decimal transactionValue = 301;
            var mocker = new AutoMocker();
            var accountService = mocker.CreateInstance<AccountService>();

            Account acc = new Account { Id = 1, Balance = 300 };

            mocker.GetMock<IAccountRepository>().Setup(c => c.GetById(1))
                .Returns(acc);
            
            mocker.GetMock<ITransactionService>().Setup(c => c.CreateTransaction(acc, Operation.Withdraw, transactionValue))
                .Returns(this.getDebitTransaction(transactionValue));
            
            CreateTransactionRequestDto dto = new CreateTransactionRequestDto { Value = transactionValue };
            
            Assert.Throws<BusinessException>(() => accountService.Withdraw(1, dto));
        }


        [Fact]
        public void ShouldThrowTransactionValueMustBeHigherThanZero() {
            decimal transactionValue = 0;
            var mocker = new AutoMocker();
            var accountService = mocker.CreateInstance<AccountService>();

            Account acc = new Account { Id = 1 };

            mocker.GetMock<IAccountRepository>().Setup(c => c.GetById(1))
                .Returns(acc);
            
            mocker.GetMock<ITransactionService>().Setup(c => c.CreateTransaction(acc, Operation.Deposit, transactionValue))
                .Returns(this.getCreditTransaction(transactionValue));
            
            CreateTransactionRequestDto dto = new CreateTransactionRequestDto { Value = transactionValue };
            Assert.Throws<BusinessException>(() => accountService.Deposit(1, dto));
        }

        [Fact]
        public void ShouldThrowTransactionValueMustBeHigherThanZero2() {
            decimal transactionValue = -99;
            var mocker = new AutoMocker();
            var accountService = mocker.CreateInstance<AccountService>();

            Account acc = new Account { Id = 1 };

            mocker.GetMock<IAccountRepository>().Setup(c => c.GetById(1))
                .Returns(acc);
            
            mocker.GetMock<ITransactionService>().Setup(c => c.CreateTransaction(acc, Operation.Deposit, transactionValue))
                .Returns(this.getCreditTransaction(transactionValue));
            
            CreateTransactionRequestDto dto = new CreateTransactionRequestDto { Value = transactionValue };
            Assert.Throws<BusinessException>(() => accountService.Deposit(1, dto));
        }


        private Transaction getCreditTransaction(decimal value) {
            return new Transaction {
                Value = value,
                TransactionOperation = new TransactionOperation { Type = OperationType.Credit }
            };
        }

        private Transaction getDebitTransaction(decimal value) {
            return new Transaction {
                Value = value,
                TransactionOperation = new TransactionOperation { Type = OperationType.Debit }
            };
        }
    }
}