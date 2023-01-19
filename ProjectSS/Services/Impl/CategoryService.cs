using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ProjectSS.Models;
using ProjectSS.Models.RequestModels;
using ProjectSS.Models.ViewModels;

namespace ProjectSS.Services.Impl
{
    public class CategoryService:ICategoryService
    {
        private readonly MasterDbContext _context;

        public CategoryService(MasterDbContext context)
        {
            _context = context;
        }

        public CategoryResponse CreateCategory(CreateCategoryRequest request)
        {
            var product = new List<Product>();
            var categorys = _context.Categories.FirstOrDefault(c => c.title == request.Tittle);
            if (categorys != null)
            {
                throw new Exception("This category is exist");
            }
            
            

            foreach (var guid in request.ProductsID)
            {
                var tarProduct = _context.Products.FirstOrDefault(p => p.id == guid);
                if (tarProduct == null)
                {
                    throw new Exception("This product not exist");
                }
                product.Add(tarProduct);
                
            }

            var newCategory = new Category
            {
                id = Guid.NewGuid(),
                title = request.Tittle,
                Products = product
            };
            _context.Categories.Add(newCategory);
            _context.SaveChanges();

            return new CategoryResponse
            {
                Id = newCategory.id,
                Title = newCategory.title,
                /*Products = newCategory.Products*/
            };
        }

        public CategoryResponse EditCategory(EditCategoryRequest request)
        {
            var targetCategory = _context.Categories
                .Include(c=>c.Products)
                .FirstOrDefault(c => c.id == request.Id);
            if (targetCategory == null)
            {
                throw new Exception("This category not exist");
            }

            var products = new List<Product>();
            targetCategory.Products.Clear();
            foreach (var Pid in request.ProductID)
            {
                var product = _context.Products.FirstOrDefault(p => p.id == Pid);
                if (product == null)
                {
                    throw new Exception("this product not exist");
                }

                if (product != null)
                {
                    products.Add(product);
                }
                else
                {
                    products = new List<Product>{product};
                }

            }
            targetCategory.title = request.Title;
            targetCategory.Products = products;
            _context.SaveChanges();
            return new CategoryResponse
            {
                Id = targetCategory.id,
                Title = targetCategory.title,
                Products = targetCategory.Products

            };
        }

        public CategoryResponse DeleteCategory(Guid id)
        {
            var targetCategory = _context.Categories.FirstOrDefault(c => c.id == id);
            if (targetCategory == null)
            {
                throw new Exception("This category not exist");
            }

            _context.Remove(targetCategory);
            _context.SaveChanges();
            return new CategoryResponse
            {
                Id = targetCategory.id,
                Title = targetCategory.title
            };
        }

        public List<CategoryResponse> GetlistCategory()
        {
            var listCategory = _context.Categories.Select(c => new CategoryResponse
            {
                Id = c.id,
                Title = c.title,
                Products = c.Products
            }).ToList();
            return listCategory;
        }

        public CategoryResponse GetCategoryById(Guid id)
        {
            var targetCategory = new CategoryResponse();
            var listCategory = _context.Categories.Select(c => new CategoryResponse
            {
                Id = c.id,
                Title = c.title,
                Products = c.Products
            }).ToList();
            foreach (var c in listCategory)
            {
                if (c.Id == id)
                {
                    targetCategory = c;
                }
            }
            return targetCategory;
        }
    }
}