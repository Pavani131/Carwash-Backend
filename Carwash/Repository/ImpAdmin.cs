using Carwash.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Carwash.Repository
{
    public class ImpAdmin:IAdmin
    {
        private UserContext _admin;
        public ImpAdmin(UserContext admin)
        {
            _admin = admin;
        }
        //add package
        public async Task<Admin> Add(Admin admin)
        {
            try
            {
                var add = _admin.Admins.Add(admin);
                await _admin.SaveChangesAsync();

            }
            catch
            {
                throw;
            }
            return admin;
        }
        //delete
        public async Task Delete(int ID)
        {
            Admin deleteadmin = _admin.Admins.Find(ID);
            try
            {
                var delete = _admin.Admins.Remove(deleteadmin);
                await _admin.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }


        }
        //getall
        public async Task<List<Admin>> GetAll()
        {
            try
            {
                List<Admin> admin = await _admin.Admins.ToListAsync();
                return admin;
            }
            catch (Exception)
            {
                throw;
            }
        }
        //GetById
        public async Task<Admin> GetByID(int Id)
        {
            try
            {
                Admin admin = await _admin.Admins.FindAsync(Id);
                return admin;
            }
            catch (Exception)
            {
                throw;
            }
        }
        //update
        public async Task<Admin> Update(int Id, Admin admin)
        {
            var cus = await _admin.Admins.FindAsync(Id);
            if (cus != null)
            {
                cus.Email = admin.Email;
                cus.Password = admin.Password;
                

                await _admin.SaveChangesAsync();
            }
            return cus;

        }
       public async Task<Admin> AdminLogin(AdminLogin alogin)
        {
            var admin = await _admin.Admins.FirstOrDefaultAsync(x => x.Email == alogin.Email && x.Password == alogin.Password);
            if (admin == null)
            {
                return null;
            }
            return admin;
        }
        public async Task<bool> CheckEmailExistAsync(string Email)
        {
            var check = await _admin.Admins.AnyAsync(x => x.Email == Email);
            return check;
        }
    }
}

