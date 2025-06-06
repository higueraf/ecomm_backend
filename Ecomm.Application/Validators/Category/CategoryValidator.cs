﻿using FluentValidation;
using Ecomm.Application.Dtos.Category.Request;

namespace Ecomm.Application.Validators.Category
{
    public class CategoryValidator : AbstractValidator<CategoryRequestDto>
    {
        public CategoryValidator() 
        { 
            RuleFor(c => c.Name)
                .NotNull().WithMessage("El campo Nombre no puede ser nulo")
                .NotEmpty().WithMessage("El campo Nombre no puede ser nulo");

        }

    }
}
