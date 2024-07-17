using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobCandidateHub.DataAccess.Models
{
    public class JobCandidate
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public TimeSpan PreferredCallTimeFrom { get; set; }
        public TimeSpan PreferredCallTimeTo { get; set; }
        public string LinkedInProfileUrl { get; set; }
        public string GitHubProfileUrl { get; set; }
        public string FreeTextComment { get; set; }
    }
}
