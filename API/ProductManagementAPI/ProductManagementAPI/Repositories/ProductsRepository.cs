using ProductManagementAPI.Repositories.Models;
using SQLite;

namespace ProductManagementAPI.Repositories
{
    public class ProductsRepository : IProductsRepository
    {
        private SQLiteAsyncConnection _db;

        public ProductsRepository()
        {
            _db = new SQLiteAsyncConnection("../../../Database/ProductManagement.db", 
                SQLiteOpenFlags.Create |
                SQLiteOpenFlags.FullMutex | 
                SQLiteOpenFlags.ReadWrite);

            _db.CreateTableAsync<ProductData>();
        }
        public Task<ProductData> CreateProductAsync(ProductData product)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ProductData>> GetAllProductsAsync()
        {
            return await _db.Table<ProductData>().ToListAsync();
        }
    }
}
