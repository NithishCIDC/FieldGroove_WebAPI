using System.ComponentModel.DataAnnotations;

namespace FieldGroove.Api.Models
{
	public class RegisterModel
	{
		[Display(Name = "First Name*")]
		public required string FirstName { get; set; }

        [Display(Name = "Last Name*")]
		public required string LastName { get; set; }

        [Display(Name = "Company Name*")]
		public required string CompanyName { get; set; }

        [Display(Name = "Phone*")]
        public long Phone { get; set; }

        [Key]
        [Display(Name = "Email*")]
        public required string Email { get; set; }

        [Display(Name = "Password*")]
        public required string Password { get; set; }

        [Display(Name = "Password Again*")]
		public required string PasswordAgain { get; set; }

        [Display(Name = "Time Zone*")]
		public required string TimeZone { get; set; }

        [Display(Name = "Street Address 1*")]
		public required string StreetAddress1 { get; set; }

        [Display(Name = "Street Address 2*")]
		public required string StreetAddress2 { get; set; }

        [Display(Name = "City*")]
		public required string City { get; set; }

        [Display(Name = "State*")]
		public required string State { get; set; }

        [Display(Name = "Zip*")]
		public required string Zip { get; set; }
	}
}
