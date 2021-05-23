using OSKI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OSKI.ViewModels
{
    public class QuestionViewModel
    {
        public int Count { get; set; }
        public int QuestionIndex { get; set; }
        public Question Question { get; set; }
        public int TestResultId { get; set; }
    }
}
