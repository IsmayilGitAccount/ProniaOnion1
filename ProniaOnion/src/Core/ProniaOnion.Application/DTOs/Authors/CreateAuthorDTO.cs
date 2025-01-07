using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnion.Application.DTOs.Authors
{
    public record CreateAuthorDTO(string Name, string Surname, string ProfileImage)
    {
    }
}
