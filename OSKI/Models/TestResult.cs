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
    public class TestResult
    {
        [Key]   
        public int Id { get; set; }


        [Range(0.00, 100.00)]
        public double? Rating { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime StartDate { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? FinishDate { get; set; }


        public List<Answer> Answers { get; set; }


        public int? TestId { get; set; }
        [ForeignKey(nameof(TestId))]
        public Test Test { get; set; }

        public string ApplicationUserId { get; set; }
        //[ForeignKey(nameof(ApplicationUserId))]
        public ApplicationUser ApplicationUser { get; set; }
    }
}
