namespace WebApiDay5Lab.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public IRepository<Department> DepartmentRepository { get; }
        public IRepository<Employee> EmployeeRepository { get; }
        public IRepository<Category> CategoriesRepository { get; }
        public IRepository<Product> ProductsRepository { get; }
        public IRepository<Customer> CustomersRepository { get; }
        public IRepository<Order> OrdersRepository { get; }
        public IRepository<OrderItem> OrderItemsRepository { get; }
        public UnitOfWork(AppDbContext context,
                          IRepository<Department> departmentRepository,
                          IRepository<Employee> employeeRepository,
                          IRepository<Category> categoriesRepository,
                          IRepository<Product> productsRepository,
                          IRepository<Customer> customersRepository,
                          IRepository<Order> ordersRepository,
                          IRepository<OrderItem> orderItemsRepository)
        {
            _context = context;
            DepartmentRepository = departmentRepository ?? throw new ArgumentNullException(nameof(departmentRepository));
            EmployeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));

            CategoriesRepository = categoriesRepository ?? throw new ArgumentNullException(nameof(categoriesRepository));
            ProductsRepository = productsRepository ?? throw new ArgumentNullException(nameof(productsRepository));
            CustomersRepository = customersRepository ?? throw new ArgumentNullException(nameof(customersRepository));
            OrdersRepository = ordersRepository ?? throw new ArgumentNullException(nameof(ordersRepository));
            OrderItemsRepository = orderItemsRepository ?? throw new ArgumentNullException(nameof(orderItemsRepository));

            //DepartmentRepository = new GenericRepository<Department>(_context);
            //EmployeeRepository = new GenericRepository<Employee>(_context);
        }
        public int Complete()
        {
            var rows = _context.SaveChanges();
            _context.ChangeTracker.Clear();//.State = EntityState.Detached;
            return rows;
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
