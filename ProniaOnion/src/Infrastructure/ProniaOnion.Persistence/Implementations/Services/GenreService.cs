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
using ProniaOnion.Application.DTOs.Genres;
using ProniaOnion.Domain.Entities;
using ProniaOnion.Persistence.Implementations.Repositories;

namespace ProniaOnion.Persistence.Implementations.Services
{
    internal class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IMapper _mapper;

        public GenreService(IGenreRepository genreRepository, IMapper mapper)
        {
            _genreRepository = genreRepository;
            _mapper = mapper;
        }
        public async Task CreateAsync(CreateGenreDTO genreDTO)
        {
            if (await _genreRepository.AnyAsync(g => g.Name == genreDTO.Name))
                throw new Exception("Already exist");

            var genre = _mapper.Map<Genre>(genreDTO);

            genre.CreatedAt = DateTime.Now;
            genre.UpdatedAt = DateTime.Now;

            await _genreRepository.AddAsync(genre);

            await _genreRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Genre genre = await _genreRepository.GetByIdAsync(id);

            if (genre == null) throw new Exception("Not Found");
            _genreRepository.Delete(genre);

            await _genreRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<GenreItemDTO>> GetAllAsync(int page, int take)
        {
            IEnumerable<Genre> genres = await _genreRepository
              .GetAll(skip: (page - 1) * take, take: take)
              .ToListAsync();

            return _mapper.Map<IEnumerable<GenreItemDTO>>(genres);
        }

        public async Task<GetGenreDTO> GetByIdAsync(int id)
        {
            Genre genre = await _genreRepository.GetByIdAsync(id);

            if (genre == null) return null;

            GetGenreDTO genreDTO = _mapper.Map<GetGenreDTO>(genre);

            return genreDTO;
        }

        public async Task UpdateAsync(int id, UpdateGenreDTO genreDTO)
        {
            Genre genre = await _genreRepository.GetByIdAsync(id);

            if (genre == null) throw new Exception("Not Found");

            if (await _genreRepository.AnyAsync(g => g.Name == genreDTO.Name && g.Id != id)) throw new Exception("Already Exists");


            genre = _mapper.Map(genreDTO, genre);


            genre.UpdatedAt = DateTime.Now;

            _genreRepository.Update(genre);

            await _genreRepository.SaveChangesAsync();
        }


        public async Task SoftDelete(int id)
        {
            Genre genre = await _genreRepository.GetByIdAsync(id);

            if (genre == null) throw new Exception("Not Found");

            genre.IsDeleted = true;

            _genreRepository.Update(genre);
            await _genreRepository.SaveChangesAsync();
        }

    }
}
