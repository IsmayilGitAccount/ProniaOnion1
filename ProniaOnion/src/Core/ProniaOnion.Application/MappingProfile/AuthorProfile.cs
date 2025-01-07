using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ProniaOnion.Application.DTOs.Authors;
using ProniaOnion.Domain.Entities;

namespace ProniaOnion.Application.MappingProfile
{
    internal class AuthorProfile:Profile    
    {
        public AuthorProfile()  
        {
            CreateMap<Author, GetAuthorDTO>();
            CreateMap<Author, AuthorItemDTO>().ReverseMap();
            CreateMap<CreateAuthorDTO, Author>();
            CreateMap<UpdateAuthorDTO, Author>().ForMember(a=>a.Id, opt=>opt.Ignore());
        }
    }
}
