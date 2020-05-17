using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentAppAPI.Models
{
    public class StudentDetailsContext : DbContext
    {
        public StudentDetailsContext(DbContextOptions<StudentDetailsContext> options) : base(options)
        {

        }
        public DbSet<StudentDetails> StudentDetails { get; set; }
        public DbSet<ClassDetails> ClassDetails { get; set; }
        public DbSet<SubjectDetails> SubjectDetails { get; set; }
        public DbSet<StuSubjectMarks> StuSubjectMarks { get; set; }


    }
}
