using CourseManangementModels.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementRepository.IRepository
{
    public class MajorRepository : IMajorRepository
    {
        private readonly CourseManagementContext _context;
        private readonly DbSet<Major> _dbSet;

        public MajorRepository()
        {
            _context = new CourseManagementContext();
            _dbSet = _context.Set<Major>();
        }
        public void Create(Major entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(Major entity)
        {
            _dbSet.Remove(entity);
            _context.SaveChanges();
        }

        public IEnumerable<Major> GetAll()
        {
            return _dbSet.ToList();
        }

        public Major GetById(int id)
        {
            return _dbSet.SingleOrDefault(attendance => attendance.Id == id);
        }

        public void Update(Major entity)
        {
            CourseManagementContext context = new CourseManagementContext();
            var tracker = context.Attach(entity);
            tracker.State = EntityState.Modified;

            context.SaveChanges();
        }
    }
}
