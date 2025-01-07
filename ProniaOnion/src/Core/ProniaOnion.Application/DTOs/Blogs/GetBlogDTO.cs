using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProniaOnion.Application.DTOs.Genres;
using ProniaOnion.Domain.Entities;

namespace ProniaOnion.Application.DTOs.Blogs
{
    public record GetBlogDTO(int Id, string Title, string Article, string Image, int AuthorId, int GenreId)
    {
    }
}
