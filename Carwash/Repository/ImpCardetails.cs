using Carwash.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Carwash.Repository
{
    public class ImpCardetails:ICardetails
    {
        private UserContext _car;
        public ImpCardetails(UserContext car)
        {
            _car = car;
        }
        //add package
        public async Task<Cardetails> Add(Cardetails cardetails)
        {
            try
            {
                var add = _car.Cardetails.Add(cardetails);
                await _car.SaveChangesAsync();

            }
            catch
            {
                throw;
            }
            return cardetails;
        }
        //delete
        public async Task Delete(int ID)
        {
            Cardetails deletecardetails = _car.Cardetails.Find(ID);
            try
            {
                var delete = _car.Cardetails.Remove(deletecardetails);
                await _car.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }


        }
        //getall
        public async Task<List<Cardetails>> GetAll()
        {
            try
            {
                List<Cardetails> cardetails = await _car.Cardetails.ToListAsync();
                return cardetails;
            }
            catch (Exception)
            {
                throw;
            }
        }
        //GetById
        public async Task<Cardetails> GetByID(int Id)
        {
            try
            {
                Cardetails cardetails = await _car.Cardetails.FindAsync(Id);
                return cardetails;
            }
            catch (Exception)
            {
                throw;
            }
        }
        //update
        public async Task<Cardetails> Update(int Id, Cardetails cardetails)
        {
            var cus = await _car.Cardetails.FindAsync(Id);
            if (cus != null)
            {
                cus.Carmodel = cardetails.Carmodel;
                cus.Status = cardetails.Status;
                

                await _car.SaveChangesAsync();
            }
            return cus;

        }
    }
}

