using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Patika.NetCore.Example.BookStore.DBOperations;
using Patika.NetCore.Example.BookStore.Entity;
using System.Collections.Generic;
using System.Linq;

namespace Patika.NetCore.Example.BookStore.Application.AuthorOperations.Queries.GetAuthors
{
    public class GetAuthorsQuery
    {
        BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetAuthorsQuery(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public List<GetAuthorsVM> Handle()
        {
            var authorlist = _dbContext.Authors.Include(x => x.Books).OrderBy(x => x.LastName);
            List<GetAuthorsVM> getAuthorsVMs = _mapper.Map<List<GetAuthorsVM>>(authorlist);
            return getAuthorsVMs;
        }
        public class GetAuthorsVM
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string[] BookNames{ get; set; }
        }
    }
}
