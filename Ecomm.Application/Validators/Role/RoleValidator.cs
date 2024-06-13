using FluentValidation;
using Ecomm.Application.Dtos.Role.Request;

namespace Ecomm.Application.Validators.Role
{
    public class RoleValidator : AbstractValidator<RoleRequestDto>
    {
        public RoleValidator() 
        { 
            RuleFor(c => c.Name)
                .NotNull().WithMessage("El campo Nombre no puede ser nulo")
                .NotEmpty().WithMessage("El campo Nombre no puede ser nulo");

        }

    }
}
