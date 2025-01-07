using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProniaOnion.Application.DTOs.Products;

namespace ProniaOnion.Application.DTOs.Categories
{
    public record GetCategoryDTO(int Id, string Name, ICollection<ProductItemDTO> Products);

}
