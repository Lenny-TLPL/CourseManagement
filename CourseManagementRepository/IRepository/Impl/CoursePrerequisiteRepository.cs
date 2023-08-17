using CourseManangementModels.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementRepository.IRepository
{
    public class CoursePrerequisiteRepository : ICoursePrerequisiteRepository
    {
        private readonly CourseManagementContext _context;
        private readonly DbSet<CoursePrerequisite> _dbSet;

        public CoursePrerequisiteRepository()
        {
            _context = new CourseManagementContext();
            _dbSet = _context.Set<CoursePrerequisite>();
        }
        public void Create(CoursePrerequisite entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(CoursePrerequisite entity)
        {
            _dbSet.Remove(entity);
            _context.SaveChanges();
        }

        public IEnumerable<CoursePrerequisite> GetAll()
        {
            return _dbSet.ToList();
        }

        public CoursePrerequisite GetById(int id)
        {
            return _dbSet.SingleOrDefault(attendance => attendance.CourseId == id);
        }

        public void Update(CoursePrerequisite entity)
        {
            CourseManagementContext context = new CourseManagementContext();
            var tracker = context.Attach(entity);
            tracker.State = EntityState.Modified;

            context.SaveChanges();
        }
    }
}
