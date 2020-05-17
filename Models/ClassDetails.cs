using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StudentAppAPI.Models
{
    public class ClassDetails
    {
        [Key]
        public int ClassId { get; set; }
       
        [Column(TypeName = "nvarchar(50)")]
        public string ClassName { get; set; }
    }
}
