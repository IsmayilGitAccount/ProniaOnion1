using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ProniaOnion.Application.DTOs.Authors;
using ProniaOnion.Application.DTOs.Genres;
using ProniaOnion.Domain.Entities;

namespace ProniaOnion.Application.MappingProfile
{
    internal class GenreProfile:Profile
    {
        public GenreProfile()
        {
            CreateMap<Genre, GetGenreDTO>();
            CreateMap<Genre, GenreItemDTO>().ReverseMap();
            CreateMap<CreateGenreDTO, Genre>();
            CreateMap<UpdateGenreDTO, Genre>().ForMember(a => a.Id, opt => opt.Ignore());
        }
    }
}
