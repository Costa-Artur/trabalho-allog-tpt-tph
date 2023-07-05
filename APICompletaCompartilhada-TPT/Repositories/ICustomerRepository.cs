// TPT
using Univali.Api.Entities;

namespace Univali.Api.Repositories;

public interface ICustomerRepository 
{
    Task<bool> SaveChangesAsync();

    Task<IEnumerable<Customer>> GetCustomersAsync();
    
    Task<Customer?> GetCustomerByIdAsync(int customerId);

    //Task<Customer?> GetCustomerByCpfAsync(string cpf);

    void AddCustomer(Customer customerReference);

    void RemoveCustomer(Customer customerReference);

    Task<List<Customer>> GetCustomersWithAddressesAsync();

    Task<Customer?> GetCustomerByIdWithAddressesAsync(int customerId);

    //Task<Customer?> GetCustomerByCpfWithAddressesAsync(string cpf);

    Task<bool> CustomerExistsAsync(int customerId);

    Task<IEnumerable<object>> GetCustomersCollection(string? category, string? searchQuery);

    Task<IEnumerable<Object>> GetCustomersCollectionAsync();

// Addresses

    Task<List<Address>> GetAddressesAsync(int customerId);

    Task<Address?> GetAddressAsync(int customerId, int addressId);
    
    void AddAddress(Address addressReference);

    void RemoveAddress(Address addressReference);
}