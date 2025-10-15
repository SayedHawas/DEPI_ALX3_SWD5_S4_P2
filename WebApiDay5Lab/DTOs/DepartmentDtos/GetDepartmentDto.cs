namespace WebApiDay5Lab.DTOs.DepartmentDtos
{
    public class GetDepartmentDto
    {
        //[JsonIgnore]
        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}
