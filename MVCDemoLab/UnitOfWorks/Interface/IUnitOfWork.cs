namespace MVCDemoLab.UnitOfWorks.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Category> CategoriesRepository { get; }
        IRepository<Product> ProductsRepository { get; }
        int Complete();
    }
}
