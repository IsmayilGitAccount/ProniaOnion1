using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ProniaOnion.Application.DTOs.Colors;
using ProniaOnion.Application.DTOs.Sizes;
using ProniaOnion.Domain.Entities;

namespace ProniaOnion.Application.MappingProfile
{
    internal class SizeProfile:Profile  
    {
        public SizeProfile()
        {
            CreateMap<Size, SizeItemDTO>();
            CreateMap<Size, GetSizeDTO>().ReverseMap();
            CreateMap<CreateSizeDTO, Size>();
            CreateMap<UpdateSizeDTO, Size>().ForMember(c => c.Id, opt => opt.Ignore());
        }
    }
}
