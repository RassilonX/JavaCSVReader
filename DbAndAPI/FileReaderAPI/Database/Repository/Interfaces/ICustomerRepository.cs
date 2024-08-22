using Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Repository.Interfaces;

public interface ICustomerRepository
{
    public Task<Customer> GetByCustomerRef(string customerRef);

    public Task<bool> CreateCustomer(Customer customer);
}
