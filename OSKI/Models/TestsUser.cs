using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OSKI.Models
{
    public class TestsUser
    {
        [Key]
        public int Id { get; set; }


        public string ApplicationUserId { get; set; }
        [ForeignKey(nameof(ApplicationUserId))]
        public ApplicationUser ApplicationUser { get; set; }

        public int TestId { get; set; }
        [ForeignKey(nameof(TestId))]
        public Test Test { get; set; }
    }
}
