using Carwash.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Carwash.Repository
{
   public interface ICardetails
    {
        Task<List<Cardetails>> GetAll();
        Task<Cardetails> GetByID(int Id);
        Task<Cardetails> Add(Cardetails cardetails);
        Task<Cardetails> Update(int Id, Cardetails cardetails);
        Task Delete(int Id);
    }
}
