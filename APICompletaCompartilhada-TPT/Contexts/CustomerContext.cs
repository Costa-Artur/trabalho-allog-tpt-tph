using Microsoft.EntityFrameworkCore;
using Univali.Api.Entities;

namespace Univali.Api.DbContexts;

public class CustomerContext : DbContext
{
// TPT
    public DbSet<Customer> Customers { get; set; } = null!;

    public DbSet<Address> Addresses { get; set; } = null!;

    public DbSet<NaturalCustomer> NaturalCustomers { get; set; } = null!;

    public DbSet<LegalCustomer> LegalCustomers { get; set; } = null!;

    public CustomerContext(DbContextOptions<CustomerContext> options) : 
        base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var customer = modelBuilder.Entity<Customer>().UseTptMappingStrategy();

        customer
            .HasMany(customer => customer.Addresses)
            .WithOne(address => address.customer)
            .HasForeignKey(address => address.CustomerId);

        customer
            .Property(a => a.Name)
            .HasMaxLength(100)
            .IsRequired();


        var legalCustomer = modelBuilder.Entity<LegalCustomer>();

        legalCustomer
            .Property(a => a.CNPJ)
            .IsFixedLength()
            .HasMaxLength(14)
            .IsRequired();

        legalCustomer
            .HasData(
                new LegalCustomer()
                {
                    CustomerId = 1,
                    Name = "Linus Torvalds",
                    CNPJ = "14698277000144"
                }   
            );

        var naturalCustomer = modelBuilder.Entity<NaturalCustomer>();

        naturalCustomer
            .Property(a => a.CPF)
            .IsFixedLength()
            .HasMaxLength(11)
            .IsRequired();
        
        naturalCustomer
            .HasData(
                new NaturalCustomer
                {
                    CustomerId = 2,
                    Name = "Bill Gates",
                    CPF = "95395994076"
                }
            );
// TPT FIM

        var address = modelBuilder.Entity<Address>();
            
        address
            .Property(address => address.Street)
            .HasMaxLength(50)
            .IsRequired();

        address
            .Property(address => address.City)
            .HasMaxLength(50)
            .IsRequired();

        address
            .Property(address => address.CustomerId)
            .IsRequired();

        address
            .HasData(
                new Address()
                {
                    AddressId = 1,
                    Street = "Verão do Cometa",
                    City = "Elvira",
                    CustomerId = 1
                },
                new Address()
                {
                    AddressId = 2,
                    Street = "Borboletas Psicodélicas",
                    City = "Perobia",
                    CustomerId = 1
                },
                new Address()
                {
                    AddressId = 3,
                    Street = "Canção Excêntrica",
                    City = "Salandra",
                    CustomerId = 2
                }
            );

        base.OnModelCreating(modelBuilder);
    }
}