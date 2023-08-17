using CourseManangementModels.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementRepository.IRepository.Impl
{
    public class UserBasicRepository : IUserBasicRepository
    {
        private readonly CourseManagementContext _context;
        private readonly DbSet<UserBasic> _dbSet;

        public UserBasicRepository()
        {
            _context = new CourseManagementContext();
            _dbSet = _context.Set<UserBasic>();
        }
        public void Create(UserBasic entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(UserBasic entity)
        {
            _dbSet.Remove(entity);
            _context.SaveChanges();
        }

        public IEnumerable<UserBasic> GetAll()
        {
            return _dbSet.ToList();
        }

        public UserBasic GetById(int id)
        {
            return _dbSet.SingleOrDefault(attendance => attendance.Id == id);
        }

        public void Update(UserBasic entity)
        {
            CourseManagementContext context = new CourseManagementContext();
            var tracker = context.Attach(entity);
            tracker.State = EntityState.Modified;

            context.SaveChanges();
        }
    }
}
