using Microsoft.EntityFrameworkCore;
using ProductCatalogV2.Models;

namespace ProductCatalogV2.BusinessLogic
{
    public class ProductBL
    {
        private readonly AppDbContext _context;
        private static bool _isInitialized = false;

        public ProductBL(AppDbContext context)
        {
            _context = context;

            initTestData();
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _context.Products.Include(p => p.Category).ToListAsync();
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task AddProductAsync(Product product)
        {
            _context.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(Product product)
        {
            var existingProduct = await _context.Products.FindAsync(product.Id);
            if (existingProduct != null)
            {
                existingProduct.ProductName = product.ProductName;
                existingProduct.ProductPrice = product.ProductPrice;
                existingProduct.ImageUrl = product.ImageUrl;
                existingProduct.Description = product.Description;
                existingProduct.CategoryId = product.CategoryId;
                
                _context.Products.Update(existingProduct);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }

        public bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }

        private void initTestData()
        {
            if (_context.Products.Count() == 0 && _isInitialized==false)
            {
                // Ensure a category exists
                var category = _context.Categories.FirstOrDefault();
                if (category == null)
                {
                    category = new Category { CategoryName = "Dell Products" };
                    _context.Categories.Add(category);

                    category = new Category { CategoryName = "Apple Products" };
                    _context.Categories.Add(category);
                    
                    _context.SaveChanges();
                }

                var products = new List<Product>
                {
                    new Product
                    {
                        Id = 1,
                        ProductName = "iPhone 17 Pro",
                        ProductPrice = 999.99m,
                        ImageUrl = "https://store.storeimages.cdn-apple.com/1/as-images.apple.com/is/iphone-card-40-17pro-202509?wid=680&hei=528&fmt=p-jpg&qlt=95",
                        Description = "The first iPhone to feature an aerospace-grade titanium design.",
                        CategoryId = category.Id
                    },
                    new Product
                    {
                        Id = 2,
                        ProductName = "AirPods Pro (2nd Gen)",
                        ProductPrice = 249.00m,
                        ImageUrl = "https://store.storeimages.cdn-apple.com/4982/as-images.apple.com/is/MTJV3?wid=1144&hei=1144&fmt=jpeg&qlt=90",
                        Description = "Rich audio. Magical active noise cancellation.",
                        CategoryId = category.Id
                    },
                    new Product
                    {
                        Id = 3,
                        ProductName = "iPad Air",
                        ProductPrice = 599.00m,
                        ImageUrl = "https://store.storeimages.cdn-apple.com/4982/as-images.apple.com/is/ipad-air-storage-select-202207-blue?wid=2560&hei=1440&fmt=p-jpg&qlt=95",
                        Description = "Light. Bright. Full of might.",
                        CategoryId = category.Id
                    },
                    new Product
                    {
                        Id = 4,
                        ProductName = "MacBook Air 15-inch",
                        ProductPrice = 1299.00m,
                        ImageUrl = "https://store.storeimages.cdn-apple.com/1/as-images.apple.com/is/mac-card-40-macbook-air-202503?wid=680&hei=528&fmt=p-jpg&qlt=95",
                        Description = "Impressively big. Impossibly thin.",
                        CategoryId = category.Id
                    },
                    new Product
                    {
                        Id = 5,
                        ProductName = "Apple Watch Series 9",
                        ProductPrice = 399.00m,
                        ImageUrl = "https://store.storeimages.cdn-apple.com/4982/as-images.apple.com/is/watch-card-40-s9-202309?wid=1200&hei=1500&fmt=p-jpg&qlt=95",
                        Description = "Smarter. Brighter. Mightier.",
                        CategoryId = category.Id
                    }
                };

                _context.Products.AddRange(products);
                _context.SaveChanges();

                _isInitialized = true;
            }
        }
    }
}
