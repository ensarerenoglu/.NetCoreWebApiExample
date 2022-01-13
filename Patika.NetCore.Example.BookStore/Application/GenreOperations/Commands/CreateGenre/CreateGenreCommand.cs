using AutoMapper;
using Patika.NetCore.Example.BookStore.DBOperations;
using Patika.NetCore.Example.BookStore.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Patika.NetCore.Example.BookStore.Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommand
    {
        public CreateGenreModel Model { get; set; }
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateGenreCommand(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public void Handle()
        {
            var genre = _dbContext.Genres.SingleOrDefault(x => x.Name == Model.Name);
            if (genre != null)
            {
                throw new InvalidOperationException("Kayıtlarımızda bu kategori bulunuyor!");
            }
            genre = _mapper.Map<Genre>(Model);
            _dbContext.Add(genre);
            _dbContext.SaveChanges();
        }

        public class CreateGenreModel
        {
            public string Name { get; set; }
        }
    }
}
