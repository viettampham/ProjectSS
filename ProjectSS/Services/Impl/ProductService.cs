using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ProjectSS.Models;
using ProjectSS.Models.RequestModels;
using ProjectSS.Models.ViewModels;

namespace ProjectSS.Services.Impl
{
    public class ProductService: IProductService
    {
        private readonly MasterDbContext _context;

        public ProductService(MasterDbContext context)
        {
            _context = context;
        }
        
        
        public ProductResponse CreateProduct(CreateProductRequest request)
        {
            var products = _context.Products.FirstOrDefault(t => t.title == request.title);
            if (products!=null)
            {
                throw new Exception("This product is exist");
            }

            var categorys = new List<Category>();
            foreach (var guid in request.CategorieID)
            {
                var categoryid = _context.Categories.FirstOrDefault(c => c.id == guid);
                if (categoryid == null)
                {
                    throw new Exception("This Category not exist");
                }
                categorys.Add(categoryid);
            }
            
            var newProduct = new Product
            {
                id = Guid.NewGuid(),
                title = request.title,
                description = request.description,
                image = request.image,
                image_url = request.image_url,
                quantityAvailable = request.quantityAvailable,
                price = request.price,
                size = request.size,
                Brand = request.Brand,
                Categories = categorys
            };

            _context.Add(newProduct);
            _context.SaveChanges();
            return new ProductResponse
            {
                Id = newProduct.id,
                Title = newProduct.title,
                Description = newProduct.description,
                image = newProduct.image,
                image_url = newProduct.image_url,
                QuantityaVailable = newProduct.quantityAvailable,
                Price = newProduct.price,
                Size = newProduct.size,
                Brand = newProduct.Brand,
                Categorys = newProduct.Categories
            };
        }

        public ProductResponse EditProduct(EditProductRequest request)
        {
            var targetProduct = _context.Products
                .Include(p=>p.Categories)
                .FirstOrDefault(p => p.id == request.id);
            
            if (targetProduct == null)
            {
                throw new Exception("This product not exist");
            }
            
            targetProduct.Categories.Clear();

            var categorys = new List<Category>();
            foreach (var categoryId in request.Categories)
            {
                var targetCategory = _context.Categories
                    .Include(c => c.Products)
                    .FirstOrDefault(c => c.id == categoryId);
                if (targetCategory == null)
                {
                    throw new Exception("Category not exist");
                }

                if (targetCategory != null)
                {
                    categorys.Add(targetCategory);
                }
                else
                {
                    categorys = new List<Category> { targetCategory };
                }
            }

            targetProduct.title = request.title;
            targetProduct.description = request.description;
            targetProduct.image = request.image;
            targetProduct.image_url = request.image_url;
            targetProduct.quantityAvailable = request.quantityAvailable;
            targetProduct.price = request.price;
            targetProduct.size = request.size;
            targetProduct.Brand = request.Brand;
            targetProduct.Categories = categorys;


            _context.SaveChanges();
            
            return new ProductResponse
            {
                Id = targetProduct.id,
                Title = targetProduct.title,
                Description = targetProduct.description,
                image = targetProduct.image,
                image_url = targetProduct.image_url,
                QuantityaVailable = targetProduct.quantityAvailable,
                Price = targetProduct.price,
                Size = targetProduct.size,
                Brand = targetProduct.Brand,
                Categorys = targetProduct.Categories
            };
        }

        public ProductResponse DeleteProduct(DeleteProductRequest request)
        {
            var targetProduct = _context.Products.FirstOrDefault(p => p.id == request.Id);
            if (targetProduct == null)
            {
                throw new Exception("This product not exist");
            }

            _context.Remove(targetProduct);
            _context.SaveChanges();
            return new ProductResponse
            {
                Id = targetProduct.id,
                Title = targetProduct.title,
                Description = targetProduct.description,
                image = targetProduct.image,
                image_url = targetProduct.image_url,
                QuantityaVailable = targetProduct.quantityAvailable,
                Price = targetProduct.price,
                Size = targetProduct.size,
                Brand = targetProduct.Brand,
                Categorys = targetProduct.Categories
            };
        }

        public List<GetListProductResponse> GetListProduct()
        {
            var listProduct = _context.Products.Select(product => new ProductResponse
            {
                Id = product.id,
                Title = product.title,
                image_url = product.image_url,
                QuantityaVailable = product.quantityAvailable,
                Price = product.price,
                Description = product.description,
                Size = product.size,
                Brand = product.Brand,
                Categorys = product.Categories
            }).ToList();

            var products = new List<GetListProductResponse>();
            foreach (var product in listProduct)
            {
                var caregoryTempo = new List<CategoryTempo>();
                foreach (var p in product.Categorys)
                {
                    var category = new CategoryTempo
                    {
                        Id = p.id,
                        Title = p.title
                    };
                    caregoryTempo.Add(category);
                }
                var productTemporariry = new GetListProductResponse
                {
                    Id = product.Id,
                    Title = product.Title,
                    Description = product.Description,
                    image = product.image,
                    image_url = product.image_url,
                    Size = product.Size,
                    Price = product.Price,
                    QuantityaVailable = product.QuantityaVailable,
                    Brand = product.Brand,
                    Categorys = caregoryTempo
                };
                products.Add(productTemporariry);
            }
            return products;
        }
    }
}