using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Carwash.Model
{
    public class User
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }


        [Required(ErrorMessage = "Email is required.")]
        [StringLength(60)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [StringLength(10)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "PhNumber")]
        [StringLength(10)]
        [Required(ErrorMessage = "PhNumber is required")]
        [RegularExpression("[6-9][0-9]{9}", ErrorMessage ="Mobile Number must begin with 6,7,8 or 9")]
        public string PhNumber { get; set; }
        [Required]
        [RegularExpression(@"Customer|Washer")]
        public string Role { get; set; }
        [Required]
        [RegularExpression(@"Active|InActive")]
        public string IsActive { get; set; }
    }
}
