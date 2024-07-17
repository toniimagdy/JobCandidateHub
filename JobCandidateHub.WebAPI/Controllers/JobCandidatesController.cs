using JobCandidateHub.Models;
using JobCandidateHub.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobCandidateHub.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
            return Ok(await _jobCandidateService.UpsertJobCandidateAsync(jobCandidate));
        }
    }
}
