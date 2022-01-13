using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Patika.NetCore.Example.BookStore.BookOperations.CreateBook
{
    // Nuget'tan Fluent Validation paketi indirildikten sonra validasyonların düzenleneceği class'a "AbsractorValidator<T>" miras olarak alınır. 
    // Validasyon controller'da kullanılmalıdır. 
    public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
    {
        //Validator tanımları ctor içerisinde yapılmalıdır. 
        public CreateBookCommandValidator()
        {
            RuleFor(command => command.Model.GenreID).GreaterThan(0);
            RuleFor(command => command.Model.PageCount).GreaterThan(0);
            RuleFor(command => command.Model.PublishDate.Date).NotEmpty().LessThan(DateTime.Now.Date);
            RuleFor(command => command.Model.Title).NotEmpty().MinimumLength(4);
        }
    }
}
