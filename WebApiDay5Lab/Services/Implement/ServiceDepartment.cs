using System.Linq.Expressions;
using WebApiDay5Lab.DTOs.DepartmentDtos;

namespace WebApiDay5Lab.Services.Implement
{
    public class ServiceDepartment : IServiceDepartment
    {
        public enum IsExistType
        {
            Create, // Sales 
            Update  //   1 - Sales
                    //   2 - HR  
        }
        private readonly IUnitOfWork _unitOfWork;
        public ServiceDepartment(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<GetDepartmentDto> GetDepartments()
        {
            var getList = _unitOfWork.DepartmentRepository.GetAll();
            List<GetDepartmentDto> result = new List<GetDepartmentDto>();
            foreach (var item in getList)
            {
                result.Add(new GetDepartmentDto()
                {
                    DepartmentId = item.DepartmentId,
                    Name = item.Name,
                    Description = item.Description
                });
            }
            return result;
        }
        public GetDepartmentDto GetDepartmentByID(int id)
        {
            var department = _unitOfWork.DepartmentRepository.GetById(id);
            if (department != null)
            {
                GetDepartmentDto result = new GetDepartmentDto()
                {
                    DepartmentId = department.DepartmentId,
                    Name = department.Name,
                    Description = department.Description
                };
                return result;
            }
            else
            {
                return null;
            }


        }
        public void AddDepartment(PostDepartmentDto department)
        {
            _unitOfWork.DepartmentRepository.Add(new Department() { Name = department.Name, Description = department.Description });
            _unitOfWork.Complete();
        }
        public void UpdateDepartment(PutDepartmentDto department)
        {
            var oldDepartment = _unitOfWork.DepartmentRepository.GetById(department.DepartmentId);

            oldDepartment.Name = department.Name;
            oldDepartment.Description = department.Description;

            _unitOfWork.DepartmentRepository.Update(oldDepartment);
            _unitOfWork.Complete();
        }
        public void DeleteDepartment(int id)
        {
            var department = _unitOfWork.DepartmentRepository.GetById(id);
            if (department != null)
            {
                _unitOfWork.DepartmentRepository.Delete(id);
                _unitOfWork.Complete();
            }
        }
        public int GetDepartmentCounter()
        {
            return _unitOfWork.DepartmentRepository.Counter();
        }
        public IEnumerable<GetDepartmentDto> PaginationDepartments(int page = 1, int pageSize = 10)
        {
            var getList = _unitOfWork.DepartmentRepository.GetWithPagination(page, pageSize);
            List<GetDepartmentDto> result = new List<GetDepartmentDto>();
            foreach (var item in getList)
            {
                result.Add(new GetDepartmentDto()
                {
                    DepartmentId = item.DepartmentId,
                    Name = item.Name,
                    Description = item.Description
                });
            }
            return result;
        }
        public IEnumerable<GetDepartmentDto> SearchDepartment(Expression<Func<Department, bool>> predicate)
        {
            var getList = _unitOfWork.DepartmentRepository.Search(predicate);
            List<GetDepartmentDto> result = new List<GetDepartmentDto>();
            foreach (var item in getList)
            {
                result.Add(new GetDepartmentDto()
                {
                    DepartmentId = item.DepartmentId,
                    Name = item.Name,
                    Description = item.Description
                });
            }
            return result;
        }
        public IEnumerable<GetDepartmentWithEmpNamesDto> DepartmentWithEmployee(params string[] including)
        {
            var getList = _unitOfWork.DepartmentRepository.GetAllIncluding(including);
            List<GetDepartmentWithEmpNamesDto> result = new List<GetDepartmentWithEmpNamesDto>();
            foreach (var item in getList)
            {
                result.Add(new GetDepartmentWithEmpNamesDto()
                {
                    DepartmentId = item.DepartmentId,
                    Name = item.Name,
                    Description = item.Description,
                    EmployeesCount = item.Employees.Count(),
                    EmployeeNames = item.Employees.Select(e => e.Name).ToList()
                });
            }
            return result;
        }
        public int GetMaxID()
        {
            return _unitOfWork.DepartmentRepository.GetMaxId();
        }

        public bool isExist(CheckDepartmentDto checkDepartmentDto, ExistType existType = ExistType.Create)
        {
            bool result = false;
            switch (existType)
            {
                case ExistType.Create:
                    result = _unitOfWork.DepartmentRepository.GetAll().Any(d => d.Name.Trim().ToLower() == checkDepartmentDto.Name.Trim().ToLower());
                    break;
                case ExistType.Update:
                    result = _unitOfWork.DepartmentRepository.GetAll().Any(d => d.Name.Trim().ToLower() == checkDepartmentDto.Name.Trim().ToLower()
                                                                      && d.DepartmentId != checkDepartmentDto.DepartmentId);
                    break;
            }
            return result;
        }
    }
}
