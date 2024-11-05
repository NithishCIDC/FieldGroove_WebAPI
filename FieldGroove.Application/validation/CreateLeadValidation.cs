using FieldGroove.Domain.Models;
using FluentValidation;

namespace FieldGroove.Application.validation
{
    public class CreateLeadValidation : AbstractValidator<LeadsModel>
    {
        public CreateLeadValidation()
        {
            RuleFor(x => x.ProjectName)
               .NotEmpty().WithMessage("Project name is required.");

            RuleFor(x => x.Contact)
                .NotEmpty().WithMessage("Phone Number is required")
                .LessThanOrEqualTo(9999999999).WithMessage("Contact Should be in 10 digit")
                .GreaterThanOrEqualTo(1000000000).WithMessage("Contact Should be in 10 digit");
        }
    }
}
