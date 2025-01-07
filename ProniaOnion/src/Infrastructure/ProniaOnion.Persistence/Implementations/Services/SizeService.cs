using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProniaOnion.Application.Abstractions.Repositories;
using ProniaOnion.Application.Abstractions.Services;
using ProniaOnion.Application.DTOs.Colors;
using ProniaOnion.Application.DTOs.Sizes;
using ProniaOnion.Domain.Entities;
using ProniaOnion.Persistence.Implementations.Repositories;

namespace ProniaOnion.Persistence.Implementations.Services
{
    internal class SizeService : ISizeService
    {
        private readonly ISizeRepository _sizeRepository;
        private readonly IMapper _mapper;

        public SizeService(ISizeRepository sizeRepository, IMapper mapper)
        {
            _sizeRepository = sizeRepository;
            _mapper = mapper;
        }
        public async Task CreateAsync(CreateSizeDTO sizeDTO)
        {
            if (await _sizeRepository.AnyAsync(s => s.Name == sizeDTO.Name))
                throw new Exception("Already exist");

            var size = _mapper.Map<Size>(sizeDTO);

            size.CreatedAt = DateTime.Now;
            size.UpdatedAt = DateTime.Now;

            await _sizeRepository.AddAsync(size);
            await _sizeRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var size = await _sizeRepository.GetByIdAsync(id);

            if (size == null) throw new Exception("Not Found");

            _sizeRepository.Delete(size);

            await _sizeRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<SizeItemDTO>> GetAllAsync(int page, int take)
        {
            IEnumerable<Size> sizes = await _sizeRepository
                .GetAll(skip: (page - 1) * take, take: take)
                .ToListAsync();

            return _mapper.Map<IEnumerable<SizeItemDTO>>(sizes);
        }

        public async Task<GetSizeDTO> GetByIdAsync(int id)
        {
            Size size = await _sizeRepository.GetByIdAsync(id);

            if (size == null) return null;

            GetSizeDTO sizeDTO = _mapper.Map<GetSizeDTO>(size);

            return sizeDTO;
        }

        public async Task UpdateAsync(int id, UpdateSizeDTO sizeDTO)
        {
            Size size = await _sizeRepository.GetByIdAsync(id);

            if (size == null) throw new Exception("Not Found");

            if (await _sizeRepository.AnyAsync(s => s.Name == sizeDTO.Name && s.Id != id)) throw new Exception("Already exist");

            size = _mapper.Map(sizeDTO, size);

            size.UpdatedAt = DateTime.Now;

            _sizeRepository.Update(size);

            await _sizeRepository.SaveChangesAsync();
        }


        public async Task SoftDelete(int id)
        {
            Size size = await _sizeRepository.GetByIdAsync(id);

            if (size == null) throw new Exception("Not Found");

            size.IsDeleted = true;

            _sizeRepository.Update(size);
            await _sizeRepository.SaveChangesAsync();
        }

    }
}
