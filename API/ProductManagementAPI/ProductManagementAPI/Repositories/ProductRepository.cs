﻿using ProductManagementAPI.Repositories.Models;
using SQLite;

namespace ProductManagementAPI.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private SQLiteAsyncConnection _db;

        public ProductRepository()
        {
            _db = new SQLiteAsyncConnection("../../../Database/ProductManagement.db", 
                SQLiteOpenFlags.Create |
                SQLiteOpenFlags.FullMutex | 
                SQLiteOpenFlags.ReadWrite);

            _db.CreateTableAsync<ProductData>();
        }
        public async Task<ProductData> CreateProductAsync(ProductData product)
        {
            await _db.InsertAsync(product);
            return product;
        }

        public async Task<IEnumerable<ProductData>> GetAllProductsAsync()
        {
            return await _db.Table<ProductData>().ToListAsync();
        }
    }
}
