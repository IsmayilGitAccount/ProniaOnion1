using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProniaOnion.Application.DTOs.Colors;

namespace ProniaOnion.Application.Abstractions.Services
{
    public interface IColorService
    {
        Task<IEnumerable<ColorItemDTO>> GetAll(int page, int take);

        Task<GetColorDTO> GetByIdAsync(int id);

        Task CreateAsync(CreateColorDTO color);

        Task DeleteAsync(int id);

        Task UpdateAsync(int id, UpdateColorDTO color);
    }
}
