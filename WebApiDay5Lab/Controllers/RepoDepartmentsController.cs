using Microsoft.AspNetCore.Mvc;
using WebApiDay5Lab.DTOs.DepartmentDtos;

namespace WebApiDay5Lab.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepoDepartmentsController : ControllerBase
    {
        private readonly IServiceDepartment _service;
        //Comment 
        /*
         
         */
        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        public RepoDepartmentsController(IServiceDepartment service)
        {
            this._service = service;
        }
        // GET: api/Departments
        /// <summary>
        ///  This EndPoint For Get List Of Departments
        /// </summary>
        /// <returns> List Of Departments</returns>
        /// <example>
        ///  GET: api/Departments
        /// </example>
        /// <remarks>
        /// Get Request :api/Departments
        /// </remarks>
        /// <response code="200">Returns the list of departments</response>
        /// <response code="404">No departments found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<GetDepartmentDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<GetDepartmentDto>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<GetDepartmentDto>>> GetDepartments()
        {
            try
            {
                var departments = _service.GetDepartments();
                if (departments == null || !departments.Any())
                {
                    return NotFound("No departments found.");
                }
                return Ok(departments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        // GET: api/Departments/5
        /// <summary>
        /// Retrieves a specific department by ID
        /// </summary>
        /// <param name="id">The department ID</param>
        /// <returns>Department details</returns>
        /// <remarks>
        /// Get Request :api/Departments/{id} Like 1
        /// </remarks>
        /// <response code="200">Returns the department</response>
        /// <response code="404">Department not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetDepartmentDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GetDepartmentDto), 404)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetDepartmentById(int id)
        {
            try
            {
                var department = _service.GetDepartmentByID(id);
                if (department == null)
                {
                    return NotFound($"Department with ID {id} not found.");
                }
                return Ok(department);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        // PUT: api/Departments/5
        /// <summary>
        /// Updates an existing department
        /// </summary>
        /// <param name="id">Department ID to update</param>
        /// <param name="department">Updated department object</param>
        /// <returns>Success message</returns>
        /// <response code="200">Department updated successfully</response>
        /// <response code="400">Invalid request data</response>
        /// <response code="404">Department not found</response>
        /// <response code="500">Internal server error</response>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(PutDepartmentDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(PutDepartmentDto), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateDepartment(int id, [FromBody] PutDepartmentDto department)
        {
            if (_service.isExist(new CheckDepartmentDto { DepartmentId = department.DepartmentId, Name = department.Name }, ExistType.Update))
                return BadRequest("this Department is Al-Ready Exist ....");

            if (id != department.DepartmentId)
            {
                return BadRequest($"The Id Not Matched.");
            }
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var existingDepartment = _service.GetDepartmentByID(id);
                if (existingDepartment == null)
                {
                    return NotFound($"Department with ID {id} not found.");
                }

                department.DepartmentId = id;
                _service.UpdateDepartment(department);
                return Ok("Department updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        // POST: api/Departments
        /// <summary>
        /// Creates a new department
        /// </summary>
        /// <param name="department">Department object to create</param>
        /// <returns>Created department</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/Department
        ///     {
        ///        "name": "IT Department",
        ///        "description": "Information Technology"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Department created successfully</response>
        /// <response code="400">Invalid request data</response>
        /// <response code="500">Internal server error</response>
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PostDepartmentDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(PostDepartmentDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateDepartment([FromBody] PostDepartmentDto department)
        {

            if (_service.isExist(new CheckDepartmentDto { DepartmentId = department.DepartmentId, Name = department.Name }, ExistType.Create))
                return BadRequest("this Department is Al-Ready Exist ....");
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _service.AddDepartment(department);
                int newId = _service.GetMaxID();
                //return CreatedAtAction("GetDepartment", new { id = newId }, department);
                return CreatedAtAction(nameof(GetDepartmentById), new { id = newId }, department);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        // DELETE: api/Departments/5
        /// <summary>
        /// Deletes a department
        /// </summary>
        /// <param name="id">Department ID to delete</param>
        /// <returns>Success message</returns>
        /// <response code="200">Department deleted successfully</response>
        /// <response code="404">Department not found</response>
        /// <response code="500">Internal server error</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GetDepartmentDto), 404)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteDepartment(int id)
        {
            try
            {
                var department = _service.GetDepartmentByID(id);
                if (department == null)
                {
                    return NotFound($"Department with ID {id} not found.");
                }

                _service.DeleteDepartment(id);
                return Ok("Department deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        // GET: api/Departments
        /// <summary>
        /// This EndPoint For Get List Of Departments With Pagination
        /// </summary>
        /// <param name="page"> Current Page Integer Number default 1</param>
        /// <param name="pageSize"> Page size Of Row default 10</param>
        /// <returns> List Of Department With Pagination</returns>
        [HttpGet("Pagination")]
        public async Task<ActionResult<IEnumerable<GetDepartmentDto>>> GetWithPagination([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var result = _service.PaginationDepartments(page, pageSize);
            return Ok(result);
        }
        [HttpGet("EmployeeNames")]
        public async Task<ActionResult<IEnumerable<GetDepartmentWithEmpNamesDto>>> GetDepartmentEmpNames()
        {
            var result = _service.DepartmentWithEmployee("Employees");
            return Ok(result);
        }
    }
}
//        private readonly IServiceDepartment _service;
//        //Comment 
//        /*

//         */
//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="service"></param>
//        public RepoDepartmentsController(IServiceDepartment service)
//        {
//            this._service = service;
//        }
//        // GET: api/Departments
//        /// <summary>
//        ///  This EndPoint For Get List Of Departments
//        /// </summary>
//        /// <returns> List Of Departments</returns>
//        /// <example>
//        ///  GET: api/Departments
//        /// </example>
//        /// <remarks>
//        /// Get Request :api/Departments
//        /// </remarks>
//        [HttpGet]
//        [Produces("application/json")]
//        public async Task<ActionResult<IEnumerable<GetDepartmentDto>>> GetDepartments()
//        {
//            return Ok(_service.GetDepartments());
//        }
//        // GET: api/Departments/5
//        /// <summary>
//        ///  This EndPoint For Get One Of Departments By ID
//        /// </summary>
//        /// <param name="id"> Integer Number</param>
//        /// <returns> Object Of Department </returns>
//        /// /// <remarks>
//        /// Get Request :api/Departments/{id} Like 1
//        /// </remarks>
//        [HttpGet("{id}")]
//        [Produces("application/json")]
//        [ProducesResponseType<GetDepartmentDto>(200)]
//        [ProducesResponseType(404, Type = (typeof(void)))]
//        public async Task<ActionResult<GetDepartmentDto>> GetDepartment(int id)
//        {
//            var result = _service.GetDepartmentByID(id);
//            if (result == null)
//            {
//                return NotFound();//404
//            }
//            return Ok(result);
//        }
//        // PUT: api/Departments/5
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        /// <summary>
//        /// This EndPoint For Update Department
//        /// </summary>
//        ///  <param name="id"> Integer Number</param>
//        /// <param name="department"> New Object Of Department</param>
//        /// <returns> NoContent 204</returns>
//        [HttpPut("{id}")]
//        [Produces("application/json")]
//        [Consumes("application/json")]
//        [ProducesResponseType(204, Type = (typeof(void)))]
//        [ProducesResponseType<PutDepartmentDto>(400, Type = (typeof(void)))]

//        public async Task<IActionResult> PutDepartment(int id, PutDepartmentDto department)
//        {
//            if (id != department.DepartmentId)
//            {
//                return BadRequest();
//            }
//            if (!ModelState.IsValid) return BadRequest(department);
//            try
//            {
//                _service.UpdateDepartment(department);
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                throw;
//            }
//            return NoContent(); //204
//        }
//        // POST: api/Departments
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        /// <summary>
//        /// This EndPoint For Create New department
//        /// </summary>
//        /// <param name="department"> new Department </param>
//        /// <returns>Created 200</returns>
//        [HttpPost]
//        [Consumes("application/json")]
//        [Produces("application/json")]
//        public async Task<ActionResult<PostDepartmentDto>> PostDepartment(PostDepartmentDto department)
//        {
//            if (!ModelState.IsValid) return BadRequest(department);
//            _service.AddDepartment(department);
//            int newId = _service.GetMaxID();

//            return CreatedAtAction("GetDepartment", new { id = newId }, department);
//        }
//        // DELETE: api/Departments/5
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteDepartment(int id)
//        {
//            var department = _service.GetDepartmentByID(id);
//            if (department == null)
//            {
//                return NotFound();
//            }
//            _service.DeleteDepartment(id);
//            return NoContent();
//        }
//        // GET: api/Departments
//        /// <summary>
//        /// This EndPoint For Get List Of Departments With Pagination
//        /// </summary>
//        /// <param name="page"> Current Page Integer Number default 1</param>
//        /// <param name="pageSize"> Page size Of Row default 10</param>
//        /// <returns> List Of Department With Pagination</returns>
//        [HttpGet("Pagination")]
//        public async Task<ActionResult<IEnumerable<GetDepartmentDto>>> GetWithPagination([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
//        {
//            var result = _service.PaginationDepartments(page, pageSize);
//            return Ok(result);
//        }
//        [HttpGet("EmployeeNames")]
//        public async Task<ActionResult<IEnumerable<GetDepartmentWithEmpNamesDto>>> GetDepartmentEmpNames()
//        {
//            var result = _service.DepartmentWithEmployee("Employees");
//            return Ok(result);
//        }

//    }
//}
