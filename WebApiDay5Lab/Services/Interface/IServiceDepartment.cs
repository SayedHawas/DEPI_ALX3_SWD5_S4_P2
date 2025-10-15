using System.Linq.Expressions;
using WebApiDay5Lab.DTOs.DepartmentDtos;

namespace WebApiDay5Lab.Services.Interface
{
    public interface IServiceDepartment
    {
        //Logical Business
        IEnumerable<GetDepartmentDto> GetDepartments();
        GetDepartmentDto GetDepartmentByID(int id);
        void AddDepartment(PostDepartmentDto department);
        void UpdateDepartment(PutDepartmentDto department);
        void DeleteDepartment(int id);
        int GetDepartmentCounter();
        IEnumerable<GetDepartmentDto> PaginationDepartments(int page = 1, int pageSize = 10);
        IEnumerable<GetDepartmentDto> SearchDepartment(Expression<Func<Department, bool>> predicate);
        IEnumerable<GetDepartmentWithEmpNamesDto> DepartmentWithEmployee(params string[] including);
        int GetMaxID();
        public bool isExist(CheckDepartmentDto checkDepartmentDto, ExistType existType = ExistType.Create);
    }
}
