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
using ProniaOnion.Application.DTOs.Categories;
using ProniaOnion.Domain.Entities;
using ProniaOnion.Persistence.Implementations.Repositories;

namespace ProniaOnion.Persistence.Implementations.Services
{
    internal class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        public AuthorService(IAuthorRepository authorRepository, IMapper mapper)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }
        public async Task CreateAsync(CreateAuthorDTO authorDTO)
        {
            if (await _authorRepository.AnyAsync(a => a.Name == authorDTO.Name))
                throw new Exception("Already exist");

            var author = _mapper.Map<Author>(authorDTO);

            author.CreatedAt = DateTime.Now;
            author.UpdatedAt = DateTime.Now;

            await _authorRepository.AddAsync(author);

            await _authorRepository.SaveChangesAsync();

        }

        public async Task DeleteAsync(int id)
        {
            Author author = await _authorRepository.GetByIdAsync(id);

            if (author == null) throw new Exception("Not Found");
            _authorRepository.Delete(author);

            await _authorRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<AuthorItemDTO>> GetAllAsync(int page, int take)
        {
            IEnumerable<Author> authores = await _authorRepository
               .GetAll(skip: (page - 1) * take, take: take)
               .ToListAsync();

            return _mapper.Map<IEnumerable<AuthorItemDTO>>(authores);
        }

        public async Task<GetAuthorDTO> GetByIdAsync(int id)
        {
            Author author = await _authorRepository.GetByIdAsync(id);

            if (author == null) return null;

            GetAuthorDTO authorDTO = _mapper.Map<GetAuthorDTO>(author);

            return authorDTO;
        }

        public async Task UpdateAsync(int id, UpdateAuthorDTO authorDTO)
        {
            Author author = await _authorRepository.GetByIdAsync(id);

            if (author == null) throw new Exception("Not Found");

            if (await _authorRepository.AnyAsync(a => a.Name == authorDTO.Name && a.Id != id)) throw new Exception("Already Exists");


            author = _mapper.Map(authorDTO, author);


            author.UpdatedAt = DateTime.Now;

            _authorRepository.Update(author);

            await _authorRepository.SaveChangesAsync();
        }

        public async Task SoftDelete(int id)
        {
            Author author = await _authorRepository.GetByIdAsync(id);

            if (author == null) throw new Exception("Not Found");

            author.IsDeleted = true;

            _authorRepository.Update(author);
            await _authorRepository.SaveChangesAsync();
        }
    }
}
