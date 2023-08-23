using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CourseManangementModels.Models;
using CourseManagementRepository.IRepository;

namespace CourseManagement.Pages.Teacher
{
    public class ScheduleModel : PageModel
    {
        private readonly ISessionRepository sessionRepository;
        private readonly IStudentInCourseRepository studentInCourseRepository;
        private readonly IAttendanceRepository attendanceRepository;
        private readonly ICourseRepository courseRepository;

        [BindProperty]
        public int Month { get; set; }

        [BindProperty]
        public int Year { get; set; }

        public IDictionary<int, Attendance> Attendances { get; set; } = new Dictionary<int, Attendance>();
        public ScheduleModel(ISessionRepository sessionRepository, IStudentInCourseRepository studentInCourseRepository, IAttendanceRepository attendanceRepository, ICourseRepository courseRepository)
        {
            this.sessionRepository = sessionRepository;
            this.studentInCourseRepository = studentInCourseRepository;
            this.attendanceRepository = attendanceRepository;
            this.Month = DateTime.Now.Month;
            this.Year = DateTime.Now.Year;
            this.courseRepository = courseRepository;
        }
        public List<Session> Session { get;set; } = default!;
        //public IDictionary<DateTime, List<Session>> DateSession { get; set; } = new Dictionary<DateTime, Session>();
        //public IDictionary<DateTime, Session> DateSession { get; set; } = new Dictionary<DateTime, Session>();

        public async Task OnGetAsync()
        {
            Month = DateTime.Now.Month;
            Year = DateTime.Now.Year;
            int TeacherId = HttpContext.Session.GetInt32("UserId").Value;
            //Session = await _context.Sessions
            //.Include(s => s.Course)
            //.Include(s => s.Room)
            //.Include(s => s.Teacher).ToListAsync();
            //var list = studentInCourseRepository.GetAll().Where(a => a.StudentId == StudentId && a.Course.StartDate.Value.Month <= Month && a.Course.EndDate.Value.Month >= Month);
            //var list1 = studentInCourseRepository.GetAll().Where(a => a.Course.StartDate.Value.Month <= Month && a.Course.EndDate.Value.Month >= Month);
            try
            {
                var courses = courseRepository.GetAll().Where(a => a.TeacherId == TeacherId && a.StartDate.Value.Month <= Month && a.EndDate.Value.Month >= Month && a.StartDate.Value.Year == Year).ToList();
                List<int> courseIds = new List<int>();
                for (int i = 0; i < courses.Count; i++)
                {
                    courseIds.Add(courses[i].Id);
                }
                Session = (List<Session>)sessionRepository.GetAll().Where(a => a.Date.Month == Month && courseIds.Contains(a.CourseId)).OrderBy(x => x.StartTime).ToList();
                
            }
            catch (Exception)
            {
                Session = new List<Session>();
            }
            
        }
        public async Task<IActionResult> OnPostAsync()
        {
            
            int TeacherId = HttpContext.Session.GetInt32("UserId").Value;
            //Session = await _context.Sessions
            //.Include(s => s.Course)
            //.Include(s => s.Room)
            //.Include(s => s.Teacher).ToListAsync();
            //var list = studentInCourseRepository.GetAll().Where(a => a.StudentId == StudentId && a.Course.StartDate.Value.Month <= Month && a.Course.EndDate.Value.Month >= Month);
            //var list1 = studentInCourseRepository.GetAll().Where(a => a.Course.StartDate.Value.Month <= Month && a.Course.EndDate.Value.Month >= Month);
            try
            {
                var courses = courseRepository.GetAll().Where(a => a.TeacherId == TeacherId && a.StartDate.Value.Month <= Month && a.EndDate.Value.Month >= Month && a.StartDate.Value.Year == Year).ToList();
                List<int> courseIds = new List<int>();
                for (int i = 0; i < courses.Count; i++)
                {
                    courseIds.Add(courses[i].Id);
                }
                Session = (List<Session>)sessionRepository.GetAll().Where(a => a.Date.Month == Month && courseIds.Contains(a.CourseId)).OrderBy(x => x.StartTime).ToList();

            }
            catch (Exception)
            {
                Session = new List<Session>();
                return Page();
            }
            return Page();
        }
    }
}
