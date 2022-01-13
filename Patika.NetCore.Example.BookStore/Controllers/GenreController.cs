
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Patika.NetCore.Example.BookStore.Application.GenreOperations.Commands.CreateGenre;
using Patika.NetCore.Example.BookStore.Application.GenreOperations.Commands.DeleteGenre;
using Patika.NetCore.Example.BookStore.Application.GenreOperations.Commands.UpdateGenre;
using Patika.NetCore.Example.BookStore.Application.GenreOperations.Queries.GetGenreDetail;
using Patika.NetCore.Example.BookStore.Application.GenreOperations.Queries.GetGenres;
using Patika.NetCore.Example.BookStore.BookOperations.CreateBook;
using Patika.NetCore.Example.BookStore.BookOperations.GetBookDetail;
using Patika.NetCore.Example.BookStore.DBOperations;
using System;
using static Patika.NetCore.Example.BookStore.Application.GenreOperations.Commands.CreateGenre.CreateGenreCommand;
using static Patika.NetCore.Example.BookStore.Application.GenreOperations.Commands.UpdateGenre.UpdateGenreCommand;
using static Patika.NetCore.Example.BookStore.Application.GenreOperations.Queries.GetGenreDetail.GetGenreDetailQuery;

namespace Patika.NetCore.Example.BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public GenreController(BookStoreDbContext dbContext,IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetGenres()
        {
            GetGenresQuery query = new GetGenresQuery(_dbContext,_mapper);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetbyId(int id)
        {
            GenreDetailViewModel genreDetailViewModel;

            try
            {
                GetGenreDetailQuery query = new GetGenreDetailQuery(_dbContext, _mapper);
                query.GenreId = id;
                GetGenreDetailValidator validator = new GetGenreDetailValidator();
                validator.ValidateAndThrow(query);

                genreDetailViewModel = query.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(genreDetailViewModel);
        }

        [HttpPost]
        public IActionResult AddGenre([FromBody] CreateGenreModel newGenre)
        {
            CreateGenreCommand command = new CreateGenreCommand(_dbContext,_mapper);
            try
            {
                command.Model = newGenre;
                CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
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
        public IActionResult UpdateGenre(int id, [FromBody] UpdateGenreViewModel updatedGenre)
        {
            try
            {
                UpdateGenreCommand command = new UpdateGenreCommand(_dbContext);
                command.GenreId = id;
                command.Model = updatedGenre;
                UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
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
        public IActionResult DeleteGenre(int id)
        {
            try
            {
                DeleteGenreCommand command = new DeleteGenreCommand(_dbContext);
                command.GenreId = id;
                DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
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
