#region Namespaces

using System.Collections.Generic;
using Evolent.Entities;
using Evolent.DataModel.UnitOfWork;
using System.Linq;
using AutoMapper;
using Evolent.DataModel;
using System.Transactions;

#endregion

namespace Evolent.Services
{
    #region Class

    /// <summary>
    /// <see cref="ContactService"/> class implements IContactService.
    /// </summary>
    public class ContactService : IContactService
    {
        #region Private Variables

        private readonly IUnitOfWork _unitOfWork;


        #endregion

        #region Constructors

        /// <summary>
        /// Public constructor.
        /// </summary>
        public ContactService()
        {
            _unitOfWork = new UnitOfWork();
        }

        #endregion

        #region Functions and Methods

        /// <summary>
        /// Get All Contacts
        /// </summary>
        /// <returns>returns IEnumarable list of Contact Entity</returns>
        public IEnumerable<ContactEntity> GetAllContacts()
        {
            var contacts = _unitOfWork.ContactRepository.GetAll().Where(cont => !cont.IsDeleted).ToList();
            if (contacts.Any())
            {
                Mapper.CreateMap<Contact, ContactEntity>();
                var productsModel = Mapper.Map<List<Contact>, List<ContactEntity>>(contacts);
                return productsModel;
            }
            return null;
        }

        /// <summary>
        /// Get Contact By ContactId
        /// </summary>
        /// <param name="contactId">contactId</param>
        /// <returns>returns contact details for given contact id</returns>
        public ContactEntity GetContactById(int contactId)
        {
            var contact = _unitOfWork.ContactRepository.GetByID(contactId);
            if (contact != null)
            {
                Mapper.CreateMap<Contact, ContactEntity>();
                var contactEntity = Mapper.Map<Contact, ContactEntity>(contact);
                return contactEntity;
            }
            return null;
        }

        /// <summary>
        /// Create Contact
        /// </summary>
        /// <param name="contactEntity">contactEntity</param>
        /// <returns>returns contact id </returns>
        public int CreateContact(ContactEntity contactEntity)
        {
            using (var scope = new TransactionScope())
            {
                Mapper.CreateMap<ContactEntity, Contact>();
                var contact = Mapper.Map<ContactEntity, Contact>(contactEntity);
                _unitOfWork.ContactRepository.Insert(contact);
                _unitOfWork.Save();
                scope.Complete();
                return contact.ContactId;
            }
        }

        /// <summary>
        /// Update Contact
        /// </summary>
        /// <param name="contactEntity">contactEntity</param>
        /// <returns>returns true if updated else false</returns>
        public bool UpdateContact(ContactEntity contactEntity)
        {
            var success = false;
            if (contactEntity != null)
            {
                using (var scope = new TransactionScope())
                {
                    var contact = _unitOfWork.ContactRepository.GetByID(contactEntity.ContactId);
                    if (contact != null)
                    {
                        Mapper.CreateMap<ContactEntity, Contact>();
                        Mapper.Map(contactEntity, contact);
                        _unitOfWork.ContactRepository.Update(contact);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }

        /// <summary>
        /// Delete Contact
        /// </summary>
        /// <param name="contactId">contactId</param>
        /// <returns>returns true if deleted else false</returns>
        public bool DeleteContact(int contactId)
        {
            var success = false;
            if (contactId > 0)
            {
                using (var scope = new TransactionScope())
                {
                    var contact = _unitOfWork.ContactRepository.GetByID(contactId);
                    if (contact != null)
                    {
                        contact.IsDeleted = true;
                        _unitOfWork.ContactRepository.Update(contact);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }

        #endregion
    }

    #endregion
}
