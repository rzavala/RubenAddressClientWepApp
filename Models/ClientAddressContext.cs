using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RubenAddressClientWepApp.Models
{
    public class ClientAddressContext : DbContext
    {
        public ClientAddressContext(DbContextOptions<ClientAddressContext> options)
            : base(options)
        {
        }

        public DbSet<ClientAddressItem> TodoItems { get; set; }

    }
}