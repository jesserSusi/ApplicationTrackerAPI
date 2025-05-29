using ApplicationTracker.Controllers;
using ApplicationTracker.Interface;
using ApplicationTracker.Models;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace ApplicationTrackerTests;

[TestClass]
public class GetApplicationTest
{
    private readonly IApplicationRepository _repository;
    private readonly ApplicationsController _controller;
    private readonly List<Application> _applications;
    
    public GetApplicationTest()
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
        
        _repository = Substitute.For<IApplicationRepository>();
        _controller = new ApplicationsController(_repository);
    }

    [TestMethod]
    public async Task GetApplicationById_Should_Return_Application()
    {
        // Arrange
        var applicationId = 1;
        var application = _applications.FirstOrDefault(app => app.Id == 1);
        _repository.GetApplication(application.Id).Returns(application);
        
        // Act
        var result = await _controller.GetApplication(applicationId);
        var searchResult = (result.Result as OkObjectResult).Value as Application;
        
        // Assert
        Assert.IsNotNull(searchResult);
        Assert.IsInstanceOfType(searchResult, typeof(Application));
        Assert.IsTrue(searchResult.Id == applicationId);
        Assert.IsTrue(searchResult.CompanyName == application.CompanyName);
    }

    [TestMethod]
    public async Task GetApplication_Should_Throw_Error_If_NotFound()
    {
        // Arrange
        var applicationId = 999;
        
        // Act
        var result = _controller.GetApplication(applicationId);
        
        // Assert
        Assert.IsInstanceOfType(result.Result.Result, typeof(NotFoundObjectResult));
    }
}