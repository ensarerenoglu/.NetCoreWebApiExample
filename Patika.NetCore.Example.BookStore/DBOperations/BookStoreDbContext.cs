using Microsoft.EntityFrameworkCore;
using Patika.NetCore.Example.BookStore.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Patika.NetCore.Example.BookStore.DBOperations
{
    public class BookStoreDbContext:DbContext
    {
        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options): base(options)
        {
            
        }

        public DbSet<Book> Books { get; set; }

    }
}
