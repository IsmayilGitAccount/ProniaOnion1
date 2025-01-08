using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnion.Application.DTOs.Products
{
    public record UpdateProductDTO(
        string Name,
        string SKU,
        string Description,
        decimal Price,
        int CategoryId,
        ICollection<int> ColorsIds);
    
}
