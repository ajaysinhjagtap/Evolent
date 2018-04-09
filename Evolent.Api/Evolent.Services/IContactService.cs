#region Namesapces

using Evolent.Entities;
using System.Collections.Generic;

#endregion

namespace Evolent.Services
{
    #region Interface

    /// <summary>
    /// <see cref="IContactService"/> Interface.
    /// </summary>
    public interface IContactService
    {
        IEnumerable<ContactEntity> GetAllContacts();
        ContactEntity GetContactById(int contactId);
        int CreateContact(ContactEntity contactEntity);
        bool UpdateContact(ContactEntity contactEntity);
        bool DeleteContact(int contactId);
    }

    #endregion
}
