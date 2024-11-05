using System.ComponentModel.DataAnnotations;

namespace FieldGroove.Api.Models
{
	public class LoginModel
	{
		[Required]
		public string? Email { get; set; }

		[Required]
		public string? Password { get; set; }

		[Required]
		[Display(Name = "Remember Me")]
		public bool RemenberMe { get; set; }
	}
}
