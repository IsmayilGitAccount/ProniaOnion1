using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProniaOnion.Application.Abstractions.Repositories;
using ProniaOnion.Application.Abstractions.Services;
using ProniaOnion.Application.DTOs.Sizes;
using ProniaOnion.Application.DTOs.Tags;
using ProniaOnion.Domain.Entities;
using ProniaOnion.Persistence.Implementations.Repositories;

namespace ProniaOnion.Persistence.Implementations.Services
{
    internal class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;
        private readonly IMapper _mapper;

        public TagService(ITagRepository tagRepository, IMapper mapper)
        {
            _tagRepository = tagRepository;
            _mapper = mapper;
        }
        public async Task CreateAsync(CreateTagDTO tagDTO)
        {
            if (await _tagRepository.AnyAsync(t => t.Name == tagDTO.Name))
                throw new Exception("Already exist");

            var tag = _mapper.Map<Tag>(tagDTO);

            tag.CreatedAt = DateTime.Now;
            tag.UpdatedAt = DateTime.Now;

            await _tagRepository.AddAsync(tag);
            await _tagRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var tag = await _tagRepository.GetByIdAsync(id);

            if (tag == null) throw new Exception("Not Found");

            _tagRepository.Delete(tag);

            await _tagRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<TagItemDTO>> GetAllAsync(int page, int take)
        {
            IEnumerable<Tag> tags = await _tagRepository
                 .GetAll(skip: (page - 1) * take, take: take)
                 .ToListAsync();

            return _mapper.Map<IEnumerable<TagItemDTO>>(tags);
        }

        public async Task<GetTagDTO> GetByIdAsync(int id)
        {
            Tag tag = await _tagRepository.GetByIdAsync(id);

            if (tag == null) return null;

            GetTagDTO tagDTO = _mapper.Map<GetTagDTO>(tag);

            return tagDTO;
        }

        public async Task UpdateAsync(int id, UpdateTagDTO tagDTO)
        {
            Tag tag = await _tagRepository.GetByIdAsync(id);

            if (tag == null) throw new Exception("Not Found");

            if (await _tagRepository.AnyAsync(t => t.Name == tagDTO.Name && t.Id != id)) throw new Exception("Already exist");

            tag = _mapper.Map(tagDTO, tag);

            tag.UpdatedAt = DateTime.Now;

            _tagRepository.Update(tag);

            await _tagRepository.SaveChangesAsync();
        }

        public async Task SoftDelete(int id)
        {
            Tag tag = await _tagRepository.GetByIdAsync(id);

            if (tag == null) throw new Exception("Not Found");

            tag.IsDeleted = true;

            _tagRepository.Update(tag);
            await _tagRepository.SaveChangesAsync();
        }

    }
}
