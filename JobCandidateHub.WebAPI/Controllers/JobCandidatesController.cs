using JobCandidateHub.Models;
using JobCandidateHub.Services.IServices;
using JobCandidateHub.WebAPI.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobCandidateHub.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ValidateModel]
    public class JobCandidatesController : ControllerBase
    {
        private readonly IJobCandidateService _jobCandidateService;
        private readonly ILogger<JobCandidatesController> _logger;


        public JobCandidatesController(IJobCandidateService jobCandidateService, ILogger<JobCandidatesController> logger)
        {
            _jobCandidateService = jobCandidateService;
            _logger = logger;
        }

        [HttpPost("upsert")]
        public async Task<IActionResult> Upsert(JobCandidateModel jobCandidate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(await _jobCandidateService.UpsertJobCandidateAsync(jobCandidate));
        }
    }
}
