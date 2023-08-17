using CourseManangementModels.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementRepository.IRepository
{
    public class CourseRepository : ICourseRepository
    {
        private readonly CourseManagementContext _context;
        private readonly DbSet<Course> _dbSet;

        public CourseRepository()
        {
            _context = new CourseManagementContext();
            _dbSet = _context.Set<Course>();
        }
        public void Create(Course entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(Course entity)
        {
            _dbSet.Remove(entity);
            _context.SaveChanges();
        }

        public IEnumerable<Course> GetAll()
        {
            return _dbSet.ToList();
        }

        public Course GetById(int id)
        {
            return _dbSet.SingleOrDefault(attendance => attendance.Id == id);
        }

        public void Update(Course entity)
        {
            CourseManagementContext context = new CourseManagementContext();
            var tracker = context.Attach(entity);
            tracker.State = EntityState.Modified;

            context.SaveChanges();
        }
    }
}
