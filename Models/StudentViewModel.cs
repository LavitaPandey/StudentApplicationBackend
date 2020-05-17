using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentAppAPI.Models
{
    public class StudentViewModel
    {
        public int stuId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }

        public string className { get; set; }

        public int? classId { get; set; }
       


        public List<SubjectMarksData> subjectMarksDetail { get; set; }
    }
    public class SubjectMarksData
    {
        public int subId { get; set; }
        public float marks { get; set; }
        public string subjectName { get; set; }
    }
}
