using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Patika.NetCore.Example.BookStore.DBOperations;
using Patika.NetCore.Example.BookStore.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Patika.NetCore.Example.BookStore.Application.AuthorOperations.Queries.GetAuthorDetail
{
    public class GetAuthorDetailQuery
    {
        public int AuthorId { get; set; }
        BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetAuthorDetailQuery(BookStoreDbContext dbContext,IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public GetAuthorDetailVM Handle()
        {
            var author = _dbContext.Authors.Include(x => x.Books).Where(x => x.Id == AuthorId).FirstOrDefault();
            if (author == null)
            {
                throw new InvalidOperationException("Aradığınız yazar bulunamadı!");
            }
            GetAuthorDetailVM vM = _mapper.Map<GetAuthorDetailVM>(author);
            return vM;

        }
        public class GetAuthorDetailVM
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string[] BookNames { get; set; }
            public DateTime BirthDate { get; set; }
        }
    }
}
