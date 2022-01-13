using Patika.NetCore.Example.BookStore.DBOperations;
using System;

using System.Linq;


namespace Patika.NetCore.Example.BookStore.BookOperations.DeleteBook
{
    public class DeleteBookCommand
    {
        private readonly BookStoreDbContext _dbContext;
        public int BookId { get; set; }
        public DeleteBookCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public void Handle()
        {

            if (BookId <= 0)
            {
                throw new InvalidOperationException("Silinecek kitap bulunmadı");
            }
            var book = _dbContext.Books.SingleOrDefault(x => x.ID == BookId);
            if (book == null)
            {
                throw new InvalidOperationException("Silinecek kitap bulunmaadı");
            }
            _dbContext.Books.Remove(book);
            _dbContext.SaveChanges();
        }
    }
}
