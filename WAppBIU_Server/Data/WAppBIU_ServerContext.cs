using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WAppBIU_Server.Models;
using Domain;

namespace WAppBIU_Server.Data
{
    public class WAppBIU_ServerContext : DbContext
    {
        public WAppBIU_ServerContext (DbContextOptions<WAppBIU_ServerContext> options)
            : base(options)
        {
        }

        public DbSet<WAppBIU_Server.Models.Rank> Rank { get; set; }

        public DbSet<Domain.User> User { get; set; }
    }
}
