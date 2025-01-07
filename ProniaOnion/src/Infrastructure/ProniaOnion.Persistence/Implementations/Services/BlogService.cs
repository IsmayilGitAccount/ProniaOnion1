using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProniaOnion.Application.Abstractions.Repositories;
using ProniaOnion.Application.Abstractions.Services;
using ProniaOnion.Application.DTOs.Authors;
using ProniaOnion.Application.DTOs.Blogs;
using ProniaOnion.Domain.Entities;
using ProniaOnion.Persistence.Implementations.Repositories;

namespace ProniaOnion.Persistence.Implementations.Services
{
    internal class BlogService : IBlogService
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IMapper _mapper;

        public BlogService(IBlogRepository blogRepository, IMapper mapper)
        {
            _blogRepository = blogRepository;
            _mapper = mapper;
        }
        public async Task CreateAsync(CreateBlogDTO blogDTO)
        {
            if (await _blogRepository.AnyAsync(b => b.Title == blogDTO.Title))
                throw new Exception("Already exist");

            var blog = _mapper.Map<Blog>(blogDTO);

            blog.CreatedAt = DateTime.Now;
            blog.UpdatedAt = DateTime.Now;

            await _blogRepository.AddAsync(blog);

            await _blogRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Blog blog = await _blogRepository.GetByIdAsync(id);

            if (blog == null) throw new Exception("Not Found");

             _blogRepository.Delete(blog);

            await _blogRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<BlogItemDTO>> GetAllAsync(int page, int take)
        {
            IEnumerable<Blog> blogs = await _blogRepository.GetAll(skip: (page-1)*take, take: take)
                .ToListAsync();

            return _mapper.Map<IEnumerable<BlogItemDTO>>(blogs);    
        }

        public async Task<GetBlogDTO> GetByIdAsync(int id)
        {
            Blog blog = await _blogRepository.GetByIdAsync(id);

            if (blog == null) return null;

            GetBlogDTO blogDTO = _mapper.Map<GetBlogDTO>(blog);

            return blogDTO;
        }

        public async Task UpdateAsync(int id, UpdateBlogDTO blogDTO)
        {
            Blog blog = await _blogRepository.GetByIdAsync(id);

            if (blog == null) throw new Exception("Not Found");

            if (await _blogRepository.AnyAsync(b => b.Title == blogDTO.Title && b.Id != id)) throw new Exception("Already Exists");


            blog = _mapper.Map(blogDTO, blog);


            blog.UpdatedAt = DateTime.Now;

            _blogRepository.Update(blog);

            await _blogRepository.SaveChangesAsync();
        }


        public async Task SoftDelete(int id)
        {
            Blog blog = await _blogRepository.GetByIdAsync(id);

            if (blog == null) throw new Exception("Not Found");

            blog.IsDeleted = true;

            _blogRepository.Update(blog);
            await _blogRepository.SaveChangesAsync();
        }

    }
}
