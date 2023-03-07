using CrudOperations.Data;
using CrudOperations.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudOperations.Controllers
{
    [ApiController]
    [Route("api/[controller]")]//same as [Route("api/contacts")]
    public class ContactsController : Controller
    {
        private readonly ContactsAPIDbContext dbContext;

        public ContactsController(ContactsAPIDbContext dbContext)//injecting ContactsAPIDbContext to talk to our database
        {
            this.dbContext = dbContext;
        }
        [HttpGet]//to use swagger ui we are indicating this
        public async Task<IActionResult> GetContacts()
        {
           return Ok(await dbContext.Contacts.ToListAsync());
           
        }
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult>GetContact([FromRoute] Guid id)
        {
            var contact = await dbContext.Contacts.FindAsync(id);//This line talks to contacts table in database
            if(contact == null)
            {
                return NotFound();
            }
            return Ok(contact);
        }
        [HttpPost]
        public async Task<IActionResult> AddContact(AddContactRequests addContactRequests) //here we need to get some values from user, so we have created AddContactRequests class in Models
        {
            var contact = new Contact()
            {
                Id = Guid.NewGuid(),
                Address = addContactRequests.Address,
                Email = addContactRequests.Email,
                FullName = addContactRequests.FullName,
                Phone = addContactRequests.Phone

            };
            await dbContext.Contacts.AddAsync(contact);
            await dbContext.SaveChangesAsync();// with entity framework core we also need to save the changes to db before we see the changes
            return Ok(contact);
        }
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateContact([FromRoute] Guid id,UpdateContactsRequest updateContactsRequest)//to know updated fields, we hace created UpdateContactsRequest
        {
            var contact = await dbContext.Contacts.FindAsync(id);
            if(contact != null)
            {
                contact.FullName = updateContactsRequest.FullName;
                contact.Address = updateContactsRequest.Address;
                contact.Phone = updateContactsRequest.Phone;
                contact.Email = updateContactsRequest.Email;

               await  dbContext.SaveChangesAsync();
                return Ok(contact);

            }
            return NotFound();
        }
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteContact([FromRoute] Guid id)
        {
            var contact = await dbContext.Contacts.FindAsync(id);
            if (contact != null)
            {
                dbContext.Remove(contact);
                await dbContext.SaveChangesAsync();
                return Ok(contact);

            }
            return NotFound();
        }
        }
    }
