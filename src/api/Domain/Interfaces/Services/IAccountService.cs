using Domain.Interfaces.Services.Base;
using Domain.Models;
using Domain.Dtos.Request;
using Domain.Dtos.Response;

namespace Domain.Interfaces.Services {
    public interface IAccountService: IBaseService<Account> {
        void Deposit(int AccountId, TransactionRequestDto dto);
        void Withdraw(int AccountId, TransactionRequestDto dto);
        void Payment(int AccountId, TransactionRequestDto dto);
        AccountResponseDto GetAccount(int AccountId);
    }
}