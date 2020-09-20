using Infra.Repositories.Base;
using Infra.Context;
using Domain.Models;
using Domain.Interfaces.Repositories;
using System.Linq;

namespace Infra.Repositories
{
    public class UserRepository: BaseRepository<User>, IUserRepository {
        public UserRepository(ApiDbContext context): base(context) { }

        public virtual User GetUserByUsername(string username) {
            return this._context.Users.SingleOrDefault(user => user.Username == username);
        }
    }

}