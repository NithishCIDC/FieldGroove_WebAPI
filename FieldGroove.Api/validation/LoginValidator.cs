
using FieldGroove.Domain.Models;
using FluentValidation;

namespace FieldGroove.Api.Validation
{
	public class LoginValidator : AbstractValidator<LoginModel>
	{
		public LoginValidator()
		{
			RuleFor(x => x.Email)
				.NotEmpty().WithMessage("Email is Required")
				.EmailAddress().WithMessage("Invalid Email Address");
			RuleFor(x => x.Password)
				.NotEmpty().WithMessage("Password is Required");
		}
	}
}
