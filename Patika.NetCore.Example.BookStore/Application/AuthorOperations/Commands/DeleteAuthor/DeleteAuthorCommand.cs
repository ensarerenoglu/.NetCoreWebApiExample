using Patika.NetCore.Example.BookStore.DBOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Patika.NetCore.Example.BookStore.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommand
    {
        private readonly BookStoreDbContext _dbContext;
        public int AuthorId { get; set; }
        public DeleteAuthorCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Handle()
        {
            if (AuthorId <= 0)
            {
                throw new InvalidOperationException("Aradığınız yazar bulunamadı!");
            }
            var author = _dbContext.Authors.Find(AuthorId);
            if (author == null)
            {
                throw new InvalidOperationException("Aradığınız yazar bulunamadı!");
            }
            var books = _dbContext.Books.Where(x => x.AuthorId == author.Id);
            if (books != null)
            {
                throw new InvalidOperationException("Silinmek istenen yazara ait kitaplar bulunmaktadır!");
            }
            _dbContext.Remove(author);
            _dbContext.SaveChanges();
        }
    }
}
