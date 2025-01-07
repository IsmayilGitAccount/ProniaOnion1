using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProniaOnion.Application.DTOs.Categories;
using ProniaOnion.Application.DTOs.Genres;

namespace ProniaOnion.Application.Abstractions.Services
{
    public interface IGenreService
    {
        Task<IEnumerable<GenreItemDTO>> GetAllAsync(int page, int take);

        Task<GetGenreDTO> GetByIdAsync(int id);

        Task CreateAsync(CreateGenreDTO genreDTO);

        Task UpdateAsync(int id, UpdateGenreDTO genreDTO);

        Task DeleteAsync(int id);
    }
}
