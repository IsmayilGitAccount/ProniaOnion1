using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using ProniaOnion.Application.DTOs.Sizes;
using ProniaOnion.Application.DTOs.Tags;
using ProniaOnion.Domain.Entities;

namespace ProniaOnion.Application.MappingProfile
{
    internal class TagProfile:Profile
    {
        public TagProfile()
        {
            CreateMap<Tag, TagItemDTO>();
            CreateMap<Tag, GetTagDTO>().ReverseMap();
            CreateMap<CreateTagDTO, Tag>();
            CreateMap<UpdateTagDTO, Tag>().ForMember(c => c.Id, opt => opt.Ignore());
        }
    }
}
