using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OSKI.Models
{
    public class Question
    {
        [Key]
        public int Id { get; set; }


        [StringLength (250)]
        [Required]
        public string Text { get; set; }


        public List<Answer> Answers { get;  set; }


        public int TestId { get; set; }
        [ForeignKey(nameof(TestId))]
        public Test Test { get; set; }
    }
}
