using Infra.Repositories.Base;
using Infra.Context;
using Domain.Models;
using Domain.Interfaces.Repositories;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories
{
    public class UserRepository: BaseRepository<User>, IUserRepository {
        public UserRepository(ApiDbContext context): base(context) { }

        public override User GetById(int id) {
            return this._context.Users.Include(user=>user.Accounts).SingleOrDefault(user => user.Id == id);
        }

        public virtual User GetUserByUsername(string username) {
            return this._context.Users.Include(user=>user.Accounts).SingleOrDefault(user => user.Username == username);
        }
    }

}