using Patika.NetCore.Example.BookStore.Common;
using Patika.NetCore.Example.BookStore.DBOperations;
using Patika.NetCore.Example.BookStore.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Patika.NetCore.Example.BookStore.BookOperations.GetBooks
{
    public class GetBooksQuery
    {
        private readonly BookStoreDbContext _dbContext;
        public GetBooksQuery(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<BooksViewModel> Handle()
        {
            var bookList = _dbContext.Books.OrderBy(x => x.ID).ToList();
            List<BooksViewModel> vm = new List<BooksViewModel>();
            foreach (var book in bookList)
            {
                vm.Add(new BooksViewModel
                {
                    Title = book.Title,
                    PageCount = book.PageCount,
                    Genre = ((GenreEnum)book.GenreID).ToString(),
                    PublishDate = book.PublishDate.Date.ToString("dd/MM/yyyy")
                    
                });
                
            }
            
            return vm;
        }

        public class BooksViewModel
        {
            public string Title { get; set; }
            public int PageCount { get; set; }
            public string PublishDate { get; set; }
            public string Genre { get; set; }
        }
    }
}
