using FluentValidation;
using Ecomm.Application.Dtos.Order.Request;

namespace Ecomm.Application.Validators.Order
{
    public class OrderValidator : AbstractValidator<OrderRequestDto>
    {
        public OrderValidator() 
        { 
            RuleFor(c => c.ClientId)
                .NotNull().WithMessage("El campo Nombre no puede ser nulo")
                .NotEmpty().WithMessage("El campo Nombre no puede ser nulo");

        }

    }
}
