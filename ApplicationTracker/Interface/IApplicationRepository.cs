using ApplicationTracker.Models;

namespace ApplicationTracker.Interface;

public interface IApplicationRepository
{
    Task<IEnumerable<Application>> GetApplications();
    
    Task<Application> GetApplication(int id);
    
    Task<Application> AddApplication(Application application);
    
    Task<Application> UpdateApplication(Application application);
}