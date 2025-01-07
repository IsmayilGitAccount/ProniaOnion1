using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProniaOnion.Application.Abstractions.Repositories;
using ProniaOnion.Application.Abstractions.Services;
using ProniaOnion.Application.DTOs.Categories;
using ProniaOnion.Application.DTOs.Colors;
using ProniaOnion.Domain.Entities;
using ProniaOnion.Persistence.Implementations.Repositories;

namespace ProniaOnion.Persistence.Implementations.Services
{
    internal class ColorService : IColorService
    {
        private readonly IColorRepository _colorRepository;
        private readonly IMapper _mapper;

        public ColorService(IColorRepository colorRepository, IMapper mapper)
        {
            _colorRepository = colorRepository;
            _mapper = mapper;
        }
        public async Task CreateAsync(CreateColorDTO colorDTO)
        {
            if (await _colorRepository.AnyAsync(c => c.Name == colorDTO.Name))
                throw new Exception("Already exist");

            var color = _mapper.Map<Color>(colorDTO);

            color.CreatedAt = DateTime.Now;
            color.UpdatedAt = DateTime.Now;

            await _colorRepository.AddAsync(color);
            await _colorRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var color = await _colorRepository.GetByIdAsync(id);

            if (color == null) throw new Exception("Not Found");

            _colorRepository.Delete(color);

            await _colorRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<ColorItemDTO>> GetAll(int page, int take)
        {
            IEnumerable<Color> colors = await _colorRepository
                .GetAll(skip: (page - 1) * take, take: take)
                .ToListAsync();

            return _mapper.Map<IEnumerable<ColorItemDTO>>(colors);


        }

        public async Task<GetColorDTO> GetByIdAsync(int id)
        {
            Color color = await _colorRepository.GetByIdAsync(id, nameof(Color.ProductColors));

            if (color == null) return null;

            GetColorDTO colorDTO = _mapper.Map<GetColorDTO>(color);

            return colorDTO;
        }

        public async Task UpdateAsync(int id, UpdateColorDTO colorDTO)
        {
            Color color = await _colorRepository.GetByIdAsync(id);

            if (color == null) throw new Exception("Not Found");

            if (await _colorRepository.AnyAsync(c => c.Name == colorDTO.Name && c.Id != id)) throw new Exception("Already exist");

            //color = _mapper.Map<Color>(colorDTO);

            color = _mapper.Map(colorDTO, color);

            color.UpdatedAt = DateTime.Now;

            _colorRepository.Update(color);

            await _colorRepository.SaveChangesAsync();
        }


        public async Task SoftDelete(int id)
        {
            Color color = await _colorRepository.GetByIdAsync(id);

            if (color == null) throw new Exception("Not Found");

            color.IsDeleted = true;

            _colorRepository.Update(color);
            await _colorRepository.SaveChangesAsync();
        }

    }
}
