using CourseManangementModels.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementRepository.IRepository
{
    public class SubjectRepository : ISubjectRepository 
    {
        private readonly CourseManagementContext _context;
        private readonly DbSet<Subject> _dbSet;

        public SubjectRepository()
        {
            _context = new CourseManagementContext();
            _dbSet = _context.Set<Subject>();
        }
        public void Create(Subject entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(Subject entity)
        {
            _dbSet.Remove(entity);
            _context.SaveChanges();
        }

        public IEnumerable<Subject> GetAll()
        {
            return _dbSet.ToList();
        }

        public Subject GetById(int id)
        {
            return _dbSet.SingleOrDefault(attendance => attendance.Id == id);
        }

        public void Update(Subject entity)
        {
            CourseManagementContext context = new CourseManagementContext();
            var tracker = context.Attach(entity);
            tracker.State = EntityState.Modified;

            context.SaveChanges();
        }
    }
}
