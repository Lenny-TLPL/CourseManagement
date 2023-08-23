using CourseManangementModels.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementRepository.IRepository
{
    public class SubjectPrerequisiteRepository : ISubjectPrerequisiteRepository
    {
        private readonly CourseManagementContext _context;
        private readonly DbSet<SubjectPrerequisite> _dbSet;

        public SubjectPrerequisiteRepository()
        {
            _context = new CourseManagementContext();
            _dbSet = _context.Set<SubjectPrerequisite>();
        }
        public void Create(SubjectPrerequisite entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(SubjectPrerequisite entity)
        {
            _dbSet.Remove(entity);
            _context.SaveChanges();
        }

        public IEnumerable<SubjectPrerequisite> GetAll()
        {
            return _dbSet.ToList();
        }

        public SubjectPrerequisite GetById(int id)
        {
            return _dbSet.SingleOrDefault(attendance => attendance.SubjectId == id);
        }

        public List<SubjectPrerequisite> GetCoursePrerequisites(int subjectId)
        {
            return _dbSet.Where(course => course.SubjectId == subjectId).ToList();    
        }

        public void Update(SubjectPrerequisite entity)
        {
            CourseManagementContext context = new CourseManagementContext();
            var tracker = context.Attach(entity);
            tracker.State = EntityState.Modified;

            context.SaveChanges();
        }
    }
}
