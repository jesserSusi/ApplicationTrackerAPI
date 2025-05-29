using ApplicationTracker.Models;
using ApplicationTracker.Interface;
using Microsoft.EntityFrameworkCore;

namespace ApplicationTracker.Data
{
    public class ApplicationStatusesRepository : IApplicationStatusRepository
    {
        private readonly ApplicationDbContext _context;

        public ApplicationStatusesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Status>> GetApplicationStatuses()
        {
            return await _context.Statuses.ToListAsync();
        }
    }
}