using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProniaOnion.Application.DTOs.Categories;
using ProniaOnion.Application.DTOs.Colors;
using ProniaOnion.Application.DTOs.Sizes;
using ProniaOnion.Application.DTOs.Tags;

namespace ProniaOnion.Application.DTOs.Products
{
    public record GetProductDTO(
        int Id,
        string Name,
        string SKU,
        decimal Price,
        string Description,
        CategoryItemDTO Category,
        IEnumerable<ColorItemDTO> Colors,
        IEnumerable<SizeItemDTO> Sizes,
        IEnumerable<TagItemDTO> Tags
        );
}
