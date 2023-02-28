using Carwash.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Carwash.Repository
{
    public class ImpOrderdetails:IOrderdetails
    {
        private UserContext _order;
        public ImpOrderdetails(UserContext order)
        {
            _order = order;
        }
        //add package
        public async Task<Orderdetails> Add(Orderdetails order)
        {
            try
            {
                var add = _order.Orderdetails.Add(order);
                await _order.SaveChangesAsync();

            }
            catch
            {
                throw;
            }
            return order;
        }
        //delete
        public async Task Delete(int ID)
        {
            Orderdetails deleteorder = _order.Orderdetails.Find(ID);
            try
            {
                var delete = _order.Orderdetails.Remove(deleteorder);
                await _order.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }


        }
        //getall
        public async Task<List<Orderdetails>> GetAll()
        {
            try
            {
                List<Orderdetails> order = await _order.Orderdetails.ToListAsync();
                return order;
            }
            catch (Exception)
            {
                throw;
            }
        }
        //GetById
        public async Task<Orderdetails> GetByID(int Id)
        {
            try
            {
                Orderdetails order = await _order.Orderdetails.FindAsync(Id);
                return order;
            }
            catch (Exception)
            {
                throw;
            }
        }
        //update
        public async Task<Orderdetails> Update(int Id, Orderdetails order)
        {
            var cus = await _order.Orderdetails.FindAsync(Id);
            if (cus != null)
            {
                cus.WashingInstructions = order.WashingInstructions;
                cus.Date = order.Date;
                cus.packagename = order.status;
                cus.description = order.description;
                cus.price = order.price;
                cus.city = order.city;
                cus.pincode = order.pincode;

                await _order.SaveChangesAsync();
            }
            return cus;

        }
    }
}

