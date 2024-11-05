using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace FieldGroove.Domain.Models
{
    public class ForgotPasswordModel
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
    }
}
