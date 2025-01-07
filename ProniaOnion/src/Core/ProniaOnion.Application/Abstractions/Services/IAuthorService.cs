using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProniaOnion.Application.DTOs.Authors;
using ProniaOnion.Application.DTOs.Categories;

namespace ProniaOnion.Application.Abstractions.Services
{
    public interface IAuthorService
    {
        Task<IEnumerable<AuthorItemDTO>> GetAllAsync(int page, int take);

        Task<GetAuthorDTO> GetByIdAsync(int id);

        Task CreateAsync(CreateAuthorDTO authorDTO);

        Task UpdateAsync(int id, UpdateAuthorDTO authorDTO);

        Task DeleteAsync(int id);
    }
}
