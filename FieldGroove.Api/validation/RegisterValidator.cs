using FieldGroove.Domain.Models;
using FluentValidation;

namespace FieldGroove.Api.Validation
{
	public class RegisterValidator : AbstractValidator<RegisterModel>
	{
		public RegisterValidator()
		{
			RuleFor(x => x.FirstName)
			   .NotEmpty().WithMessage("First Name is required");

			RuleFor(x => x.LastName)
				.NotEmpty().WithMessage("Last Name is required");

			RuleFor(x => x.CompanyName)
				.NotEmpty().WithMessage("Company Name is required");

			RuleFor(x => x.Phone)
				.NotEmpty().WithMessage("Phone Number is required")
				.LessThanOrEqualTo(9999999999).WithMessage("Phone Number Should be in 10 digit")
				.GreaterThanOrEqualTo(1000000000).WithMessage("Phone Number Should be in 10 digit");

			RuleFor(x => x.Email)
					.NotEmpty().WithMessage("Email is required").EmailAddress().WithMessage("Invalid Email Address");

			RuleFor(x => x.Password)
					.NotEmpty().WithMessage("password is Required")
					.MinimumLength(8);

			RuleFor(x => x.PasswordAgain)
					.NotEmpty().WithMessage("Confirm Password is Required").Equal(x => x.Password).WithMessage("Password does not Match");

			RuleFor(x => x.TimeZone)
					.NotEmpty().WithMessage("Select the Time Zone");

			RuleFor(x => x.StreetAddress1)
					.NotEmpty().WithMessage("Street Address 1 should not be Empty");

			RuleFor(x => x.StreetAddress2)
					.NotEmpty().WithMessage("Street Address 2 should not be Empty");

			RuleFor(x => x.City)
					.NotEmpty().WithMessage("City should not be empty");

			RuleFor(x => x.State)
					.NotEmpty().WithMessage("State should not be empty");

			RuleFor(x => x.Zip)
					.NotEmpty().WithMessage("Zip should not be empty");
		}

	}
}
