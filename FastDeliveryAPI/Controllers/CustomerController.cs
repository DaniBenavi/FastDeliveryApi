using FastDeliveryAPI.Data;
using FastDeliveryAPI.Entity;
using Microsoft.AspNetCore.Mvc;


namespace FastDeliveryAPI.Controllers;

[ApiController]
[Route("api/customer")]
public class CustomerController : ControllerBase
{
    private readonly FastDeliveryDbContext _context;

    public CustomerController(FastDeliveryDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Customer>> Get()
    {
        var customers = _context.Customers.ToList();
        return Ok(customers);
    }

    [HttpPost("Add-Customer")]
    public async Task<ActionResult> CreateCustomer(Customer customer)
    {
        if (ModelState.IsValid)
        {
            var result = _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
        }
        return Ok(customer);
    }
    [HttpPut("Modify-Customer")]
    public async Task<ActionResult> ModifyCustomer(Customer customer)
    {
        if (ModelState.IsValid)
        {
            if (_context.Customers.Where(x => x.Id == customer.Id).Any())
            {
                var result = _context.Customers.Update(customer);
                await _context.SaveChangesAsync();
            }
            else
            {
                return StatusCode(StatusCodes.Status404NotFound, "Error: Nothing found");
            }
        }
        return Ok(customer);
    }

    [HttpDelete("Delete-Customer/{id}")]
    public ActionResult DeleteCustomer(int id)
    {
        var objects = _context.Customers.Find(id);
        if (objects != null)
        {

            _context.Customers.Remove(objects);
            _context.SaveChanges();
            return StatusCode(StatusCodes.Status200OK, objects);
        }
        else
        {
            return StatusCode(StatusCodes.Status404NotFound, "Error: Nothing found ");
        }

    }

    [HttpGet("Get-Customer/{id}")]
    public ActionResult FindCustomers(int id)
    {
        var find = from s in _context.Customers select s;

        if (id != 0)
        {
            find = find.Where(s => s.Id == id);

        }
        else
        {
            return StatusCode(StatusCodes.Status404NotFound, "Error: Nothing found");
        }
        return Ok(find.ToList());
    }

}
