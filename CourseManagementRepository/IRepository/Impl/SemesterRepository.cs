using CourseManangementModels.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementRepository.IRepository
{
    public class SemesterRepository :ISemesterRepository
    {
        private readonly CourseManagementContext _context;
        private readonly DbSet<Semester> _dbSet;

        public SemesterRepository()
        {
            _context = new CourseManagementContext();
            _dbSet = _context.Set<Semester>();
        }
        public void Create(Semester entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(Semester entity)
        {
            _dbSet.Remove(entity);
            _context.SaveChanges();
        }

        public IEnumerable<Semester> GetAll()
        {
            return _dbSet.ToList();
        }

        public Semester GetById(int id)
        {
            return _dbSet.SingleOrDefault(attendance => attendance.Id == id);
        }

        public void Update(Semester entity)
        {
            CourseManagementContext context = new CourseManagementContext();
            var tracker = context.Attach(entity);
            tracker.State = EntityState.Modified;

            context.SaveChanges();
        }
    }
}
