using AutoMapper;
using Patika.NetCore.Example.BookStore.DBOperations;
using System;
using System.Linq;


namespace Patika.NetCore.Example.BookStore.Application.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreDetailQuery
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public int GenreId { get; set; }
        public GetGenreDetailQuery(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public GenreDetailViewModel Handle()
        {
            var genre = _dbContext.Genres.SingleOrDefault(x => x.IsActive && x.Id == GenreId);
            if (genre == null)
            {
                throw new InvalidOperationException("Aradığını kategori bulunamadı");
            }
            GenreDetailViewModel vm = _mapper.Map<GenreDetailViewModel>(genre);
            return vm;
        }

        public class GenreDetailViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}
