using AutoMapper;
using Patika.NetCore.Example.BookStore.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Patika.NetCore.Example.BookStore.Application.AuthorOperations.Commands.CreateAuthor.CreateAuthorCommand;
using static Patika.NetCore.Example.BookStore.Application.AuthorOperations.Commands.UpdateAuthor.UpdateAuthorCommand;
using static Patika.NetCore.Example.BookStore.Application.AuthorOperations.Queries.GetAuthorDetail.GetAuthorDetailQuery;
using static Patika.NetCore.Example.BookStore.Application.AuthorOperations.Queries.GetAuthors.GetAuthorsQuery;
using static Patika.NetCore.Example.BookStore.Application.GenreOperations.Commands.CreateGenre.CreateGenreCommand;
using static Patika.NetCore.Example.BookStore.Application.GenreOperations.Commands.UpdateGenre.UpdateGenreCommand;
using static Patika.NetCore.Example.BookStore.Application.GenreOperations.Queries.GetGenreDetail.GetGenreDetailQuery;
using static Patika.NetCore.Example.BookStore.Application.GenreOperations.Queries.GetGenres.GetGenresQuery;
using static Patika.NetCore.Example.BookStore.BookOperations.CreateBook.CreateBookCommand;
using static Patika.NetCore.Example.BookStore.BookOperations.GetBookDetail.GetBookDetailQuery;
using static Patika.NetCore.Example.BookStore.BookOperations.GetBooks.GetBooksQuery;

namespace Patika.NetCore.Example.BookStore.Common.MappingProfile
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            //Book
            CreateMap<CreateBookModel, Book>();
            CreateMap<Book, BookDetailViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name));
            CreateMap<Book, BooksViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src =>src.Genre.Name));
            //Genre
            CreateMap<Genre, GenresViewModel>();
            CreateMap<Genre, GenreDetailViewModel>();
            CreateMap<CreateGenreModel, Genre>();
            //Author
            CreateMap<Author, GetAuthorsVM>().ForPath(dest => dest.BookNames, opt => opt.MapFrom(src => src.Books.Where(x => x.AuthorId == src.Id).Select(x=> x.Title).ToArray()));
            CreateMap<Author, GetAuthorDetailVM>().ForPath(dest => dest.BookNames, opt => opt.MapFrom(src => src.Books.Where(x => x.Author.Id == src.Id).Select(x => x.Title).ToArray()));
            CreateMap<CreateAuthorCommandVM, Author>();
            
            
        }
    }
}
