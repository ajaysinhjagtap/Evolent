using Evolent.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Evolent.Web.Services
{
    /// <summary>
    /// <see cref="IContactService"/> interface.
    /// </summary>
    public interface IContactService
    {
        Task<IEnumerable<ContactEntity>> GetAllContacts();
        Task<ContactEntity> GetContactById(int contactId);
        Task<int> CreateContact(ContactEntity contactEntity);
        Task<bool> UpdateContact(ContactEntity contactEntity);
        Task<bool> DeleteContact(int contactId);
    }
}