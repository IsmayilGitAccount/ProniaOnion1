using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProniaOnion.Application.DTOs.Categories;
using ProniaOnion.Application.DTOs.Tags;

namespace ProniaOnion.Application.Abstractions.Services
{
    public interface ITagService
    {
        Task<IEnumerable<TagItemDTO>> GetAllAsync(int page, int take);

        Task<GetTagDTO> GetByIdAsync(int id);

        Task CreateAsync(CreateTagDTO tagDTO);

        Task UpdateAsync(int id, UpdateTagDTO tagDTO);

        Task DeleteAsync(int id);
    }
}
