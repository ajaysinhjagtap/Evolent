#region Namespaces

using System.Collections.Generic;

#endregion

namespace Evolent.DataModel.GenericRepository
{
    /// <summary>
    /// <see cref="IGenericRepository"/> interface.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        TEntity GetByID(object id);
        void Insert(TEntity entity);
        void Update(TEntity entityToUpdate);
        void Delete(object id);
        void Delete(TEntity entityToDelete);
    }
}
