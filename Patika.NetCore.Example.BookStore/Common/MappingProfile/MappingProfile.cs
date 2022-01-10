using AutoMapper;
using Patika.NetCore.Example.BookStore.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Patika.NetCore.Example.BookStore.BookOperations.CreateBook.CreateBookCommand;
using static Patika.NetCore.Example.BookStore.BookOperations.GetBookDetail.GetBookDetailQuery;
using static Patika.NetCore.Example.BookStore.BookOperations.GetBooks.GetBooksQuery;

namespace Patika.NetCore.Example.BookStore.Common.MappingProfile
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBookModel, Book>();
            CreateMap<Book, BookDetailViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => ((GenreEnum)src.GenreID).ToString()));
            CreateMap<Book, BooksViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => ((GenreEnum)src.GenreID).ToString()));
        }
    }
}
