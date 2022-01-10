using Patika.NetCore.Example.BookStore.DBOperations;
using Patika.NetCore.Example.BookStore.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Patika.NetCore.Example.BookStore.BookOperations.CreateBook
{
    public class CreateBookCommand
    {
        public CreateBookModel Model { get; set; }
        private readonly BookStoreDbContext _dbContext;
        public CreateBookCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(x => x.Title == Model.Title);
            if (book != null)
            {
                throw new InvalidOperationException("Kitap zaten mevcut");
            }
            book = new Book();
            book.Title = Model.Title;
            book.PublishDate = Model.PublishDate;
            book.GenreID = Model.GenreID;
            book.PageCount = Model.PageCount;
            _dbContext.Add(book);
            _dbContext.SaveChanges();


        }
        public class CreateBookModel
        {
            public string Title { get; set; }
            public int GenreID { get; set; }
            public int PageCount { get; set; }
            public DateTime PublishDate { get; set; }
        }
    }
}
