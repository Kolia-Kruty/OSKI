using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OSKI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public List<TestResult> TestResults { get; set; }
        public List<Test> Tests { get; set; }
    }
}
