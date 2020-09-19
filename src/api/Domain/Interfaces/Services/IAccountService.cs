using Domain.Interfaces.Services.Base;
using Domain.Models;

namespace Domain.Interfaces.Services {
    public interface IAccountService: IBaseService<Account> {
        void Deposit();
        void Withdraw();
        void Payment();
    }
}