using OSKI.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OSKI.Models
{
    public class Answer
    {
        [Key]
        public int Id { get; set; }


        [Required]
        [StringLength (250)]
        public string Text { get; set; }

        [DefaultValue(false)]
        public bool RightAnswer { get; set; }


        public List<TestResult> TestResults { get; set; }


        public int QuestionId { get; set; }
        [ForeignKey(nameof(QuestionId))]
        public Question Question { get; set; }
    }
}
