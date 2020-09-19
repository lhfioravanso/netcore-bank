using Domain.Interfaces.Services.Base;
using Domain.Models;
using Domain.Dtos.Request;
using Domain.Dtos.Response;

namespace Domain.Interfaces.Services {
    public interface IAccountService: IBaseService<Account> {
        TransactionResponseDto Deposit(int AccountId, TransactionRequestDto dto);
        TransactionResponseDto Withdraw(int AccountId, TransactionRequestDto dto);
        TransactionResponseDto Payment(int AccountId, TransactionRequestDto dto);
        AccountResponseDto GetAccount(int AccountId);
        CreateAccountResponseDto CreateAccount(CreateAccountRequestDto dto);
    }
}