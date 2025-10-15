using WebApiDay5Lab.Models;

namespace WebApiDay5Lab.Repository
{
    public interface IDepartmentRepository
    {
        IEnumerable<Department> GetAll();
        Department GetById(int id);
        void Add(Department department);
        void Update(Department department);
        void delete(int id);
        void Save();
    }
}
