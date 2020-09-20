using Domain.Interfaces.Services.Base;
using Domain.Models;
using Domain.Dtos.Request;
using Domain.Dtos.Response;
using System.Collections.Generic;

namespace Domain.Interfaces.Services {
    public interface IAccountService: IBaseService<Account> {
        CreateTransactionResponseDto Deposit(int AccountId, CreateTransactionRequestDto dto);
        CreateTransactionResponseDto Withdraw(int AccountId, CreateTransactionRequestDto dto);
        CreateTransactionResponseDto Payment(int AccountId, CreateTransactionRequestDto dto);
        AccountResponseDto GetAccount(int AccountId);
        CreateAccountResponseDto CreateAccount(CreateAccountRequestDto dto);
        IList<AccountTransactionResponseDto> GetAccountTransactions(int AccountId);
    }
}