using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProniaOnion.Application.DTOs.Categories;
using ProniaOnion.Application.DTOs.Sizes;

namespace ProniaOnion.Application.Abstractions.Services
{
    public interface ISizeService
    {
        Task<IEnumerable<SizeItemDTO>> GetAllAsync(int page, int take);

        Task<GetSizeDTO> GetByIdAsync(int id);

        Task CreateAsync(CreateSizeDTO sizeDTO);

        Task UpdateAsync(int id, UpdateSizeDTO sizeDTO);

        Task DeleteAsync(int id);
    }
}
