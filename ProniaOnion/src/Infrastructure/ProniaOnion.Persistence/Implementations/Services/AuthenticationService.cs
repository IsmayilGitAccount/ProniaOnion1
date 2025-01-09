using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProniaOnion.Application.Abstractions.Services;
using ProniaOnion.Application.DTOs.AppUsers;
using ProniaOnion.Domain.Entities;

namespace ProniaOnion.Persistence.Implementations.Services
{
    internal class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public AuthenticationService(UserManager<AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task LoginAsync(LoginDTO loginDTO)
        {
            AppUser? user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == loginDTO.UserNameofEmail || u.Email == loginDTO.UserNameofEmail);

            if (user == null)
                throw new Exception("Username, Email or Password is wrong!");

            bool result = await _userManager.CheckPasswordAsync(user, loginDTO.Password);

            if (!result)
            {
                user.AccessFailedCount++;
                throw new Exception("Username, Email or Password is wrong!");
            }
        }

        public async Task RegisterAsync(RegisterDTO registerDTO)
        {
            if (await _userManager.Users.AnyAsync(u => u.UserName == registerDTO.UserName || u.Email == registerDTO.Email))
                throw new Exception("User already exist");

           var result = await _userManager.CreateAsync(_mapper.Map<AppUser>(registerDTO), registerDTO.Password);

            if(!result.Succeeded)
            {
                StringBuilder stringBuilder = new StringBuilder();

                foreach (var error in result.Errors)
                {
                    stringBuilder.AppendLine(error.Description);
                }

                throw new Exception(stringBuilder.ToString());
            }
        }
    }
}
