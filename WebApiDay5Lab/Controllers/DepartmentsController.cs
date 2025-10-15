using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiDay5Lab.DTOs.DepartmentDtos;
using WebApiDay5Lab.Services.Interface;
namespace WebApiDay5Lab.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        //private readonly AppDbContext _context;
        //private readonly IDepartmentRepository _repos;
        //public DepartmentsController(AppDbContext context, IDepartmentRepository repos)
        //{
        //    _context = context;
        //    //_repos = new DepartmentRepository(_context);
        //    _repos = repos;
        //}

        private readonly IServiceDepartment _service;
        public DepartmentsController(IServiceDepartment service)
        {
            this._service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetDepartmentDto>>> GetDepartments()
        {
            //var departments = _repos.GetAll(); //await _context.Departments.ToListAsync();
            //var departments = _service.GetDepartments();
            //List<GetDepartmentDto> result = new List<GetDepartmentDto>();
            //foreach (var item in departments)
            //{
            //    result.Add(new GetDepartmentDto()
            //    {
            //        DepartmentId = item.DepartmentId,
            //        Name = item.Name,
            //        Description = item.Description
            //    });
            //}
            return Ok(_service.GetDepartments());
        }
        // GET: api/Departments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetDepartmentDto>> GetDepartment(int id)
        {
            //var department = _repos.GetById(id); // await _context.Departments.FindAsync(id);
            var result = _service.GetDepartmentByID(id);
            if (result == null)
            {
                return NotFound();
            }

            //GetDepartmentDto result = new GetDepartmentDto()
            //{
            //    DepartmentId = department.DepartmentId,
            //    Name = department.Name,
            //    Description = department.Description
            //};
            return Ok(result);
        }
        // PUT: api/Departments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDepartment(int id, PutDepartmentDto department)
        {
            if (id != department.DepartmentId)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid) return BadRequest(department);

            //var oldDepartment = await _context.Departments.FindAsync(id);

            //oldDepartment.Name = department.Name;
            //oldDepartment.Description = department.Description;

            //_context.Entry(department).State = EntityState.Modified;

            try
            {
                //await _context.SaveChangesAsync();
                _service.UpdateDepartment(department);
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!DepartmentExists(id))
                //{
                //    return NotFound();
                //}
                //else
                //{
                throw;
                //}
            }

            return NoContent(); //204
        }
        // POST: api/Departments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PostDepartmentDto>> PostDepartment(PostDepartmentDto department)
        {
            if (!ModelState.IsValid) return BadRequest(department);


            //Department newDepartment = new Department()
            //{
            //    // DepartmentId = department.DepartmentId,
            //    Name = department.Name,
            //    Description = department.Description
            //};
            //_context.Departments.Add(newDepartment);
            //await _context.SaveChangesAsync();
            //_repos.Add(newDepartment);
            ////_repos.Add(newDepartment);
            ////_repos.Add(newDepartment);
            //_repos.Save();
            //Get Max Identity From ID
            //int newId = _context.Departments.Max(d => d.DepartmentId);
            _service.AddDepartment(department);
            return Ok();
            // return CreatedAtAction("GetDepartment", new { id = newId }, department);
        }
        // DELETE: api/Departments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            //var department = await _context.Departments.FindAsync(id);
            var department = _service.GetDepartmentByID(id);
            if (department == null)
            {
                return NotFound();
            }

            //_context.Departments.Remove(department);
            //await _context.SaveChangesAsync();
            //_repos.delete(id);
            //_repos.Save();
            _service.DeleteDepartment(id);
            return NoContent();
        }
        //private bool DepartmentExists(int id)
        //{
        //    return _context.Departments.Any(e => e.DepartmentId == id);
        //}
        // GET: api/Departments
        [HttpGet("Pagination")]
        public async Task<ActionResult<IEnumerable<GetDepartmentDto>>> GetWithPagination([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            //var departments = await _context.Departments.ToListAsync();
            //List<GetDepartmentDto> result = new List<GetDepartmentDto>();
            //foreach (var item in departments)
            //{
            //    result.Add(new GetDepartmentDto()
            //    {
            //        DepartmentId = item.DepartmentId,
            //        Name = item.Name,
            //        Description = item.Description
            //    });
            //}
            ////Pagination
            //var totalCount = departments.Count();
            //var totalPage = (int)Math.Ceiling((double)totalCount / pageSize);
            //result = result.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            var result = _service.PaginationDepartments(page, pageSize);
            return Ok(result);
        }
        [HttpGet("EmployeeNames")]
        public async Task<ActionResult<IEnumerable<GetDepartmentWithEmpNamesDto>>> GetDepartmentEmpNames()
        {
            //var departments = await _context.Departments.Include("Employees").ToListAsync();
            //List<GetDepartmentWithEmpNamesDto> result = new List<GetDepartmentWithEmpNamesDto>();
            //foreach (var item in departments)
            //{
            //    result.Add(new GetDepartmentWithEmpNamesDto()
            //    {
            //        DepartmentId = item.DepartmentId,
            //        Name = item.Name,
            //        Description = item.Description,
            //        EmployeesCount = item.Employees.Count(),
            //        EmployeeNames = item.Employees.Select(e => e.Name).ToList()
            //    });
            //}
            var result = _service.DepartmentWithEmployee("Employee");
            return Ok(result);
        }
        // GET: api/Departments
    }
}
