using System.ComponentModel.DataAnnotations;

namespace Evolent.Web.Models
{
    public class ContactViewModel
    {
        [Key]
        public int ContactId { get; set; }

        [Required(ErrorMessage ="Please Enter First Name.") ]
        [Display(Name ="First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please Enter Last Name.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please Enter Email Id.")]
        [EmailAddress(ErrorMessage ="Invalid Email Id")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please Enter Phone Number.")]
        [Phone(ErrorMessage ="Invalid Phone Number")]
        [Display(Name = "Phone Number")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string PhoneNumber { get; set; }

        public bool Status { get; set; }

    }
}