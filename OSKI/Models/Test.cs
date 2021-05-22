using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OSKI.Models
{
    public class Test
    {
        [Key]
        public int Id { get; set; }


        [StringLength (50)]
        [Required]
        public string Title { get; set; }
        [StringLength (450)]
        [Required]
        public string Description { get; set; }


        public List<Question> Questions { get; set; }
        public List<TestResult> TestResults { get; set; }
    }
}
