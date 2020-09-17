using Infra.Context;
using Domain.Interfaces.Repositories.Base;
using System.Collections.Generic;
using System.Linq;

namespace Infra.Repositories.Base
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity: class {

        protected readonly ApiDbContext _context;

        public BaseRepository(ApiDbContext context) {
            _context = context;
        }

        public virtual void Add(TEntity obj) {
            _context.Set<TEntity>().Add(obj);
            _context.SaveChanges();
        }

        public virtual void Remove(TEntity obj) {
            _context.Set<TEntity>().Remove(obj);
            _context.SaveChanges();
        }

        public virtual void Update(TEntity obj) {
            _context.Set<TEntity>().Update(obj);
            _context.SaveChanges();
        }

        public virtual TEntity GetById(int id) {
            return _context.Set<TEntity>().Find(id);
        }

        public virtual IList<TEntity> GetAll() {
            return _context.Set<TEntity>().ToList();
        }

    }
}