using Infra.Repositories.Base;
using Infra.Context;
using Domain.Models;
using Domain.Interfaces.Repositories;

namespace Infra.Repositories
{
    public class UserRepository: BaseRepository<User>, IUserRepository {

        public UserRepository(ApiDbContext context): base(context) {

        }

    }

}