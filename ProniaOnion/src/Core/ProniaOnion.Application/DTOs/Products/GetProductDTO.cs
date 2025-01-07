using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProniaOnion.Application.DTOs.Categories;
using ProniaOnion.Application.DTOs.Colors;

namespace ProniaOnion.Application.DTOs.Products
{
    public record GetProductDTO(
        int Id,
        string Name,
        string SKU,
        string Description,
        CategoryItemDTO Category,
        IEnumerable<ColorItemDTO> Colors
        );
}
