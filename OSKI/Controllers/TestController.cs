using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OSKI.Data;
using OSKI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OSKI.Controllers
{
    [Authorize]
    [Route("[controller]s")]
    public class TestController : Controller
    {
        private readonly ApplicationDbContext db;
        public TestController(ApplicationDbContext db)
        {
            this.db = db;
        }


        [Route("")]
        [Route("/")]
        [HttpGet]
        public IActionResult Index()
        {
            var tests = db.Tests.Include(t => t.TestsUsers)
                .Where(w => w.TestsUsers.Any(tu => tu.ApplicationUserId == User.Claims.First().Value))
                .ToList();

            return View(tests);
        }


        [HttpGet("{id:int}")]
        public IActionResult AgreeToTest(int id)
        {
            if (id < 1) return BadRequest();

            var test = db.Tests.Include(t => t.TestsUsers)
                .Where(w => w.TestsUsers.Any(tu => tu.ApplicationUserId == User.Claims.First().Value && tu.TestId == id))
                .FirstOrDefault();

            if (test == null) return NotFound();

            return View(test);
        }


        [HttpPost("{id:int}")]
        public IActionResult AgreeToTest(int id, bool check)
        {
            if (id < 1 || !check) return BadRequest();

            string userId = User.Claims.First().Value;

            var test = db.Tests.Include(t => t.TestsUsers)
                .Where(w => w.TestsUsers.Any(tu => tu.ApplicationUserId == userId && tu.TestId == id))
                .FirstOrDefault();

            if (test == null) return NotFound();


            var testResult = new TestResult()
            {
                StartDate = DateTime.Now,
                ApplicationUserId = userId,
                TestId = id
            };

            db.TestResults.Add(testResult);
            db.SaveChanges();

            return RedirectToAction("", "Questions", new { testResultId = testResult.Id });
        }

    }
}
