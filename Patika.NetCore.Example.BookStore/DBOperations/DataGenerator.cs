using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Patika.NetCore.Example.BookStore.Entity;

namespace Patika.NetCore.Example.BookStore.DBOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using(var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                if (context.Books.Any())
                {
                    return;
                }
                context.Books.AddRange(
                        new Book {  Title = "Lean Startup", GenreID = 1, PageCount = 200, PublishDate = new DateTime(2001, 06, 12) },
                        new Book {  Title = "Mariland", GenreID = 2, PageCount = 300, PublishDate = new DateTime(2005, 07, 11) },
                        new Book {  Title = "Dune", GenreID = 3, PageCount = 400, PublishDate = new DateTime(2011, 02, 21) }
                    );
                context.SaveChanges();

            }
        }
    }
}
