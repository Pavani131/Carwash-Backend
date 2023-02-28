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
    public class OrdersController : ControllerBase
    {
        private readonly IOrderdetails _order;
        public OrdersController(IOrderdetails order)
        {
            _order = order;
        }
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var orderdetails = await _order.GetAll();
            if (orderdetails == null)
            {
                return NotFound();
            }
            return Ok(orderdetails);
        }
        [HttpGet("{Id}")]
        public async Task<ActionResult<Orderdetails>> GetByID(int Id)
        {
            var orderdetails = await _order.GetByID(Id);
            if (orderdetails == null)
            {
                return NotFound();
            }
            return Ok(orderdetails);
        }
        [HttpPost]
        public async Task<ActionResult<Orderdetails>> Add(Orderdetails order)
        {


            var add = await _order.Add(order);
            return Ok(new
            {
                Message = "Orders added Successfull"
            });
            if (order == null)
            {
                return BadRequest();
            }
        }
        [HttpPut("{Id}")]
        public async Task<ActionResult> Update(int Id, Orderdetails order)
        {
            var orderdetails = await _order.Update(Id, order);
            return CreatedAtAction(nameof(GetByID), new { id = orderdetails.Id }, orderdetails);
        }
        [HttpDelete("{Id}")]
        public async Task<ActionResult> Delete(int Id)
        {
            await _order.Delete(Id);
            return Ok();
        }

    }
}

