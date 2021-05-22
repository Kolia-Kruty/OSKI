using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OSKI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OSKI.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class ResultController : Controller
    {
        private readonly ApplicationDbContext db;

        public ResultController(ApplicationDbContext db)
        {
            this.db = db;
        }

        [HttpGet("{id:int}")]        
        public IActionResult Index(int id)
        {
            if (id < 1) return BadRequest();

            var testResult = db.TestResults
                .Where(w=>w.Id == id && w.ApplicationUserId == User.Claims.First().Value).FirstOrDefault();

            if (testResult == null) return BadRequest();

            return View(testResult);
        }
    }
}
