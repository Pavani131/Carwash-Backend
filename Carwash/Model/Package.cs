using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Carwash.Model
{
    public class Package
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Nullable<decimal>price { get; set; }
        public string Status { get; set; }
    }
}
