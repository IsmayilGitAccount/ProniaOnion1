using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ProniaOnion.Application.DTOs.Authors;
using ProniaOnion.Application.DTOs.Blogs;
using ProniaOnion.Domain.Entities;

namespace ProniaOnion.Application.MappingProfile
{
    internal class BlogProfile:Profile
    {
        public BlogProfile()
        {
            CreateMap<Blog, GetBlogDTO>();
            CreateMap<Blog, BlogItemDTO>().ReverseMap();
            CreateMap<CreateBlogDTO, Blog>();
            CreateMap<UpdateBlogDTO, Blog>().ForMember(a => a.Id, opt => opt.Ignore());
        }
    }
}
