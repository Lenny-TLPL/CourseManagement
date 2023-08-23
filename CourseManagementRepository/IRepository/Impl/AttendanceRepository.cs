using CourseManangementModels.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementRepository.IRepository.Impl
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly CourseManagementContext _context;
        private readonly DbSet<Attendance> _dbSet;

        public AttendanceRepository()
        {
            _context = new CourseManagementContext();
            _dbSet = _context.Set<Attendance>();
        }
        public void Create(Attendance entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(Attendance entity)
        {
            _dbSet.Remove(entity);
            _context.SaveChanges();
        }

        public IEnumerable<Attendance> GetAll()
        {
            return _dbSet
                .Include(a => a.Student)
                .Include(a => a.Session)
                .ToList();
        }

        public List<Attendance> GetAttendancesBySessionId(int sessionId)
        {
            return _dbSet
                .Include(a => a.Student)
                .Include(a => a.Session)
                .Where(attendance => attendance.SessionId == sessionId).ToList();
        }

        public List<Attendance> GetAttendancesByStudentIdInCourse(int studentId, int courseId)
        {
            return _dbSet
                .Include(a => a.Student)
                .Include(a => a.Session)
                .Where(attendance => attendance.StudentId == studentId && attendance.Session.CourseId == courseId).ToList();
        }

        public Attendance GetAttendancesOfAStudentInSession(int studentId, int sessionId)
        {
            return _dbSet
                .Include(a => a.Student)
                .Include(a => a.Session)
                .SingleOrDefault(attendance => attendance.StudentId == studentId && attendance.SessionId == sessionId);
        }

        public Attendance GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Attendance entity)
        {
            CourseManagementContext context = new CourseManagementContext();
            var tracker = context.Attach(entity);
            tracker.State = EntityState.Modified;

            context.SaveChanges();
        }
    }
}
