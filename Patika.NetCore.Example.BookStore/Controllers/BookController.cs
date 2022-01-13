using AutoMapper;
using FluentValidation;

using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Patika.NetCore.Example.BookStore.BookOperations.CreateBook;
using Patika.NetCore.Example.BookStore.BookOperations.DeleteBook;
using Patika.NetCore.Example.BookStore.BookOperations.GetBookDetail;
using Patika.NetCore.Example.BookStore.BookOperations.GetBooks;
using Patika.NetCore.Example.BookStore.BookOperations.UpdateBook;
using Patika.NetCore.Example.BookStore.DBOperations;
using System;
using static Patika.NetCore.Example.BookStore.BookOperations.CreateBook.CreateBookCommand;
using static Patika.NetCore.Example.BookStore.BookOperations.GetBookDetail.GetBookDetailQuery;
using static Patika.NetCore.Example.BookStore.BookOperations.UpdateBook.UpdateBookCommand;

namespace Patika.NetCore.Example.BookStore.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public BookController(BookStoreDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context,_mapper);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetbyId(int id)
        {
            BookDetailViewModel bookDetailViewModel;

            try
            {
                GetBookDetailQuery query = new GetBookDetailQuery(_context, _mapper);
                query.BookId = id;
                GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
                validator.ValidateAndThrow(query);

                bookDetailViewModel = query.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(bookDetailViewModel);
        }

        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            CreateBookCommand command = new CreateBookCommand(_context,_mapper);
            try
            {
                command.Model = newBook;
                CreateBookCommandValidator validator = new CreateBookCommandValidator();
                validator.ValidateAndThrow(command);
                command.Handle();
            }
            catch (Exception ex)
            {
                var result = JsonConvert.SerializeObject(new { error = ex.Message });
                return BadRequest(result);
            }
            return Ok();
        }
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id,[FromBody] UpdateBookModel updatedBook)
        {
            try
            {
                UpdateBookCommand command = new UpdateBookCommand(_context);
                command.BookId = id;
                command.Model = updatedBook;
                UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
                validator.ValidateAndThrow(command);
                command.Handle();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();

        }
        
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            try
            {
                DeleteBookCommand command = new DeleteBookCommand(_context);
                command.BookId = id;
                DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
                validator.ValidateAndThrow(command);
                command.Handle();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            return Ok();
        }
    }
}
