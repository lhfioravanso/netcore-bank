using Infra.Repositories.Base;
using Infra.Context;
using Domain.Models;
using Domain.Interfaces.Repositories;

namespace Infra.Repositories
{
    public class AccountRepository: BaseRepository<Account>, IAccountRepository {

        public AccountRepository(ApiDbContext context): base(context) {

        }

    }

}