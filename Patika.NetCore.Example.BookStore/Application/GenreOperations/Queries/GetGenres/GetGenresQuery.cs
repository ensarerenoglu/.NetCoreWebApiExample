﻿using AutoMapper;
using Patika.NetCore.Example.BookStore.DBOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Patika.NetCore.Example.BookStore.Application.GenreOperations.Queries.GetGenres
{
    public class GetGenresQuery
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetGenresQuery(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper; 
        }
        public List<GenresViewModel> Handle()
        {
            var genres = _dbContext.Genres.Where(genre => genre.IsActive).OrderBy(x => x.Id);
            List<GenresViewModel> returnObj = _mapper.Map<List<GenresViewModel>>(genres);
            return returnObj;
        }

        public class GenresViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}
