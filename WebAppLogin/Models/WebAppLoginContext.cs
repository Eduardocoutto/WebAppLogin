using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebAppLogin.Models
{
    public class WebAppLoginContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
    }
}