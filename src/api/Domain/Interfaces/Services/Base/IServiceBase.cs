using System.Collections.Generic;

namespace Domain.Interfaces.Services.Base {
    public interface IBaseService<TEntity> where TEntity: class {
        public void Add(TEntity obj);
        public void Remove(TEntity obj);
        public void Update(TEntity obj);
        public IList<TEntity> GetAll();
        public TEntity GetById(int id);
    }
}