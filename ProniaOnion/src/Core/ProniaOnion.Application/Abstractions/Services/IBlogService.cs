using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProniaOnion.Application.DTOs.Blogs;
    
namespace ProniaOnion.Application.Abstractions.Services
{
    public interface IBlogService
    {
        Task<IEnumerable<BlogItemDTO>> GetAllAsync(int page, int take);

        Task<GetBlogDTO> GetByIdAsync(int id);

        Task CreateAsync(CreateBlogDTO blogDTO);

        Task UpdateAsync(int id, UpdateBlogDTO blogDTO);

        Task DeleteAsync(int id);
    }
}
