using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProniaOnion.Application.Abstractions.Repositories;
using ProniaOnion.Application.Abstractions.Services;
using ProniaOnion.Application.DTOs.Categories;
using ProniaOnion.Application.DTOs.Products;
using ProniaOnion.Domain.Entities;

namespace ProniaOnion.Persistence.Implementations.Services
{
    internal class CategoryService : ICategoryService
    {
        public readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task CreateAsync(CreateCategoryDTO categoryDTO)
        {
            if (await _categoryRepository.AnyAsync(c => c.Name == categoryDTO.Name))
                throw new Exception("Already exist");

            var category = _mapper.Map<Category>(categoryDTO);

            category.CreatedAt = DateTime.Now;
            category.UpdatedAt = DateTime.Now;

            await _categoryRepository.AddAsync(category);

            await _categoryRepository.SaveChangesAsync();
            
        }

        public async Task DeleteAsync(int id)
        {
            Category category = await _categoryRepository.GetByIdAsync(id);

            if (category == null) throw new Exception("Not Found");
            _categoryRepository.Delete(category);

            await _categoryRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<CategoryItemDTO>> GetAllAsync(int page, int take)
        {
            IEnumerable<Category> categories = await _categoryRepository
                .GetAll(skip: (page - 1) * take, take: take, ignoreQuery: true)
                .ToListAsync();

            return _mapper.Map<IEnumerable<CategoryItemDTO>>(categories);
        }

        public async Task<GetCategoryDTO> GetByIdAsync(int id)
        {
            Category category = await _categoryRepository.GetByIdAsync(id, nameof(Category.Products));

            if (category == null) return null;

            //ICollection<ProductItemDTO> productItems = category.Products.Select(p => new ProductItemDTO(p.Id, p.Name, p.Price, p.SKU, p.Description)).ToList();
            //GetCategoryDTO categoryDTO = new(category.Id, category.Name, productItems);
            
            GetCategoryDTO categoryDTO = _mapper.Map<GetCategoryDTO>(category);
                
            return categoryDTO;
        }

        public async Task UpdateAsync(int id, UpdateCategoryDTO categoryDTO)
        {
            Category category = await _categoryRepository.GetByIdAsync(id);

            if (category == null) throw new Exception("Not Found");

            if (await _categoryRepository.AnyAsync(c => c.Name == categoryDTO.Name && c.Id != id)) throw new Exception("Already Exists");


            category = _mapper.Map(categoryDTO, category);


            category.UpdatedAt = DateTime.Now;

            _categoryRepository.Update(category);

            await _categoryRepository.SaveChangesAsync();
        }


        public async Task SoftDelete(int id)
        {
            Category category = await _categoryRepository.GetByIdAsync(id);

            if (category == null) throw new Exception("Not Found");

            category.IsDeleted = true;

            _categoryRepository.Update(category);
            await _categoryRepository.SaveChangesAsync();
        }


    }
}
