namespace WebApiDay5Lab.DTOs.DepartmentDtos
{
    public class GetDepartmentWithEmpNamesDto
    {
        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public List<string> EmployeeNames { get; set; }
        public int EmployeesCount { get; set; }
    }
}
