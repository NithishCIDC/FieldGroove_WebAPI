using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FieldGroove.Domain.Models
{
    public class ChangePasswordModel
    {
        [Required(ErrorMessage = "Old Password is required")]
        public required string Old_Password { get; set; }

        [Required(ErrorMessage = "New Password is required")]
        public required string New_Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [Compare("New_Password", ErrorMessage = "Confirm Password should Match with new Password")]
        public required string Confirm_Password { get; set; }

    }
}
