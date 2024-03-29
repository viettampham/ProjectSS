﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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

        public ProductResponse DeleteProduct(Guid id)
        {
            var targetProduct = _context.Products.FirstOrDefault(p => p.id == id);
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

        public List<string> GetBrand()
        {
            var ListBrand = new List<string>();
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

            foreach (var product in listProduct)
            {
                ListBrand.Add(product.Brand);
            }

            for (int i = 0; i < ListBrand.Count; i++)
            {
                for (int j = i+1 ; j < ListBrand.Count; j++)
                {
                    if (ListBrand[i] == ListBrand[j])
                    {
                        ListBrand.Remove(ListBrand[i]);
                    }
                }
            }
            return ListBrand;
        }

        public ProductResponse GetProductById(Guid id)
        {
            var productResponse = new ProductResponse();
            var targetproduct = _context.Products.FirstOrDefault(p => p.id == id);
            if (targetproduct == null)
            {
                throw new Exception("not found");
            }

            productResponse = new ProductResponse()
            {
                Id = targetproduct.id,
                Title = targetproduct.title,
                Description = targetproduct.description,
                image = targetproduct.image,
                image_url = targetproduct.image_url,
                Size = targetproduct.size,
                Price = targetproduct.price,
                QuantityaVailable = targetproduct.quantityAvailable,
                Brand = targetproduct.Brand,
                Categorys = targetproduct.Categories
            };
            return productResponse;
        }

        public List<ProductResponse> GetListProductByCategory(Guid id)
        {
            var Products = new List<ProductResponse>();
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
            if (targetCategory == null)
            {
                throw new Exception("Error");
            }
            
            foreach (var p in targetCategory.Products)
            {
                var ProductResponse = new ProductResponse()
                {
                    Id = p.id,
                    Title = p.title,
                    Description = p.description,
                    image = p.image,
                    image_url = p.image_url,
                    Size = p.size,
                    Price = p.price,
                    QuantityaVailable = p.quantityAvailable,
                    Brand = p.Brand,
                };
                Products.Add(ProductResponse);
            }
            return Products;
        }

        public List<ProductResponse> GetproductByBrand(string brand)
        {
            var Products = new List<ProductResponse>();
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
            foreach (var p in listProduct)
            {
                if (p.Brand == brand)
                {
                    Products.Add(p);
                }
            }
            return Products;
        }

        public List<ProductResponse> SearchProduct(string request)
        {
            var Products = new List<ProductResponse>();
            var listProduct = _context.Products.Where(p => p.title.ToLower().Contains(request.ToLower()) || p.Brand.ToLower().Contains(request.ToLower())).ToList();
            if (listProduct == null)
            {
                throw new Exception("not found");
            }
            foreach (var product in listProduct)
            {
                var productResponse = new ProductResponse()
                {
                    Id = product.id,
                    Title = product.title,
                    Description = product.description,
                    image = product.image,
                    image_url = product.image_url,
                    Size = product.size,
                    Price = product.price,
                    QuantityaVailable = product.quantityAvailable,
                    Brand = product.Brand,
                    Categorys = product.Categories
                };
                Products.Add(productResponse);
            }
            return Products;
        }
    }
}