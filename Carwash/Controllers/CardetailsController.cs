using Carwash.Model;
using Carwash.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Carwash.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardetailsController : ControllerBase
    {
        private readonly ICardetails _car;
        public CardetailsController(ICardetails car)
        {
            _car = car;
        }
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var car = await _car.GetAll();
            if (car == null)
            {
                return NotFound();
            }
            return Ok(car);
        }
        [HttpGet("{Id}")]
        public async Task<ActionResult<Cardetails>> GetByID(int Id)
        {
            var car = await _car.GetByID(Id);
            if (car == null)
            {
                return NotFound();
            }
            return Ok(car);
        }
        [HttpPost]
        public async Task<ActionResult<Cardetails>> Add(Cardetails cardetails)
        {


            var add = await _car.Add(cardetails);
            return Ok(new
            {
                Message = "Cardetails added Successfull"
            });
            if (cardetails == null)
            {
                return BadRequest();
            }
        }
        [HttpPut("{Id}")]
        public async Task<ActionResult> Update(int Id, Cardetails cardetails)
        {
            var car = await _car.Update(Id, cardetails);
            return CreatedAtAction(nameof(GetByID), new { id = cardetails.Id }, car);
        }
        [HttpDelete("{Id}")]
        public async Task<ActionResult> Delete(int Id)
        {
            await _car.Delete(Id);
            return Ok();
        }

    }
}

