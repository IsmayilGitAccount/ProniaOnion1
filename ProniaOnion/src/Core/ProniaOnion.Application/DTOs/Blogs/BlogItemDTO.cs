using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnion.Application.DTOs.Blogs
{
    public record BlogItemDTO(int Id, string Title, string Article, string Image, int AuthorId, int GenreId)
    {
    }
}
