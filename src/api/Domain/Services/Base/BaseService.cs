using System.Collections.Generic;
using Domain.Interfaces.Services.Base;
using Domain.Interfaces.Repositories.Base;

namespace Domain.Services.Base
{
    public abstract class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class 
    {
        private readonly IBaseRepository<TEntity> _repository;

        public BaseService(IBaseRepository<TEntity> Repository)
        {
            _repository = Repository;
        }
        
        public virtual void Add(TEntity obj) {
            this._repository.Add(obj);
        }

        public virtual void Remove(TEntity obj) {
            this._repository.Remove(obj);
        }

        public virtual void Update(TEntity obj) {
            this._repository.Update(obj);
        }

        public virtual IList<TEntity> GetAll()
        {
            return this._repository.GetAll();
        }

        public virtual TEntity GetById(int id) {
            return this._repository.GetById(id);
        }
    }
}