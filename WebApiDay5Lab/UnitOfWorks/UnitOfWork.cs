namespace WebApiDay5Lab.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public IRepository<Department> DepartmentRepository { get; }
        public IRepository<Employee> EmployeeRepository { get; }
        public UnitOfWork(AppDbContext context,
                          IRepository<Department> departmentRepository,
                          IRepository<Employee> employeeRepository)
        {
            _context = context;
            DepartmentRepository = departmentRepository ?? throw new ArgumentNullException(nameof(departmentRepository));
            EmployeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
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
