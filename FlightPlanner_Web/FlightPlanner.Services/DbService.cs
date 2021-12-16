using System.Linq;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Date;
using Microsoft.EntityFrameworkCore;

namespace FlightPlanner.Services
{
    public class DbService : IDbService
    {
        protected readonly FlightPlannerDbContext _context;

        public DbService(FlightPlannerDbContext context)
        {
            _context = context;
        }

        public IQueryable<T> Query<T>() where T : Entity
        {
            return _context.Set<T>();
        }

        public T GetById<T>(int Id) where T : Entity
        {
            return _context.Set<T>().SingleOrDefault(e => e.Id == Id);
        }

        public void Create<T>(T entity) where T : Entity
        {
             _context.Set<T>().Add(entity);
             _context.SaveChanges();

        }

        public void Update<T>(T entity) where T : Entity
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete<T>(T entity) where T : Entity
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }
    }
}
