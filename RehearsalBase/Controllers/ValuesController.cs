using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace RehearsalBase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class ValuesController : ControllerBase
    {
        public readonly PostgreDbContext _context;

        public ValuesController(PostgreDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetRehearsals()
        {
            return Ok(await _context.Customers.ToArrayAsync());
        }

        [HttpPut]
        public async Task<ActionResult> AddCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return Ok(await _context.Customers.ToArrayAsync());
        }
    }
}
