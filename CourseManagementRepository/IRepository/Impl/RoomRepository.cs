using CourseManangementModels.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementRepository.IRepository
{
    public class RoomRepository : IRoomRepository
    {
        private readonly CourseManagementContext _context;
        private readonly DbSet<Room> _dbSet;

        public RoomRepository()
        {
            _context = new CourseManagementContext();
            _dbSet = _context.Set<Room>();
        }
        public void Create(Room entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(Room entity)
        {
            _dbSet.Remove(entity);
            _context.SaveChanges();
        }

        public IEnumerable<Room> GetAll()
        {
            return _dbSet.ToList();
        }

        public Room GetById(int id)
        {
            return _dbSet.SingleOrDefault(attendance => attendance.Id == id);
        }

        public void Update(Room entity)
        {
            CourseManagementContext context = new CourseManagementContext();
            var tracker = context.Attach(entity);
            tracker.State = EntityState.Modified;

            context.SaveChanges();
        }
    }
}
