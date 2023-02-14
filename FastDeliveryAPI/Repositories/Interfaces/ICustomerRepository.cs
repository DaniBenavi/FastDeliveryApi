using FastDeliveryAPI.Entity;

namespace FastDeliveryAPI.Repositories.Interfaces;


public interface ICustomerRepository
{
    Task<IReadOnlyCollection<Customer>> GetAll();
    Task<Customer?> GetCustomerById(int id, CancellationToken cancellationToken = default);
    //Task<Customer?> GetCustomerByName(string name, CancellationToken cancellationToken = default);

    void Add(Customer customer);
    void Update(Customer customer);
    void Delete(Customer customer);
}