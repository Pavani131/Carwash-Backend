using Carwash.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Carwash.Repository
{
    public interface IOrderdetails
    {
        Task<List<Orderdetails>> GetAll();
        Task<Orderdetails> GetByID(int Id);
        Task<Orderdetails> Add(Orderdetails order);
        Task<Orderdetails> Update(int Id, Orderdetails order);
        Task Delete(int Id);
    }
}
