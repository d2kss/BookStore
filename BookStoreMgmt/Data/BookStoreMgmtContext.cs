using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BookStoreMgmt.Models;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
namespace BookStoreMgmt.Data
{
    public class BookStoreMgmtContext : IdentityDbContext
    {
        public BookStoreMgmtContext (DbContextOptions<BookStoreMgmtContext> options)
            : base(options)
        {
        }

        public DbSet<BookStoreMgmt.Models.Books> Books { get; set; } = default!;
    }
}
