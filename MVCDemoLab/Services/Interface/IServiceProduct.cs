using System.Linq.Expressions;

namespace MVCDemoLab.Services.Interface
{
    public interface IServiceProduct
    {
        //Logical Business
        IEnumerable<Product> GetProducts();
        Product GetProductByID(int id);
        void AddProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(int id);
        int GetProductCounter();
        IEnumerable<Product> PaginationProduct(int page = 1, int pageSize = 10);
        IEnumerable<Product> SearchProduct(Expression<Func<Product, bool>> predicate);
        int GetMaxID();
    }
}
