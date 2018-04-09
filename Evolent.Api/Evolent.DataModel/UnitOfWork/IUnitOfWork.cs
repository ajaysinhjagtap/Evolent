#region Namespaces

using Evolent.DataModel.GenericRepository;

#endregion

namespace Evolent.DataModel.UnitOfWork
{
    /// <summary>
    /// <see cref="IUnitOfWork"/> interface. 
    /// </summary>
    public interface IUnitOfWork
    {
        IGenericRepository<Contact> ContactRepository { get; }
        void Save();
    }
}
