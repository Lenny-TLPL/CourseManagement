using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CourseManangementModels.Models;
using CourseManagementRepository.IRepository;

namespace CourseManagement.Pages.Student
{
    public class ScheduleModel : PageModel
    {
        private readonly ISessionRepository sessionRepository;
        private readonly IStudentInCourseRepository studentInCourseRepository;
        private readonly IAttendanceRepository attendanceRepository;

        [BindProperty]
        public int Month { get; set; }

        [BindProperty]
        public int Year { get; set; }

        public IDictionary<int, Attendance> Attendances { get; set; } = new Dictionary<int, Attendance>();
        public ScheduleModel(ISessionRepository sessionRepository, IStudentInCourseRepository studentInCourseRepository, IAttendanceRepository attendanceRepository)
        {
            this.sessionRepository = sessionRepository;
            this.studentInCourseRepository = studentInCourseRepository;
            this.attendanceRepository = attendanceRepository;
            this.Month = DateTime.Now.Month;
            this.Year = DateTime.Now.Year;
        }
        public List<Session> Session { get;set; } = default!;
        //public IDictionary<DateTime, List<Session>> DateSession { get; set; } = new Dictionary<DateTime, Session>();
        //public IDictionary<DateTime, Session> DateSession { get; set; } = new Dictionary<DateTime, Session>();

        public async Task OnGetAsync()
        {
            Month = DateTime.Now.Month;
            Year = DateTime.Now.Year;
            int StudentId = HttpContext.Session.GetInt32("UserId").Value;
            //Session = await _context.Sessions
            //.Include(s => s.Course)
            //.Include(s => s.Room)
            //.Include(s => s.Teacher).ToListAsync();
            //var list = studentInCourseRepository.GetAll().Where(a => a.StudentId == StudentId && a.Course.StartDate.Value.Month <= Month && a.Course.EndDate.Value.Month >= Month);
            //var list1 = studentInCourseRepository.GetAll().Where(a => a.Course.StartDate.Value.Month <= Month && a.Course.EndDate.Value.Month >= Month);
            try
            {
                var studentInCourses = studentInCourseRepository.GetAll().Where(a => a.StudentId == StudentId && a.Course.StartDate.Value.Month <= Month && a.Course.EndDate.Value.Month >= Month && a.Course.StartDate.Value.Year == Year).ToList();
                List<int> courseIds = new List<int>();
                for (int i = 0; i < studentInCourses.Count; i++)
                {
                    courseIds.Add(studentInCourses[i].CourseId);
                }
                Session = (List<Session>)sessionRepository.GetAll().Where(a => a.Date.Month == Month && courseIds.Contains(a.CourseId)).OrderBy(x => x.StartTime).ToList();
                for (int i = 0; i< Session.Count; i++)
                {
                    var attendance = attendanceRepository.GetAttendancesOfAStudentInSession(StudentId, Session[i].Id);
                    if(attendance != null)
                    Attendances.Add(Session[i].Id, attendance) ;
                }
            }
            catch (Exception)
            {
                Session = new List<Session>();
            }
            
        }
        public async Task<IActionResult> OnPostAsync()
        {
            
            int StudentId = HttpContext.Session.GetInt32("UserId").Value;
            //Session = await _context.Sessions
            //.Include(s => s.Course)
            //.Include(s => s.Room)
            //.Include(s => s.Teacher).ToListAsync();
            //var list = studentInCourseRepository.GetAll().Where(a => a.StudentId == StudentId && a.Course.StartDate.Value.Month <= Month && a.Course.EndDate.Value.Month >= Month);
            //var list1 = studentInCourseRepository.GetAll().Where(a => a.Course.StartDate.Value.Month <= Month && a.Course.EndDate.Value.Month >= Month);
            try
            {
                var studentInCourses = studentInCourseRepository.GetAll().Where(a => a.StudentId == StudentId && a.Course.StartDate.Value.Month <= Month && a.Course.EndDate.Value.Month >= Month && a.Course.StartDate.Value.Year == Year).ToList();
                List<int> courseIds = new List<int>();
                for (int i = 0; i < studentInCourses.Count; i++)
                {
                    courseIds.Add(studentInCourses[i].CourseId);
                }
                Session = (List<Session>)sessionRepository.GetAll().Where(a => a.Date.Month == Month && courseIds.Contains(a.CourseId)).OrderBy(x => x.StartTime).ToList();
                //Session = Session.OrderBy(x => x.StartTime).ToList();
                for (int i = 0; i< Session.Count; i++)
                {
                    var attendance = attendanceRepository.GetAttendancesOfAStudentInSession(StudentId, Session[i].Id);
                    if (attendance != null)
                        Attendances.Add(Session[i].Id, attendance);
                }
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
