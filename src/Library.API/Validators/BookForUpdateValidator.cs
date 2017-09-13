using FluentValidation;
using Library.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.Validators
{
    public class BookForUpdateValidator : AbstractValidator<BookForUpdateDto>
    {
        public BookForUpdateValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("You should fill out a Title.");
            RuleFor(x => x.Title).MaximumLength(100).WithMessage("The Title shouldn't have more than 100 characters");
            RuleFor(x => x.Description).NotEmpty().WithMessage("You should fill out a Description.");
            RuleFor(x => x.Description).MaximumLength(100).WithMessage("The Description shouldn't have more than 400 characters");
            RuleFor(x => x.Description).NotEqual(x => x.Title).WithMessage("The provided Description should be different than the Title");
        }
    }
}
