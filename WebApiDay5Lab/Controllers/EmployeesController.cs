using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiDay5Lab.Data;
using WebApiDay5Lab.DTOs.EmployeeDtos;
using WebApiDay5Lab.Models;

namespace WebApiDay5Lab.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EmployeesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Departments
        [HttpGet("Pagination")]
        public async Task<ActionResult<IEnumerable<GetEmployeeDto>>> GetWithPagination([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var employees = await _context.Employees.Include("Departments").ToListAsync();
            List<GetEmployeeDto> result = new List<GetEmployeeDto>();
            foreach (var item in employees)
            {
                result.Add(new GetEmployeeDto()
                {
                    Name = item.Name,
                    Job = item.Job,
                    Salary = item.Salary.ToString("C"),
                    DepartmentName = item.Department.Name
                });
            }
            //Pagination
            var totalCount = employees.Count();
            var totalPage = (int)Math.Ceiling((double)totalCount / pageSize);
            result = result.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            return Ok(result);
        }
        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetEmployeeDto>>> GetEmployees()
        {
            var emps = await _context.Employees.Include("Department").ToListAsync();
            List<GetEmployeeDto> result = new List<GetEmployeeDto>();
            foreach (var item in emps)
            {
                GetEmployeeDto emp = new GetEmployeeDto()
                {
                    Name = item.Name,
                    Job = item.Job,
                    Salary = item.Salary.ToString("C"),
                    DepartmentName = item.Department.Name
                };
                result.Add(emp);
            }
            return Ok(result);
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetEmployeeDto>> GetEmployee(int id)
        {
            //var employee = await _context.Employees.FindAsync(id);
            var employee = await _context.Employees.Include("Department").FirstOrDefaultAsync(e => e.Id == id);
            if (employee == null)
            {
                return NotFound();
            }
            GetEmployeeDto result = new GetEmployeeDto()
            {
                Name = employee.Name,
                Job = employee.Job,
                Salary = employee.Salary.ToString("C"),
                DepartmentName = employee.Department.Name
            };
            return Ok(result);
        }

        // PUT: api/Employees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        //[Consumes("text/xml")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> PutEmployee(int id, PutEmployeeDto employee)
        {
            if (id != employee.Id)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid) return BadRequest(employee);
            var oldemployee = await _context.Employees.FindAsync(id);
            oldemployee.Name = employee.Name;
            oldemployee.Job = employee.Job;
            oldemployee.Salary = employee.Salary;
            oldemployee.departmentId = employee.departmentId;
            //_context.Entry(employee).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        // POST: api/Employees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PostEmployeeDto>> PostEmployee(PostEmployeeDto employee)
        {
            if (!ModelState.IsValid) return BadRequest(employee);
            Employee newEmployee = new Employee()
            {
                Name = employee.Name,
                Job = employee.Job,
                Salary = employee.Salary,
                departmentId = employee.departmentId
            };
            _context.Employees.Add(newEmployee);
            await _context.SaveChangesAsync();
            int newId = _context.Employees.Max(e => e.Id);
            return CreatedAtAction("GetEmployee", new { id = newId }, employee);
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }
    }
}
