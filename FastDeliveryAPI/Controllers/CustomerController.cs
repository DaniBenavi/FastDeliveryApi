using FastDeliveryAPI.Data;
using FastDeliveryAPI.Entity;
using FastDeliveryAPI.Models;
using FastDeliveryAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace FastDeliveryAPI.Controllers;

[ApiController]
[Route("api/customer")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IUnitOfWork _unitOfWork;
    public CustomerController(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
    {
        _customerRepository = customerRepository;
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Customer>>> Get()
    {
        var customers = await _customerRepository.GetAll();
        return Ok(customers);
    }

    [HttpPost]
    public async Task<ActionResult> CreateCustomer([FromBody] CreateCustomerRequest request, CancellationToken cancellationToken)
    {
        var customer = new Customer(
            request.Name,
            request.PhoneNumber,
            request.Email,
            request.Address
        );
        _customerRepository.Add(customer);
        await _unitOfWork.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetCustomerByID), new { id = customer.Id }, customer);
    }

    [HttpPut("Modify-Customer/{id:int}")]
    public async Task<ActionResult> UpdateCustomer(int id, [FromBody] UpdateCustomerRequest request, CancellationToken cancellationToken)
    {
        if (request.Id != id)
        {
            return BadRequest("Body id is not equal than url Id");
        }

        var customer = await _customerRepository.GetCustomerById(id);
        if (customer is null)
        {
            return NotFound($"Customer not found with the id {id}");
        }

        customer.ChangeName(request.Name);
        customer.ChangePhoneNumber(request.PhoneNumber);
        customer.ChangeEmail(request.Email);
        customer.ChangeAddress(request.Address);
        customer.ChangeStatus(request.Status);

        _customerRepository.Update(customer);
        await _unitOfWork.SaveChangesAsync();

        return NoContent();
    }


    [HttpDelete("Delete-Customer/{id:int}")]
    public async Task<ActionResult> DeleteCustomer(int id, [FromBody] DeleteCustomerRequest request, CancellationToken cancellationToken)
    {
        if (request.Id != id)
        {
            return BadRequest("Body id is not equal than url Id");
        }

        var customer = await _customerRepository.GetCustomerById(id);
        if (customer is null)
        {
            return NotFound($"Customer not found with the id {id}");
        }

        _customerRepository.Delete(customer);
        await _unitOfWork.SaveChangesAsync();

        return Ok(customer);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetCustomerByID(int id, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetCustomerById(id);
        if (customer is null)
        {
            return NotFound($"Customer not found with the id {id}");
        }
        return Ok(customer);
    }

    /*[HttpGet("{name:string}")]
    public async Task<IActionResult> GetCustomerByName(string name, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetCustomerByName(name);
        if (customer is null)
        {
            return NotFound($"Customer not found with the id {name}");
        }
        return Ok(customer);
    }*/

    /*[HttpPut("Modify-Customer")]
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
*/
}
