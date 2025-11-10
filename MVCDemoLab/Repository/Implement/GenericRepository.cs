using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MVCDemoLab.Repository.Implement
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        private readonly MVCDbContext _appDbContext;
        public GenericRepository(MVCDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public void Add(T entity)
        {
            _appDbContext.Set<T>().Add(entity);
        }
        public int Counter()
        {
            return _appDbContext.Set<T>().Count();
        }
        public void Delete(int id)
        {
            T entity = _appDbContext.Set<T>().Find(id);
            _appDbContext.Set<T>().Remove(entity);
        }
        public IEnumerable<T> GetAll()
        {
            return _appDbContext.Set<T>().AsNoTracking().ToList();
        }
        public T GetById(int? id)
        {
            return _appDbContext.Set<T>().Find(id);
        }
        public IEnumerable<T> GetWithPagination(int page = 1, int pageSize = 10)
        {
            var totalCount = _appDbContext.Set<T>().Count();
            var totalPage = (int)Math.Ceiling((double)totalCount / pageSize);
            var list = _appDbContext.Set<T>().AsNoTracking().Skip((page - 1) * pageSize).Take(pageSize);
            return list.ToList();
        }
        public void Update(T entity)
        {
            _appDbContext.Entry(entity).State = EntityState.Modified;
        }
        public IEnumerable<T> Search(Expression<Func<T, bool>> predicate)
        {
            return _appDbContext.Set<T>().Where(predicate).AsNoTracking().ToList();
        }
        public IEnumerable<T> GetAllIncluding(params string[] includes) //("Employees"," Orders")
        {
            IQueryable<T> query = _appDbContext.Set<T>().AsQueryable();
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return query.AsNoTracking().ToList();
        }
        public int GetMaxId()
        {
            var keyName = _appDbContext.Model.FindEntityType(typeof(T)).FindPrimaryKey().Properties.Select(x => x.Name).Single();
            // Use EF Core's Find method instead of reflection
            return _appDbContext.Set<T>().Max(e => EF.Property<int>(e, keyName));

        }
    }
}
