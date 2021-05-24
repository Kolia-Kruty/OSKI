using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OSKI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public List<TestResult> TestResults { get; set; }


        public List<TestsUser> TestsUsers { get; set; }
    }
}
