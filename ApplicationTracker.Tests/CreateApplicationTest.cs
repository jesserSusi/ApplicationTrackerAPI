using ApplicationTracker.Controllers;
using ApplicationTracker.Interface;
using ApplicationTracker.Models;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;

namespace ApplicationTrackerTests;

[TestClass]
public class CreateApplicationTest
{
    private readonly IApplicationRepository _repository;
    private readonly ApplicationsController _controller;
    private readonly List<Application> _applications;

    public CreateApplicationTest()
    {
        _repository = Substitute.For<IApplicationRepository>();
        _controller = new ApplicationsController(_repository);
    }

    [TestMethod]
    public async Task CreateApplication_Should_Create_New_Application()
    {
        // Arrange
        var applicationToAdd = new Application()
        {
            Id = 10,
            CompanyName = "Test Application",
            Position = "Test Position",
            DateApplied = new DateOnly(2020, 1, 1),
            Status = ApplicationStatus.Interview
        };
        _repository.AddApplication(Arg.Any<Application>()).Returns(applicationToAdd);
        
        // Act
        var result = await _controller.AddApplication(applicationToAdd);
        var response = (result.Result as CreatedAtActionResult).Value as Application;
        
        // Assert
        Assert.IsNotNull(response);
        Assert.IsInstanceOfType(response, typeof(Application));
        Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult));
    }

    [TestMethod]
    public async Task CreateApplication_Should_Throw_Error_With_Invalid_Data()
    {
        // Arrange
        var missingDetails = new Application()
        {
            Id = 8, CompanyName = null, DateApplied = new DateOnly(2000, 1, 1), Position = null,
            Status = ApplicationStatus.Interview
        };
        
        // Act
        var nullApplicationResult = await _controller.AddApplication(null);
        var missingDetailsResult = await _controller.AddApplication(missingDetails);
        
        // Assert
        Assert.IsInstanceOfType(nullApplicationResult.Result, typeof(BadRequestObjectResult));
        Assert.IsInstanceOfType(missingDetailsResult.Result, typeof(BadRequestObjectResult));
    }
}