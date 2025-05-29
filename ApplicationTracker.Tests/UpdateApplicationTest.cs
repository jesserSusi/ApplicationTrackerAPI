using ApplicationTracker.Controllers;
using ApplicationTracker.Interface;
using ApplicationTracker.Models;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;

namespace ApplicationTrackerTests;

[TestClass]
public class UpdateApplicationTest
{
    private readonly IApplicationRepository _repository;
    private readonly ApplicationsController _controller;
    private readonly List<Application> _applications;
    private readonly Application _applicationToUpdate;

    public UpdateApplicationTest()
    {
        _applications = new List<Application>()
        {
            new Application()
            {
                Id = 1, CompanyName = "Company 1", Position = "Test Position",
                DateApplied = new DateOnly(2000, 1, 1), Status = ApplicationStatus.Interview
            },
            new Application()
            {
                Id = 2, CompanyName = "Company 2", Position = "Test Position",
                DateApplied = new DateOnly(2000, 1, 1), Status = ApplicationStatus.Interview
            },
            new Application()
            {
                Id = 3, CompanyName = "Company 3", Position = "Test Position",
                DateApplied = new DateOnly(2000, 1, 1), Status = ApplicationStatus.Interview
            },
            new Application()
            {
                Id = 4, CompanyName = "Company 4", Position = "Test Position",
                DateApplied = new DateOnly(2000, 1, 1), Status = ApplicationStatus.Interview
            },
            new Application()
            {
                Id = 5, CompanyName = "Company 5", Position = "Test Position",
                DateApplied = new DateOnly(2000, 1, 1), Status = ApplicationStatus.Interview
            }
        };
        
        _applicationToUpdate = new Application()
        {
            Id = 1,
            CompanyName = "Company Name Updated",
            Position = "Test Position",
            DateApplied = new DateOnly(2000, 1, 1),
            Status = ApplicationStatus.Interview
        };
        
        _repository = Substitute.For<IApplicationRepository>();
        _controller = new ApplicationsController(_repository);
    }

    [TestMethod]
    public async Task UpdateApplication_Should_Update_Application()
    {
        // Arrange
        var applicationId = 1;
        var application = _applications.FirstOrDefault(app => app.Id == applicationId);
        _repository.GetApplication(applicationId).Returns(application);
        _repository.UpdateApplication(_applicationToUpdate).Returns(_applicationToUpdate);
        
        // Act
        var result = await _controller.UpdateApplication(applicationId, _applicationToUpdate);
        
        // Assert
        Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
    }

    [TestMethod]
    public async Task UpdateApplication_Should_Throw_An_Error_If_Id_Mismatch()
    {
        // Arrange
        var applicationId = 99;
        
        // Act
        var result = await _controller.UpdateApplication(applicationId, _applicationToUpdate);
        
        // Assert
        Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));
    }

    [TestMethod]
    public async Task UpdateApplication_Should_Throw_An_Error_If_Details_Are_Null()
    {
        // Arrange
        var applicationId = 8;
        var missingDetails = new Application()
        {
            Id = 8, CompanyName = null, DateApplied = new DateOnly(2000, 1, 1), Position = null,
            Status = ApplicationStatus.Interview
        };
        
        // Act
        var result = await _controller.UpdateApplication(applicationId, missingDetails);
        
        // Assert
        Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));
    }
}