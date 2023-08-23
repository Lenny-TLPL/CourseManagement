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
    public class CheckStudentAttendanceModel : PageModel
    {
        private readonly IAttendanceRepository attendanceRepository;
        private readonly ICourseRepository courseRepository;
        private readonly ISessionRepository sessionRepository;
        private readonly IStudentInCourseRepository studentInCourseRepository;

        public CheckStudentAttendanceModel(IAttendanceRepository attendanceRepository, ICourseRepository courseRepository, ISessionRepository sessionRepository, IStudentInCourseRepository studentInCourseRepository)
        {
            this.attendanceRepository = attendanceRepository;
            this.courseRepository = courseRepository;
            this.sessionRepository = sessionRepository;
            this.studentInCourseRepository = studentInCourseRepository;
        }

        public IList<Attendance> Attendances { get;set; } = default!;

        public async Task OnGetAsync(int sessionId)
        {
            Attendances = attendanceRepository.GetAttendancesBySessionId(sessionId); 
            
            if(Attendances.Count <= 0)
            {
                var session = sessionRepository.GetById(sessionId);
                var studentInCourse = studentInCourseRepository.GetAll().Where(a => a.CourseId == session.CourseId).ToList();
                for(int i = 0; i < studentInCourse.Count; i++)
                {
                    Attendance attendance = new Attendance()
                    {
                        SessionId = sessionId,
                        StudentId = studentInCourse[i].StudentId,
                        AttendanceCheck = false
                    };
                    attendanceRepository.Create(attendance);
                }
                Attendances = attendanceRepository.GetAttendancesBySessionId(sessionId);
                
            }
            
        }
        public async Task OnPostAsync()
        {
            int studentId = int.Parse(Request.Form["studentId"]);
            int sessionId = int.Parse(Request.Form["sessionId"]);
            string action = Request.Form["action"];
            var attendance = attendanceRepository.GetAttendancesOfAStudentInSession(studentId, sessionId);
            switch (action)
            {
                case "Absence":
                    attendance.AttendanceCheck = false;
                    break;
                case "Attended":
                    attendance.AttendanceCheck = true;
                    break;
            }
            attendanceRepository.Update(attendance);
            Attendances = attendanceRepository.GetAttendancesBySessionId(sessionId);

            //if (Attendances.Count <= 0)
            //{
            //    var session = sessionRepository.GetById(sessionId);
            //    var studentInCourse = studentInCourseRepository.GetAll().Where(a => a.CourseId == session.CourseId).ToList();
            //    for (int i = 0; i < studentInCourse.Count; i++)
            //    {
            //        Attendance attendance = new Attendance()
            //        {
            //            SessionId = sessionId,
            //            StudentId = studentInCourse[i].StudentId,
            //            AttendanceCheck = false
            //        };
            //        attendanceRepository.Create(attendance);
            //    }
            //    Attendances = attendanceRepository.GetAttendancesBySessionId(sessionId);

            //}

        }
    }
}
