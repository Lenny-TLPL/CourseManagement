using CourseManangementModels.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementRepository.IRepository
{
    public class SubjectTeachRepository : ISubjectTeachRepository
    {
        private readonly CourseManagementContext _context;
        private readonly DbSet<SubjectTeach> _dbSet;

        public SubjectTeachRepository()
        {
            _context = new CourseManagementContext();
            _dbSet = _context.Set<SubjectTeach>();
        }
        public void Create(SubjectTeach entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(SubjectTeach entity)
        {
            _dbSet.Remove(entity);
            _context.SaveChanges();
        }

        public IEnumerable<SubjectTeach> GetAll()
        {
            return _dbSet.ToList();
        }

        public SubjectTeach GetById(int id)
        {
            return _dbSet.SingleOrDefault(attendance => attendance.SubjectId == id);
        }

        public void Update(SubjectTeach entity)
        {
            CourseManagementContext context = new CourseManagementContext();
            var tracker = context.Attach(entity);
            tracker.State = EntityState.Modified;

            context.SaveChanges();
        }
    }
}
