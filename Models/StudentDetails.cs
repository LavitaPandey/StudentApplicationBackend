using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StudentAppAPI.Models
{
    public class StudentDetails
    {
        [Key]
        public int StudentId { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(50)")]

       
        public string FirstName { get; set; }
        [Required]
        [Column(TypeName = "varchar(50)")]
        public string LastName { get; set; }

        public int? ClassId { get; set; }
        [ForeignKey("ClassId")]
        public virtual ClassDetails classData { get; set; }


    }
}
