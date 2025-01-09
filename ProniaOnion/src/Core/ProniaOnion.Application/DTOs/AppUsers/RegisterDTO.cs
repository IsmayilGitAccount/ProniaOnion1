using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnion.Application.DTOs.AppUsers
{
    public record RegisterDTO(
        string Name,
        string Surname,
        string UserName,
        string Email,
        string Password
        );
    
}
