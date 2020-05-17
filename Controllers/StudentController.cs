using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentAppAPI.Models;

namespace StudentAppAPI.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly StudentDetailsContext _context;

        public StudentController(StudentDetailsContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentViewModel>>> GetStudentDetails()
        {
            List<StudentViewModel> result = await (from s in _context.StudentDetails
                                                   join c in _context.ClassDetails on s.ClassId equals c.ClassId
                                                   select new StudentViewModel
                                                   {
                                                       stuId = s.StudentId,
                                                       firstName = s.FirstName,
                                                       lastName = s.LastName,
                                                       className = c.ClassName,
                                                       classId= c.ClassId


                                                   }).ToListAsync();

            foreach (var res in result)
            {
                var StudentId = res.stuId;
                res.subjectMarksDetail = await (from sub in _context.SubjectDetails
                                                join m in _context.StuSubjectMarks on sub.SubjectId equals m.SubjectId
                                                where m.StudentId == StudentId
                                                select new SubjectMarksData
                                                {   subId= sub.SubjectId,
                                                    subjectName = sub.Subject,
                                                    marks = m.Marks

                                                }).ToListAsync();

            }

            return result;

        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClassDetails>>> GetAllClass()
        {
            return await _context.ClassDetails.ToListAsync();
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubjectDetails>>> GetAllSubject()
        {
            return await _context.SubjectDetails.ToListAsync();
        }


        [HttpPost]
        public async Task<ActionResult<StudentViewModel>> PostStudentDetails(StudentViewModel StudentData)
        {
            StudentDetails stu = new StudentDetails();
            stu.FirstName = StudentData.firstName;
            stu.LastName = StudentData.lastName;
            stu.ClassId = StudentData.classId;
            _context.StudentDetails.Add(stu);
            await _context.SaveChangesAsync();

            StudentDetails student = _context.StudentDetails.SingleOrDefault(x => x.FirstName == StudentData.firstName);//Need to make FirstName as unique key. yet to code for making it unique.
            foreach (var stuData in StudentData.subjectMarksDetail)
            {
                StuSubjectMarks subMarks = new StuSubjectMarks();
                subMarks.SubjectId = stuData.subId;
                subMarks.StudentId = student.StudentId;
                subMarks.Marks = stuData.marks;
                _context.StuSubjectMarks.Add(subMarks);
            }


            
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStudentDetail", new { id = StudentData.stuId }, StudentData);
        }
        public async Task<ActionResult<StudentViewModel>> GetStudentDetail(int id)
        {
            var studentDetail = await _context.StudentDetails.FindAsync(id);
            if (studentDetail != null) {
                StudentViewModel stuData = new StudentViewModel();
                stuData.stuId = id;
                stuData.firstName = studentDetail.FirstName;
                stuData.lastName = studentDetail.LastName;
                stuData.classId = studentDetail.ClassId;
                stuData.subjectMarksDetail = await (from sub in _context.SubjectDetails
                                                    join m in _context.StuSubjectMarks on sub.SubjectId equals m.SubjectId
                                                    where m.StudentId == studentDetail.StudentId
                                                    select new SubjectMarksData
                                                    {
                                                        subjectName = sub.Subject,
                                                        marks = m.Marks

                                                    }).ToListAsync();


                return stuData;

            }

           else
            {
                return NotFound();
            }

           

        }
       
       [HttpPut("{id}")]
        public async Task<IActionResult> PutStudentDetails(int id, StudentViewModel studentData)
        {
            if (id != studentData.stuId)
            {
                return BadRequest();
            }
           
           
            foreach (var data in studentData.subjectMarksDetail)
            {
                bool ifExist = await _context.StuSubjectMarks.AnyAsync(x=>x.SubjectId == data.subId);
                if (!ifExist) {
                    StuSubjectMarks subMarkData = new StuSubjectMarks();
                    subMarkData.StudentId = id;
                    subMarkData.SubjectId = data.subId;
                    subMarkData.Marks = data.marks;
                    _context.StuSubjectMarks.Add(subMarkData);
                }
                else
                {
                    StuSubjectMarks subMarks = await _context.StuSubjectMarks.FirstOrDefaultAsync(x => x.SubjectId == data.subId);
                    subMarks.SubjectId = data.subId;
                    subMarks.Marks = data.marks;
                }
               
            
                
                await _context.SaveChangesAsync();
            }
            
            var studentDetail = await _context.StudentDetails.FindAsync(id);
            studentDetail.FirstName = studentData.firstName;
            studentDetail.LastName = studentData.lastName;
            studentDetail.ClassId = studentData.classId;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentDetailExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<StudentViewModel>> DeleteStudentDetails(int id)
        {
            var subMarksDetails= _context.StuSubjectMarks.Where(x => x.StudentId == id);
           
            foreach (var data in subMarksDetails)
            {
                var subMarks =  _context.StuSubjectMarks.FirstOrDefault(x => x.StudentId == data.StudentId);
                _context.StuSubjectMarks.Remove(subMarks);
            }
             _context.SaveChanges();

            var stuDetails =  _context.StudentDetails.Find(id);
            if (stuDetails == null)
            {
                return NotFound();
            }
            _context.StudentDetails.Remove(stuDetails);
             _context.SaveChanges();

            return Ok();
        }
        private bool StudentDetailExists(int id)
        {
            return _context.StudentDetails.Any(e => e.StudentId == id);
        }
    }
}