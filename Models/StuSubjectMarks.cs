using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StudentAppAPI.Models
{
    public class StuSubjectMarks
    {
        [Key]
        public int SubMarksId { get; set; }
        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public float Marks { get; set; }
        public int? StudentId { get; set; }
        [ForeignKey("StudentId")]
        public virtual StudentDetails Student { get; set; }
        public int? SubjectId { get; set; }
        [ForeignKey("SubjectId")]
        public virtual SubjectDetails Subject { get; set; }

    }
}
