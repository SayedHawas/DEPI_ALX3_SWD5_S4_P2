using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace WebApiDay5Lab.Repository.Implement
{
    public class Generic2Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext _context;
        private readonly DbSet<T> _dbSet;
        public Generic2Repository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {
            try
            {
                return _dbSet.AsNoTracking().ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
        public T GetById(int id)
        {
            var keyName = _context.Model.FindEntityType(typeof(T)).FindPrimaryKey().Properties.Select(x => x.Name).Single();

            // Use EF Core's Find method instead of reflection
            return _dbSet.AsNoTracking().FirstOrDefault(e => EF.Property<int>(e, keyName) == id);
        }
        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }
        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }
        public void Delete(int id)
        {
            var entity = _dbSet.Find(id);
            if (entity != null) _dbSet.Remove(entity);
        }
        public IEnumerable<T> Search(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate).AsNoTracking().ToList();
        }
        public int Counter()
        {
            try
            {
                return _dbSet.AsNoTracking().Count();
            }
            catch (SqlException)
            {
                return -1;
            }
            catch (Exception)
            {
                return -1;
            }
        }
        public IEnumerable<T> GetAllIncluding(params string[] includes)
        {
            IQueryable<T> query = _dbSet.AsQueryable();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return query.AsNoTracking().ToList();
        }
        public IEnumerable<T> GetWithPagination(int page = 1, int pageSize = 10)
        {
            IEnumerable<T> list = _dbSet.AsNoTracking().ToList();
            //Pagination
            var totalCount = _dbSet.Count();
            var totalPage = (int)Math.Ceiling((double)totalCount / pageSize);
            list = list.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            return list;
        }
        public int GetMaxId()
        {
            var keyName = _context.Model.FindEntityType(typeof(T)).FindPrimaryKey().Properties.Select(x => x.Name).Single();
            // Use EF Core's Find method instead of reflection
            return _dbSet.AsNoTracking().Max(e => EF.Property<int>(e, keyName));
        }
    }
}
