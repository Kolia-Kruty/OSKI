using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OSKI.Data;
using OSKI.Models;
using OSKI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OSKI.Controllers
{
    [Authorize]
    [Route("[controller]s")]
    public class QuestionController : Controller
    {
        private readonly ApplicationDbContext db;

        public QuestionController(ApplicationDbContext db)
        {
            this.db = db;
        }


        [Route("")]
        public IActionResult Index(int testResultId, int questionIndex = 0, int? answerId = null)
        {
            if (testResultId < 1 || questionIndex < 0) return BadRequest();

            var testResult = db.TestResults.Include(a => a.Answers)
                .Where(tr => tr.Id == testResultId && tr.ApplicationUserId == User.Claims.First().Value)
                .FirstOrDefault();

            var questions = db.Questions.Include(a => a.Answers)
                .Where(q => q.TestId == testResult.TestId).ToList();

            if (testResult == null || questions == null) return NotFound();

            var question = questions.Skip(questionIndex).Take(1).FirstOrDefault();

            if (questionIndex > 0 && answerId > 0)
            {
                var answer = questions[questionIndex - 1].Answers.Where(a => a.Id == answerId).FirstOrDefault();

                if (answer != null)
                {
                    this.SaveAnswerUser(testResult, answer);
                }
            }


            return View(new QuestionViewModel()
            {
                Count = questions.Count(),
                TestResultId = testResultId,
                Question = question,
                QuestionIndex = questionIndex + 1
            }); ;
        }


        [Route("[action]")]
        [HttpPost]
        public IActionResult Finish(int testResultId, int questionIndex = 0, int? answerId = null)
        {
            if (testResultId < 1 || questionIndex < 0) return BadRequest();

            var testResult = db.TestResults.Include(a => a.Answers).ThenInclude(q => q.Question)
                .Where(tr => tr.Id == testResultId && tr.ApplicationUserId == User.Claims.First().Value)
                .FirstOrDefault();

            var questions = db.Questions.Include(a => a.Answers)
                .Where(q => q.TestId == testResult.TestId).ToList();

            if (testResult == null || questions == null) return NotFound();

            if (questionIndex > 0 && answerId > 0)
            {
                var answer = questions[questionIndex - 1].Answers.Where(a => a.Id == answerId).FirstOrDefault();

                if (answer != null)
                {
                    this.SaveAnswerUser(testResult, answer);
                }
            }

            if (ProcessingAnswers(testResult, questions.Count()))
            {
                return RedirectToAction("Index", "Result", new { id = testResult.Id });
            }
            return NotFound();

        }

        private void SaveAnswerUser(TestResult testResult, Answer answer)
        {
            bool savedOldAnswer = false;
            foreach (var answ in answer.Question.Answers.ToList())
            {
                if (testResult.Answers.Any(a => a.Id == answ.Id))
                {
                    savedOldAnswer = true;
                }
            }

            if (!savedOldAnswer)
            {
                testResult.Answers.Add(answer);
                db.SaveChanges();
            }
        }

        private bool ProcessingAnswers(TestResult testResult, int countQuestion)
        {
            if (testResult.Answers.Count() > 0 && countQuestion > 0)
            {
                var rightAnswers = testResult.Answers.Where(t => t.RightAnswer).ToList();

                testResult.FinishDate = DateTime.Now;
                testResult.Rating = Math.Round((rightAnswers.Count() / (double)countQuestion) * 100, 2);

                var x = db.Update(testResult);
                db.SaveChanges();

                return true;
            }
            return true;
        }
    }
}
