using Database.Models;
using Database.Repository.Interfaces;
using FileReaderAPI.Controllers;
using Moq;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace FileReaderAPITests;

public class CustomerControllerTests
{
    private readonly Mock<ICustomerRepository> _repository;
    private readonly CustomerController _controller;

    public CustomerControllerTests()
    {
        _repository = new Mock<ICustomerRepository>();
        _controller = new CustomerController(_repository.Object);
    }

    #region Happy Path Tests
    [Fact]
    public async Task GetCustomerByRefAsync_ReturnsExpectedDataAsync()
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

        _repository.Setup( s => s.GetByCustomerRef(customerRef)).Returns(Task.FromResult(customerObject));

        //Act
        var result = await _controller.GetCustomerByRefAsync(customerRef);

        //Assert
        var okResult = result.Should().BeAssignableTo<OkObjectResult>();
        result.Should().NotBeNull();
        result.Should().BeOfType<OkObjectResult>();
        okResult.Should().NotBeNull();
        okResult.Subject.Value.Should().Be(customerObject);
    }

    [Fact]
    public async void PostCustomerByRefAsync_SuccessfullyCreatesRecord()
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

        _repository.Setup(r => r.CreateCustomer(customerObject)).Returns(Task.FromResult(true));

        //Act
        var result = await _controller.PostCustomerByRefAsync(customerObject);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<OkResult>();
    }
    #endregion
}