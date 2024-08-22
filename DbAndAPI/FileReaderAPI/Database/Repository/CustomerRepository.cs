using Database.Models;
using Database.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Repository;

public class CustomerRepository : ICustomerRepository
{
    private readonly DatabaseDbContext _dbContext;

    public CustomerRepository(DatabaseDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException("Context cannot be null when creating the repository");
    }

    public Task<bool> CreateCustomer(Customer customer)
    {
        var success = true;

        using (var transaction = _dbContext.Database.BeginTransaction())
        {
            try
            {
                _dbContext.Customer.Add(customer);
                _dbContext.SaveChanges();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                success = false;
            }
        }

        return Task.FromResult(success);
    }

    public async Task<Customer> GetByCustomerRef(string customerRef) => 
        await _dbContext.Customer.FirstOrDefaultAsync(c => c.CustomerRef == customerRef);
}
