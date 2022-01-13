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
                context.Genres.AddRange(
                        new Genre { Name = "Personel Growth" },
                        new Genre { Name = "Science Fiction" },
                        new Genre { Name = "Romance" }
                    );
                context.Authors.AddRange(
                        new Author {FirstName= "James", LastName="Joyce", BirthDate = new DateTime(1982,02,02)},
                        new Author {FirstName= "Miguel", LastName="de Cervantes", BirthDate = new DateTime(1547,09,29)},
                        new Author {FirstName= "Gabriel Garcia", LastName="Marquez", BirthDate = new DateTime(1927,03,06)}
                    );

                context.Books.AddRange(
                        new Book {  Title = "Ulysses", GenreID = 1, PageCount = 200, PublishDate = new DateTime(1904, 06, 12), AuthorId=1 },
                        new Book {  Title = "Don Quixote", GenreID = 2, PageCount = 300, PublishDate = new DateTime(1577, 07, 11),AuthorId=2 },
                        new Book {  Title = " One Hundred Years of Solitude", GenreID = 3, PageCount = 400, PublishDate = new DateTime(1947, 02, 21), AuthorId=3 }
                    );
                

                
                context.SaveChanges();

            }
        }
    }
}
