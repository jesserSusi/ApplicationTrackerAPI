using ApplicationTracker.Data;
using ApplicationTracker.Helpers;
using ApplicationTracker.Interface;
using ApplicationTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApplicationTracker.Controllers
{
    [Route("applications")]
    [ApiController]
    public class ApplicationsController : ControllerBase
    {
        private readonly IApplicationRepository _applicationRepository;
        
        public ApplicationsController(IApplicationRepository applicationRepository)
        {
            _applicationRepository = applicationRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Application>>> GetAllApplications()
        {
            return Ok(await _applicationRepository.GetApplications());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Application>> GetApplication(int id)
        {
            var application = await _applicationRepository.GetApplication(id);
            if (application == null)
            {
                return NotFound($"Application Id: ({id}) was not found.");
            }
            return Ok(application);
        }

        [HttpPost]
        public async Task<ActionResult<List<Application>>> AddApplication(Application application)
        {
            var checkResult = Helper.IsValidApplicationDetails(application);

            if (!string.IsNullOrEmpty(checkResult))
            {
                return BadRequest(checkResult);
            }
            
            var createdApplication = await _applicationRepository.AddApplication(application);
            
            return CreatedAtAction(nameof(GetApplication), new { id = createdApplication.Id }, createdApplication);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<Application>>> UpdateApplication(int id, Application application)
        {
            if (id != application.Id)
            {
                return BadRequest("Application not found.");
            }
            
            var checkResult = Helper.IsValidApplicationDetails(application);

            if (!string.IsNullOrEmpty(checkResult))
            {
                return BadRequest(checkResult);
            }
            
            var result = await _applicationRepository.UpdateApplication(application);

            if (result == null)
            {
                return BadRequest("Application not found.");
            }
            
            return Ok(result);
        }
    }
}
