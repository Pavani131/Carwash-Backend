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
    public class PackageController : ControllerBase
    {
        private readonly IPackage _package;
        public PackageController(IPackage package)
        {
            _package = package;
        }
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var packages = await _package.GetAll();
            if (packages == null)
            {
                return NotFound();
            }
            return Ok(packages);
        }
        [HttpGet("{Id}")]
         public async Task<ActionResult<Package>> GetByID(int Id)
        {
            var packages = await _package.GetByID(Id);
            if (packages == null)
            {
                return NotFound();
            }
            return Ok(packages);
        }
        [HttpPost]
        public async Task<ActionResult<Package>> Add(Package package)
        {
            
            
            var add = await _package.Add(package);
            return Ok(new
            {
                Message = "Packages added Successfull"
            });
            if (package == null)
            {
                return BadRequest();
            }
        }
        [HttpPut("{Id}")]
        public async Task<ActionResult> Update(int Id, Package package)
        {
            var packages = await _package.Update(Id, package);
            return CreatedAtAction(nameof(GetByID), new { id = packages.Id }, packages);
        }
        [HttpDelete("{Id}")]
        public async Task<ActionResult> Delete(int Id)
        {
            await _package.Delete(Id);
            return Ok();
        }

    }
}
