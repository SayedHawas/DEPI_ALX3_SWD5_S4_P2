using Microsoft.EntityFrameworkCore;
using WebApiDay5Lab.Data;
using WebApiDay5Lab.Models;

namespace WebApiDay5Lab.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly AppDbContext _context;
        public DepartmentRepository(AppDbContext context)
        {
            this._context = context;
        }
        //CRUD_Department
        public IEnumerable<Department> GetAll()
        {
            return _context.Departments.ToList();
        }
        public Department GetById(int id)
        {
            return _context.Departments.Find(id);
        }
        public void Add(Department department)
        {
            _context.Departments.Add(department);
            //_context.SaveChanges();
        }
        public void Update(Department department)
        {
            _context.Entry(department).State = EntityState.Modified;
            //_context.SaveChanges();
        }
        public void delete(int id)
        {
            //var departmentDelete = _context.Departments.Find(id);
            //_context.Departments.Remove(departmentDelete);
            _context.Departments.Remove(GetById(id));
            //_context.SaveChanges();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
