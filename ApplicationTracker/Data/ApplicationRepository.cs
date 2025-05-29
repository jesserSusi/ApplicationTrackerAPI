using ApplicationTracker.Interface;
using ApplicationTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace ApplicationTracker.Data
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly ApplicationDbContext _context;

        public ApplicationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Application>> GetApplications()
        {
            return await _context.Applications.AsNoTracking().ToListAsync();
        }

        public async Task<Application> GetApplication(int id)
        {
            return await _context.Applications.AsNoTracking().FirstOrDefaultAsync(app => app.Id == id);
        }

        public async Task<Application> AddApplication(Application application)
        {
            var result = await _context.Applications.AddAsync(application);
            await _context.SaveChangesAsync();
            return result.Entity;
        }
        public async Task<Application> UpdateApplication(Application application)
        {
            var result = await _context.Applications.FirstOrDefaultAsync(app => app.Id == application.Id);

            if (result != null)
            {
                result.CompanyName = application.CompanyName;
                result.Position =  application.Position;
                result.DateApplied = application.DateApplied;
                result.Status = application.Status;
            
                await _context.SaveChangesAsync();
                return result;
            }
            return null;
        }
    }
}