using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProniaOnion.Application.Abstractions.Repositories;
using ProniaOnion.Application.Abstractions.Services;
using ProniaOnion.Application.DTOs.Products;
using ProniaOnion.Domain.Entities;
using ProniaOnion.Persistence.Implementations.Repositories;

namespace ProniaOnion.Persistence.Implementations.Services
{
    internal class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;

        public ProductService(IProductRepository productRepository,ICategoryRepository categoryRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }
        public async Task<IEnumerable<ProductItemDTO>> GetAllAsync(int page, int take)
        {
            var products = _mapper.Map<IEnumerable<ProductItemDTO>>(
                await _productRepository
                .GetAll(skip: (page - 1) * take, take: take)
                .ToListAsync());

            return products;
        }

        public async Task<GetProductDTO> GetByIdAsync(int id)
        {
           var product = _mapper.Map<GetProductDTO>(await _productRepository.GetByIdAsync(id, "Category", "ProductColors.Color"));

            if (product == null) throw new Exception("Product does not exists");

            return product;
        }

        public async Task CreateAsync(CreateProductDTO productDTO)
        {
            if (!await _categoryRepository.AnyAsync(c => c.Id == productDTO.CategoryId))
                throw new Exception("Category does not exist");

            var colorEntities = await _productRepository.GetManyToManyEntities<Color>(productDTO.ColorsIds);

            if (colorEntities.Count() != productDTO.ColorsIds.Distinct().Count())
                throw new Exception("Color id is wrong");

            await _productRepository.AddAsync(_mapper.Map<Product>(productDTO));
            await _productRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, UpdateProductDTO productDTO)
        {
            Product product = await _productRepository .GetByIdAsync(id, "ProductColors");

            if(productDTO.CategoryId != product.CategoryId)
                if (!await _categoryRepository.AnyAsync(c => c.Id == productDTO.CategoryId))
                    throw new Exception("Category does not exist");
            
            //product.ProductColors = product.ProductColors.Where(pc=>productDTO.ColorsIds.Contains(pc.ColorId)).ToList();

           ICollection<int> createItems = productDTO.ColorsIds.Where(cId => !product.ProductColors.Any(pc => pc.ColorId == cId)).ToList();

            var colorEntities = await _productRepository.GetManyToManyEntities<Color>(createItems);

            if (colorEntities.Count() != createItems.Distinct().Count())
                throw new Exception("One or more Color Id is wrong");

            _productRepository.Update(_mapper.Map(productDTO, product));

            await _productRepository.SaveChangesAsync();    
        }
    }
}
