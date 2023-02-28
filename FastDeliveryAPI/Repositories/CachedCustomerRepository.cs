using FastDeliveryAPI.Entity;
using FastDeliveryAPI.Repositories.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace FastDeliveryAPI.Repositories;

public class CachedCustomerRepository : ICustomerRepository
{
    private readonly ICustomerRepository _decorated;
    private readonly IMemoryCache _memoryCache;
    public CachedCustomerRepository(ICustomerRepository decorated, IMemoryCache memoryCache)
    {
        _decorated = decorated;
        _memoryCache = memoryCache;
    }

    public void Add(Customer customer)
    {
        string key = "GetAllCustomers";
        _memoryCache.Remove(key);
        _decorated.Add(customer);

    }

    public void Update(Customer customer)
    {
        string key = "GetAllCustomers";
        _memoryCache.Remove(key);
        _decorated.Update(customer);
    }

    public void Delete(Customer customer)
    {
        throw new NotImplementedException();
    }

    public async Task<IReadOnlyCollection<Customer>> GetAll()
    {
        string key = "GetAllCustomers";
        return await _memoryCache.GetOrCreateAsync(key, async entry =>
        {
            var results = await _decorated.GetAll();
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1);
            return results;
        });
    }


    public Task<Customer?> GetCustomerById(int id, CancellationToken cancellationToken = default)
    {
        string key = $"customer{id}";

        return _memoryCache.GetOrCreateAsync(
            key,
            entry =>
            {
                entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(2));

                return _decorated.GetCustomerById(id, cancellationToken);
            });
    }

}