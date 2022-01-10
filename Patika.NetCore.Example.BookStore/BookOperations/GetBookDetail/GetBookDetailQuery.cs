using Patika.NetCore.Example.BookStore.Common;
using Patika.NetCore.Example.BookStore.DBOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Patika.NetCore.Example.BookStore.BookOperations.GetBookDetail
{
    public class GetBookDetailQuery
    {

        private readonly BookStoreDbContext _dbContext;
        public int BookId { get; set; }
        public GetBookDetailQuery(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public BookDetailViewModel Handle()
        {
            var book = _dbContext.Books.Where(x => x.ID == BookId).FirstOrDefault();
            if (book == null)
            {
                throw new InvalidOperationException("Aradığınız kitap bulunamadı");
            }
            BookDetailViewModel vm = new BookDetailViewModel();
            vm.Title = book.Title;
            vm.PageCount = book.PageCount;
            vm.Genre = ((GenreEnum)book.GenreID).ToString();
            vm.PublishDate = book.PublishDate.Date.ToString("dd/MM/yyyy");
            return vm;
        }

        public class BookDetailViewModel
        {
            public string Title { get; set; }
            public string Genre { get; set; }
            public int PageCount { get; set; }
            public string PublishDate { get; set; }
        }
    }
}
