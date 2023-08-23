using CourseManangementModels.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementRepository.IRepository
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly CourseManagementContext _context;
        private readonly DbSet<Teacher> _dbSet;

        public TeacherRepository()
        {
            _context = new CourseManagementContext();
            _dbSet = _context.Set<Teacher>();
        }
        public void Create(Teacher entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(Teacher entity)
        {
            _dbSet.Remove(entity);
            _context.SaveChanges();
        }

        public IEnumerable<Teacher> GetAll()
        {
            return _dbSet.ToList();
        }

        public Teacher GetById(int id)
        {
            return _dbSet.SingleOrDefault(attendance => attendance.Id == id);
        }

        public Teacher GetTeacherByUserBasicId(int userBasicId)
        {
            return _dbSet.SingleOrDefault(attendance => attendance.UserBasicId == userBasicId);
        }

        public void Update(Teacher entity)
        {
            CourseManagementContext context = new CourseManagementContext();
            var tracker = context.Attach(entity);
            tracker.State = EntityState.Modified;

            context.SaveChanges();
        }
    }
}
