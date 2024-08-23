using Database;
using Database.Models;
using Database.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DatabaseTests;

public class CustomerRepositoryIntegrationTests
{
    private readonly DatabaseDbContext _dbContext;
    private readonly CustomerRepository _repository;

    public CustomerRepositoryIntegrationTests()
    {
        var options = CreateOptions();

        _dbContext = new DatabaseDbContext(options);
        _repository = new CustomerRepository(_dbContext);
    }

    #region Happy Path Tests
    [Fact]
    public async void GetByCustomerRef_ReturnsCustomerPresentInDatabase()
    {
        //Arrange
        var customerRef = "customerTest";

        var customerObject = new Customer { 
            CustomerRef = customerRef, 
            CustomerName = "John Tester", 
            AddressLine1 = "Test House", 
            AddressLine2 = "Test Building", 
            Town = "Testmouth", 
            County = "Testshire", 
            Country = "Testland", 
            Postcode = "TE573RS" 
        };

        _dbContext.Customer.Add(customerObject);
        _dbContext.SaveChanges();

        //Act
        var result = await _repository.GetByCustomerRef(customerRef);

        //We remove the data we inserted to make the test repeatable whether it succeeds or fails
        _dbContext.Customer.Remove(customerObject);
        _dbContext.SaveChanges();

        //Assert
        Assert.NotNull(result);
        Assert.IsType<Customer>(result);
        Assert.Equal(customerRef, result.CustomerRef);
        Assert.Equal(customerObject, result);
    }

    [Fact]
    public async void CreateCustomer_CreatesCustomerRecordInDatabase()
    {
        //Arrange
        var customerRef = "customerTest";

        var customerObject = new Customer
        {
            CustomerRef = customerRef,
            CustomerName = "John Tester",
            AddressLine1 = "Test House",
            AddressLine2 = "Test Building",
            Town = "Testmouth",
            County = "Testshire",
            Country = "Testland",
            Postcode = "TE573RS"
        };

        //Act
        var result = await _repository.CreateCustomer(customerObject);
        var createdObject = _dbContext.Customer.Where(c => c.CustomerRef == customerRef).FirstOrDefault();

        if (result)
        {
            _dbContext.Customer.Remove(customerObject);
            _dbContext.SaveChanges();
        }

        Assert.NotNull(createdObject);
        Assert.IsType<Customer>(createdObject);
        Assert.True(result);
        Assert.Equal(customerObject, createdObject);
    }
    #endregion

    #region Unhappy Path Tests
    [Fact]
    public async Task CreateCustomer_CustomerRecordAlreadyInDatabase()
    {
        //Arrange
        var customerRef = "customerTest";

        var customerObject = new Customer
        {
            CustomerRef = customerRef,
            CustomerName = "John Tester",
            AddressLine1 = "Test House",
            AddressLine2 = "Test Building",
            Town = "Testmouth",
            County = "Testshire",
            Country = "Testland",
            Postcode = "TE573RS"
        };

        _dbContext.Customer.Add(customerObject);
        await _dbContext.SaveChangesAsync();

        //Act
        var result = await _repository.CreateCustomer(customerObject);

        _dbContext.Customer.Remove(customerObject);
        await _dbContext.SaveChangesAsync();

        //Assert
        Assert.False(result);
    }

    [Fact]
    public async void CreateCustomer_NullCustomerProvided()
    {
        //Arrange - Act
        var result = await _repository.CreateCustomer(null);

        //Assert
        Assert.False(result);
    }

    [Fact]
    public async void CreateCustomer_EmptyCustomerProvided()
    {
        //Arrange
        var customerObject = new Customer();

        //Act
        var result = await _repository.CreateCustomer(customerObject);

        //Assert
        Assert.False(result);
    }

    [Fact]
    public async void GetByCustomerRef_NoRecordFound()
    {
        //Arrange
        var customerRef = "customerTest";

        //Act
        var result = await _repository.GetByCustomerRef(customerRef);

        //Assert
        Assert.Null(result);
    }

    [Fact]
    public async void GetByCustomerRef_NoRefProvided()
    {
        //Arrange
        var customerRef = "";

        //Act
        var result = await _repository.GetByCustomerRef(customerRef);

        //Assert
        Assert.Null(result);
    }

    [Fact]
    public async void GetByCustomerRef_NullRefProvided()
    {
        //Arrange - Act
        var result = await _repository.GetByCustomerRef(null);

        //Assert
        Assert.Null(result);
    }
    #endregion

    private DbContextOptions<DatabaseDbContext> CreateOptions()
    {
        var builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        var configurationRoot = builder.Build();
        var connectionString = configurationRoot.GetConnectionString("sql_db");

        var options = new DbContextOptionsBuilder<DatabaseDbContext>()
                 .UseSqlServer(connectionString).Options;
        return options;
    }
}