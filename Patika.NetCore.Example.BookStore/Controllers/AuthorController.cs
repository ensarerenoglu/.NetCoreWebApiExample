using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Patika.NetCore.Example.BookStore.Application.AuthorOperations.Commands.CreateAuthor;
using Patika.NetCore.Example.BookStore.Application.AuthorOperations.Commands.DeleteAuthor;
using Patika.NetCore.Example.BookStore.Application.AuthorOperations.Commands.UpdateAuthor;
using Patika.NetCore.Example.BookStore.Application.AuthorOperations.Queries.GetAuthorDetail;
using Patika.NetCore.Example.BookStore.Application.AuthorOperations.Queries.GetAuthors;
using Patika.NetCore.Example.BookStore.DBOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Patika.NetCore.Example.BookStore.Application.AuthorOperations.Commands.CreateAuthor.CreateAuthorCommand;
using static Patika.NetCore.Example.BookStore.Application.AuthorOperations.Commands.UpdateAuthor.UpdateAuthorCommand;
using static Patika.NetCore.Example.BookStore.Application.AuthorOperations.Queries.GetAuthorDetail.GetAuthorDetailQuery;

namespace Patika.NetCore.Example.BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public AuthorController(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAuthors()
        {
            GetAuthorsQuery query = new GetAuthorsQuery(_dbContext, _mapper);
            var result = query.Handle();
            return Ok(result);
        }
        [HttpGet("id")]
        public IActionResult GetbyId(int AuthorId)
        {
            GetAuthorDetailVM getAuthorDetailVM = new GetAuthorDetailVM();
            try
            {
                GetAuthorDetailQuery query = new GetAuthorDetailQuery(_dbContext, _mapper);
                query.AuthorId = AuthorId;
                GetAuthorDetailQueryValidator validator = new GetAuthorDetailQueryValidator();
                validator.ValidateAndThrow(query);

                getAuthorDetailVM = query.Handle();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            return Ok(getAuthorDetailVM);
        }
        [HttpPost]
        public IActionResult AddAuthor([FromBody] CreateAuthorCommandVM newModel)
        {
            CreateAuthorCommand command = new CreateAuthorCommand(_dbContext, _mapper);
            try
            {

                command.Model = newModel;
                CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
                validator.ValidateAndThrow(command);
                command.Handle();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Created($"{command.Model.FirstName} {command.Model.LastName} oluşturuldu!", null);
        }
        [HttpPut("{authorId}")]
        public IActionResult UpdateAuthor(int authorId, [FromBody] UpdateAuthorCommandVM newModel)
        {
            try
            {
                UpdateAuthorCommand command = new UpdateAuthorCommand(_dbContext);
                command.AuthorId = authorId;
                command.Model = newModel;
                UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
                validator.ValidateAndThrow(command);
                command.Handle();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            return Ok();
        }
        [HttpDelete("{AuthorId}")]
        public IActionResult DeleteAuthor(int AuthorId)
        {
            try
            {
                DeleteAuthorCommand command = new DeleteAuthorCommand(_dbContext);
                command.AuthorId = AuthorId;
                DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator();
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
