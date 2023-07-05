// TPT
using Microsoft.EntityFrameworkCore;
using Univali.Api.DbContexts;
using Univali.Api.Entities;

namespace Univali.Api.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly CustomerContext _context;

    public CustomerRepository(CustomerContext customerContext) 
    {
        _context = customerContext;
    }

    public async Task<bool> SaveChangesAsync() 
    {
        return (await _context.SaveChangesAsync() > 0);
    }

    public async Task<IEnumerable<Customer>> GetCustomersAsync() 
    {
        return await _context.Customers
            .OrderBy(c => c.CustomerId).ToListAsync();       
    }

    public async Task<Customer?> GetCustomerByIdAsync(int customerId)
    {
        return  await _context.Customers
            .FirstOrDefaultAsync(c => c.CustomerId == customerId);
    }
/*
    public async Task<Customer?> GetCustomerByCpfAsync(string cpf)
    {
        return await _context.Customers.FirstOrDefaultAsync(c => c.CPF == cpf);;
    }
*/
    public void AddCustomer(
        Customer customerReference
    ) 
    {  
        _context.Customers.Add(customerReference);
    }

    public void RemoveCustomer(Customer customerReference)
    {
        _context.Customers.Remove(customerReference);
    }

    public async Task<List<Customer>> GetCustomersWithAddressesAsync()
    {
        return await _context.Customers
            .Include(customer => customer.Addresses)
            .ToListAsync();
    }

    public async Task<Customer?> GetCustomerByIdWithAddressesAsync(int customerId)
    {
        return await _context.Customers
            .Include(customer => customer.Addresses)
            .FirstOrDefaultAsync(customer => customer.CustomerId == customerId);
    }
/*
    public async Task<Customer?> GetCustomerByCpfWithAddressesAsync(string cpf)
    {
        return await _context.Customers
            .Include(customer => customer.Addresses)
            .FirstOrDefaultAsync(customer => customer.Cpf == cpf);
    }
*/

    public async Task<List<Address>> GetAddressesAsync(int customerId) 
    {
        return await _context.Addresses
            .Where(a => a.CustomerId == customerId)
            .ToListAsync();
    }

    public async Task<Address?> GetAddressAsync(int customerId, int addressId) 
    {
        return await _context.Addresses
            .FirstOrDefaultAsync(a => a.AddressId == addressId && a.CustomerId == customerId);
    }

    public void AddAddress(Address addressReference) 
    {  
        _context.Addresses.Add(addressReference);
    }

    public void RemoveAddress(Address addressReference)
    {
        _context.Addresses.Remove(addressReference);
    }

    public async Task<bool> CustomerExistsAsync(int customerId)
    {
        return await _context.Customers.AnyAsync(c => c.CustomerId == customerId);
    }

    // FILTRO
    public async Task<IEnumerable<Object>> GetCustomersCollectionAsync()
    {
        return await _context.Customers.ToListAsync();
    }

    // Pablo
    public async Task<IEnumerable<object>> GetCustomersCollection(string? category, string? searchQuery)
    {
        var customersToReturn = new List<object>();

        if (string.IsNullOrWhiteSpace(category) && string.IsNullOrWhiteSpace(searchQuery))
        {
            var customersFromDatabase = await GetCustomersCollectionAsync();

            customersToReturn.AddRange(customersFromDatabase.OfType<LegalCustomer>());

            customersToReturn.AddRange(customersFromDatabase.OfType<NaturalCustomer>());

            return customersToReturn;
        }

        IQueryable<Customer> collection = _context.Customers;

        if (!string.IsNullOrWhiteSpace(category))
        {
            collection = collection.Where(c => c.Type == category.Trim());
        }

        if (!string.IsNullOrWhiteSpace(searchQuery))
        {
            collection = collection.Where(
                c => c.Type.Contains(searchQuery.Trim()) 
                || c.Name.Contains(searchQuery.Trim())
            );
        }

        var customerIds = collection.Select(c => c.CustomerId);

        var naturalCustomersFromDatabase = _context.NaturalCustomers
            .Where(c => customerIds.Contains(c.CustomerId))
            .ToList();

        var legalCustomersFromDatabase = _context.LegalCustomers
            .Where(c => customerIds.Contains(c.CustomerId))
            .ToList();

        if (!string.IsNullOrWhiteSpace(searchQuery))
        {
            naturalCustomersFromDatabase = naturalCustomersFromDatabase
                .Where(c => c.CPF.Contains(searchQuery))
                .ToList();

            legalCustomersFromDatabase = legalCustomersFromDatabase
                .Where(c => c.CNPJ.Contains(searchQuery))
                .ToList();
        }

        customersToReturn.AddRange(naturalCustomersFromDatabase);

        customersToReturn.AddRange(legalCustomersFromDatabase);

        return customersToReturn;
    }
}
    
