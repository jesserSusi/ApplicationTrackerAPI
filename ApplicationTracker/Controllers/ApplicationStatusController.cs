using ApplicationTracker.Data;
using ApplicationTracker.Interface;
using ApplicationTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApplicationTracker.Controllers
{
    [Route("statuses")]
    [ApiController]
    public class ApplicationStatusController : Controller
    {
        private readonly IApplicationStatusRepository _statusRepository;
    
        public ApplicationStatusController(IApplicationStatusRepository statusRepository)
        {
            _statusRepository = statusRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Status>>> GetApplicationStatuses()
        {
            return Ok(await _statusRepository.GetApplicationStatuses());
        }
    }
}