using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Patika.NetCore.Example.BookStore.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandValidator:AbstractValidator<CreateAuthorCommand>
    {
        public CreateAuthorCommandValidator()
        {
            RuleFor(command => command.Model.BirthDate).NotEmpty().LessThan(DateTime.Now.Date);
            RuleFor(command => command.Model.FirstName).NotEmpty().Length(1, 50);
            RuleFor(command => command.Model.LastName).NotEmpty().Length(1, 50);
        }
    }
}
