using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Carwash.Model
{
    public class Orderdetails
    { 
    
        public int Id { get; set; }
        public string WashingInstructions { get; set; }
        public DateTime Date { get; set; }
        public string status { get; set; }
        public string packagename { get; set; }
        public string description { get; set; }
        public decimal price { get; set; }
        public string city { get; set; }
        public string pincode { get; set; }
    }
}
