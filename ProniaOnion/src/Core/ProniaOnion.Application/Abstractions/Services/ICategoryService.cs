using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProniaOnion.Application.DTOs.Categories;
using ProniaOnion.Application.DTOs.Products;

namespace ProniaOnion.Application.Abstractions.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryItemDTO>> GetAllAsync(int page, int take);

        Task<GetCategoryDTO> GetByIdAsync(int id);

        Task CreateAsync(CreateCategoryDTO categoryDTO);

        Task UpdateAsync(int id, UpdateCategoryDTO categoryDTO);

        Task DeleteAsync(int id);
    }
}
