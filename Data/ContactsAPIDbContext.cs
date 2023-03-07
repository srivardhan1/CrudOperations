using CrudOperations.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudOperations.Data
{
    public class ContactsAPIDbContext : DbContext //ContactsAPIDbContext will inherit from DbContext
    {
        public ContactsAPIDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Contact> Contacts { get; set; } //create properties which will act as tables for entityframework core
    }
}
