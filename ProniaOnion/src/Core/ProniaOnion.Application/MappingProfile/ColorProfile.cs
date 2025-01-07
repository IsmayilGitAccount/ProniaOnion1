using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ProniaOnion.Application.DTOs.Colors;
using ProniaOnion.Domain.Entities;

namespace ProniaOnion.Application.MappingProfile
{
    internal class ColorProfile:Profile
    {
        public ColorProfile()
        {
            CreateMap<Color, ColorItemDTO>();
            CreateMap<Color, GetColorDTO>().ReverseMap();
            CreateMap<CreateColorDTO, Color>();
            CreateMap<UpdateColorDTO, Color>().ForMember(c=>c.Id, opt=>opt.Ignore());
        }
    }
}
