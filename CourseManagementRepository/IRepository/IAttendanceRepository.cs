using CourseManangementModels.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementRepository.IRepository
{
    public interface IAttendanceRepository : IRepositoryBase<Attendance>
    {
        public List<Attendance> GetAttendancesBySessionId(int sessionId);
        public List<Attendance> GetAttendancesByStudentIdInCourse(int studentId, int courseId);
        public Attendance GetAttendancesOfAStudentInSession(int studentId, int sessionId);
    }
}
