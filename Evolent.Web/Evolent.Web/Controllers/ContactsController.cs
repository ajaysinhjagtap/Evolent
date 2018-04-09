using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Evolent.Web.Models;
using Evolent.Web.Services;
using Evolent.Entities;

namespace Evolent.Web.Controllers
{
    public class ContactsController : Controller
    {
        #region variables

        IContactService _contactService;

        #endregion


        public ContactsController()
        {
            _contactService = new ContactService();
        }


        // GET: Contacts
        public async Task<ActionResult> Index()
        {
            List<ContactViewModel> contactsList = new List<ContactViewModel>();
            var list = await _contactService.GetAllContacts();
            list.ToList().ForEach(contactEntity =>
            {
                contactsList.Add(
                    new ContactViewModel
                    {
                        ContactId = contactEntity.ContactId,
                        FirstName = contactEntity.FirstName,
                        LastName = contactEntity.LastName,
                        Email = contactEntity.Email,
                        PhoneNumber = contactEntity.PhoneNumber,
                        Status = contactEntity.IsActive
                    }
                    );
            });
            return View(contactsList);
        }

        // GET: Contacts/Details/5
        public async Task<ActionResult> Details(int? contactId)
        {
            if (contactId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactEntity contactEntity = await _contactService.GetContactById(contactId.Value);
            if (contactEntity == null)
            {
                return HttpNotFound();
            }
            ContactViewModel contactViewModel = new ContactViewModel
            {
                ContactId = contactEntity.ContactId,
                FirstName = contactEntity.FirstName,
                LastName = contactEntity.LastName,
                Email = contactEntity.Email,
                PhoneNumber = contactEntity.PhoneNumber,
                Status = contactEntity.IsActive
            };
            return View(contactViewModel);
        }

        // GET: Contacts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Contacts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ContactId,FirstName,LastName,Email,PhoneNumber,Status")] ContactViewModel contactViewModel)
        {
            if (ModelState.IsValid)
            {
                ContactEntity contactEntity = new ContactEntity
                {
                    ContactId = contactViewModel.ContactId,
                    FirstName = contactViewModel.FirstName,
                    LastName = contactViewModel.LastName,
                    Email = contactViewModel.Email,
                    PhoneNumber = contactViewModel.PhoneNumber,
                    IsActive = contactViewModel.Status
                };
                await _contactService.CreateContact(contactEntity);
                return RedirectToAction("Index");
            }
            return View(contactViewModel);
        }

        // GET: Contacts/Edit/5
        public async Task<ActionResult> Edit(int? contactId)
        {
            if (contactId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactEntity contactEntity = await _contactService.GetContactById(contactId.Value);
            if (contactEntity == null)
            {
                return HttpNotFound();
            }
            ContactViewModel contactViewModel = new ContactViewModel
            {
                ContactId = contactEntity.ContactId,
                FirstName = contactEntity.FirstName,
                LastName = contactEntity.LastName,
                Email = contactEntity.Email,
                PhoneNumber = contactEntity.PhoneNumber,
                Status = contactEntity.IsActive
            };
            return View(contactViewModel);
        }

        // POST: Contacts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ContactId,FirstName,LastName,Email,PhoneNumber,Status")] ContactViewModel contactViewModel)
        {
            if (ModelState.IsValid)
            {
                ContactEntity contactEntity = new ContactEntity
                {

                    ContactId = contactViewModel.ContactId,
                    FirstName = contactViewModel.FirstName,
                    LastName = contactViewModel.LastName,
                    Email = contactViewModel.Email,
                    PhoneNumber = contactViewModel.PhoneNumber,
                    IsActive = contactViewModel.Status
                };
                await _contactService.UpdateContact(contactEntity);
                return RedirectToAction("Index");
            }
            return View(contactViewModel);
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int contactId)
        {
            ContactEntity contactEntity = await _contactService.GetContactById(contactId);
            if (contactEntity == null)
            {
                return HttpNotFound();
            }
            ContactViewModel contactViewModel = new ContactViewModel
            {
                ContactId = contactEntity.ContactId,
                FirstName = contactEntity.FirstName,
                LastName = contactEntity.LastName,
                Email = contactEntity.Email,
                PhoneNumber = contactEntity.PhoneNumber,
                Status = contactEntity.IsActive
            };
            return View(contactViewModel);
        }

        [HttpPost]    
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]    
        public async Task<ActionResult> DeleteContact(int contactId)
        {
            await _contactService.DeleteContact(contactId);
            return RedirectToAction("Index");
        }
    }
}
