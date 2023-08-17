using CourseManangementModels.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementRepository.IRepository
{
    public class SessionRepository : ISessionRepository
    {
        private readonly CourseManagementContext _context;
        private readonly DbSet<Session> _dbSet;

        public SessionRepository()
        {
            _context = new CourseManagementContext();
            _dbSet = _context.Set<Session>();
        }
        public void Create(Session entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(Session entity)
        {
            _dbSet.Remove(entity);
            _context.SaveChanges();
        }

        public IEnumerable<Session> GetAll()
        {
            return _dbSet.ToList();
        }

        public Session GetById(int id)
        {
            return _dbSet.SingleOrDefault(attendance => attendance.Id == id);
        }

        public void Update(Session entity)
        {
            CourseManagementContext context = new CourseManagementContext();
            var tracker = context.Attach(entity);
            tracker.State = EntityState.Modified;

            context.SaveChanges();
        }
    }
}
