using FastDeliveryAPI.Data;
using FastDeliveryAPI.Entity;
using FastDeliveryAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FastDeliveryAPI.Repositories;

public class CustomerRepository : ICustomerRepository
{

    private readonly FastDeliveryDbContext _dbContext;

    public CustomerRepository(FastDeliveryDbContext dbContext) => _dbContext = dbContext;


    public void Add(Customer customer) =>
        _dbContext.Set<Customer>().Add(customer);

    public async Task<IReadOnlyCollection<Customer>> GetAll() =>
        await _dbContext
            .Set<Customer>()
            .ToListAsync();
    public async Task<Customer?> GetCustomerById(int id, CancellationToken cancellationToken = default) =>
        await _dbContext
            .Set<Customer>()
            .FirstOrDefaultAsync(customer => customer.Id == id, cancellationToken);

    // public async Task<Customer?> GetCustomerByName(string name, CancellationToken cancellationToken = default) =>
    // await _dbContext
    //     .Set<Customer>()
    //     .FirstOrDefaultAsync(customer => customer.Name == name, cancellationToken);

    public void Update(Customer customer) =>
        _dbContext.Set<Customer>().Add(customer);

    public void Delete(Customer customer) =>
        _dbContext.Set<Customer>().Remove(customer);
}