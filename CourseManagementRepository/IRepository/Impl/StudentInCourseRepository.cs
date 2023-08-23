using CourseManangementModels.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementRepository.IRepository
{
    public class StudentInCourseRepository : IStudentInCourseRepository
    {
        private readonly CourseManagementContext _context;
        private readonly DbSet<StudentInCourse> _dbSet;

        public StudentInCourseRepository()
        {
            _context = new CourseManagementContext();
            _dbSet = _context.Set<StudentInCourse>();
        }
        public void Create(StudentInCourse entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(StudentInCourse entity)
        {
            _dbSet.Remove(entity);
            _context.SaveChanges();
        }

        public IEnumerable<StudentInCourse> GetAll()
        {
            return _dbSet
                .Include(a => a.Course)
                .Include(a => a.Student)
                .ToList();
        }

        public StudentInCourse GetById(int id)
        {
            return _dbSet.SingleOrDefault(attendance => attendance.StudentId == id);
        }

        public void Update(StudentInCourse entity)
        {
            CourseManagementContext context = new CourseManagementContext();
            var tracker = context.Attach(entity);
            tracker.State = EntityState.Modified;

            context.SaveChanges();
        }
    }
}
