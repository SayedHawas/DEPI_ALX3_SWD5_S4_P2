namespace WebApiDay5Lab.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Department> DepartmentRepository { get; }
        IRepository<Employee> EmployeeRepository { get; }
        IRepository<Category> CategoriesRepository { get; }
        IRepository<Product> ProductsRepository { get; }
        IRepository<Customer> CustomersRepository { get; }
        IRepository<Order> OrdersRepository { get; }
        IRepository<OrderItem> OrderItemsRepository { get; }
        int Complete();
    }
}
