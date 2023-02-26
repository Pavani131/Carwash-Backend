using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Carwash.Model
{
    public class Cardetails
    {
        public int Id { get; set; }
        [Required]
        public string Carmodel { get; set; }
        [Required]
        public string Status { get; set; }
    }
}
