namespace WebApiDay5Lab.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Department> DepartmentRepository { get; }
        IRepository<Employee> EmployeeRepository { get; }
        //IRepository<Orders> OrdersRepository { get; }
        int Complete();
    }
}
