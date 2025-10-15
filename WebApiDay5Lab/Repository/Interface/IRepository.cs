using System.Linq.Expressions;

namespace WebApiDay5Lab.Repository.Interface
{
    public enum ExistType
    {
        Create = 1,
        Update
    }
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(int id);
        int Counter();
        IEnumerable<T> GetAllIncluding(params string[] includes);
        //AppDbContext.department.include("employees").tolist()
        IEnumerable<T> Search(Expression<Func<T, bool>> predicate);
        //AppDbContext.department.where(d=>d.decription == "" && ManagerName=="")
        //AppDbContext.department.where(d=>d.id== ????)
        IEnumerable<T> GetWithPagination(int page = 1, int pageSize = 10);
        int GetMaxId();

        // bool isExist(T entity, ExistType existType = ExistType.Create);

    }
}
