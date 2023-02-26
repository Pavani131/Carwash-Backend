using Carwash.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Carwash.Repository
{
   public interface IUser
    {
        Task<List<User> >GetAll();
        Task <User> GetByID(int Id);
        Task<User> AddUser(User user);
        Task<User> UpdateUser(int Id,User user);
        Task DeleteUser(int Id);
        Task<User> Login(Login login);
        //email validation
        Task<bool> CheckEmailExistAsync(string Email);
       
    }
}
