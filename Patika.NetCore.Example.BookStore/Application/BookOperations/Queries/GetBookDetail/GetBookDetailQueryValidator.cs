﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Patika.NetCore.Example.BookStore.BookOperations.GetBookDetail
{
    public class GetBookDetailQueryValidator : AbstractValidator<GetBookDetailQuery>
    {
        public GetBookDetailQueryValidator()
        {
            RuleFor(query => query.BookId).GreaterThan(0);
        }
    }
}
