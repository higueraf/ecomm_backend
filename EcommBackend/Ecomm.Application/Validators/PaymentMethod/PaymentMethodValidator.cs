using FluentValidation;
using Ecomm.Application.Dtos.PaymentMethod.Request;

namespace Ecomm.Application.Validators.PaymentMethod
{
    public class PaymentMethodValidator : AbstractValidator<PaymentMethodRequestDto>
    {
        public PaymentMethodValidator() 
        { 
            RuleFor(c => c.Name)
                .NotNull().WithMessage("El campo Nombre no puede ser nulo")
                .NotEmpty().WithMessage("El campo Nombre no puede ser nulo");

        }

    }
}
