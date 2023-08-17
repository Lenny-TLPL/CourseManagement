using CourseManangementModels.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementRepository.IRepository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly CourseManagementContext _context;
        private readonly DbSet<Student> _dbSet;

        public StudentRepository()
        {
            _context = new CourseManagementContext();
            _dbSet = _context.Set<Student>();
        }
        public void Create(Student entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(Student entity)
        {
            _dbSet.Remove(entity);
            _context.SaveChanges();
        }

        public IEnumerable<Student> GetAll()
        {
            return _dbSet.ToList();
        }

        public Student GetById(int id)
        {
            return _dbSet.SingleOrDefault(attendance => attendance.Id == id);
        }

        public void Update(Student entity)
        {
            CourseManagementContext context = new CourseManagementContext();
            var tracker = context.Attach(entity);
            tracker.State = EntityState.Modified;

            context.SaveChanges();
        }
    }
}
