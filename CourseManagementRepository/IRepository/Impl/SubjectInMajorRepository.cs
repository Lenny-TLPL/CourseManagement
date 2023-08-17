using CourseManangementModels.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementRepository.IRepository
{
    public class SubjectInMajorRepository : ISubjectInMajorRepository
    {
        private readonly CourseManagementContext _context;
        private readonly DbSet<SubjectInMajor> _dbSet;

        public SubjectInMajorRepository()
        {
            _context = new CourseManagementContext();
            _dbSet = _context.Set<SubjectInMajor>();
        }
        public void Create(SubjectInMajor entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(SubjectInMajor entity)
        {
            _dbSet.Remove(entity);
            _context.SaveChanges();
        }

        public IEnumerable<SubjectInMajor> GetAll()
        {
            return _dbSet.ToList();
        }

        public SubjectInMajor GetById(int id)
        {
            return _dbSet.SingleOrDefault(attendance => attendance.MajorId == id);
        }

        public void Update(SubjectInMajor entity)
        {
            CourseManagementContext context = new CourseManagementContext();
            var tracker = context.Attach(entity);
            tracker.State = EntityState.Modified;

            context.SaveChanges();
        }
    }
}
