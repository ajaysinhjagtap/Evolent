#region Namespaces

using Evolent.Entities;
using Evolent.Services;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Linq;

#endregion

namespace Evolent.WebApi.Controllers
{
    #region Class

    /// <summary>
    /// <see cref="ContactsController"/> class inherits ApiController class.
    /// </summary>
    public class ContactsController : ApiController
    {
        #region Private Variables

        private readonly IContactService _contactService;

        #endregion

        #region Public Constructor

        /// <summary>
        /// Public constructor to initialize contact service instance
        /// </summary>
        public ContactsController()
        {
            _contactService = new ContactService();
        }

        #endregion

        [HttpGet]
        public HttpResponseMessage Get()
        {
            var contacts = _contactService.GetAllContacts();
            if (contacts != null)
            {
                var contactEntities = contacts as List<ContactEntity> ?? contacts.ToList();
                if (contactEntities.Any())
                    return Request.CreateResponse(HttpStatusCode.OK, contactEntities);
            }          
            return Request.CreateResponse(HttpStatusCode.OK, new List<ContactEntity>());
        }

        [HttpGet]
        public HttpResponseMessage GetContactById(int contactId)
        {
            var contact = _contactService.GetContactById(contactId);
            if (contact != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, contact);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Contact not found");
        }


        [HttpPost]
        public HttpResponseMessage Post([FromBody] ContactEntity contactEntity)
        {
            int result = _contactService.CreateContact(contactEntity);
            if (result > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Contact not created");
        }

        [HttpPut]
        public HttpResponseMessage Put([FromBody]ContactEntity contactEntity)
        {
            if (contactEntity.ContactId > 0)
            {
                bool result = _contactService.UpdateContact(contactEntity);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Failed to update contact");
        }

        [HttpDelete]
        public HttpResponseMessage Delete(int contactId)
        {
            if (contactId > 0)
            {
                bool result = _contactService.DeleteContact(contactId);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Failed to delete contact");
        }
    }
    #endregion
}
