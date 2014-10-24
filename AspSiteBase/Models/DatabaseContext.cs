using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AspSiteBase.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext()
            : base("name=WebSolConnection")
        {

        }

        public DbSet<Contactus> Contacts { get; set; }
    }
}