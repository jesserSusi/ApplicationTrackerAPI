using ApplicationTracker.Models;

namespace ApplicationTracker.Interface;

public interface IApplicationStatusRepository
{
    Task<IEnumerable<Status>> GetApplicationStatuses();
}