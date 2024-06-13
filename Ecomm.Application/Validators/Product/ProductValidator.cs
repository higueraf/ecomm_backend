using FluentValidation;
using Ecomm.Application.Dtos.Product.Request;

namespace Ecomm.Application.Validators.Product
{
    public class ProductValidator : AbstractValidator<ProductRequestDto>
    {
        public ProductValidator() 
        { 
            RuleFor(c => c.Name)
                .NotNull().WithMessage("El campo Nombre no puede ser nulo")
                .NotEmpty().WithMessage("El campo Nombre no puede ser nulo");

        }

    }
}
