using Patika.NetCore.Example.BookStore.DBOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Patika.NetCore.Example.BookStore.BookOperations.UpdateBook
{
    
    public class UpdateBookCommand
    {
        private readonly BookStoreDbContext _dbContext;
        public UpdateBookModel Model { get; set; }
        public int BookId { get; set; }
        public UpdateBookCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Handle()
        {
            if (BookId == 0 || Model == null)
            {
                throw new InvalidOperationException("Aradığınız kitap bulunamadı");
            }
            var book = _dbContext.Books.SingleOrDefault(x => x.ID == BookId);

            if (book is null)
            {
                throw new InvalidOperationException("Aradığınız kitap bulunamadı");
            }

            book.GenreID = Model.GenreId != default ? Model.GenreId : book.GenreID;
           
            book.Title = Model.Title != default ? Model.Title : book.Title;
            _dbContext.SaveChanges();
           
        }
        public class UpdateBookModel
        {
            public string Title { get; set; }
            public int GenreId { get; set; }
        }
    }
    
}
