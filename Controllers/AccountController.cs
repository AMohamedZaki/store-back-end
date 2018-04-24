using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using Store.data.Model;
using Store.data.dto;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace store_back_end.api
{
    [Route("[controller]/[action]")]
    [EnableCors("AllowAnyOrigin")]
    [AllowAnonymous]
    public class AccountController : Controller
    {

        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManger;
        private readonly IConfiguration _configuration;
        public AccountController(SignInManager<IdentityUser> SignInManager,
        UserManager<IdentityUser> UserManger,
        IConfiguration Configuration
        )
        {
            _signInManager = SignInManager;
            _userManger = UserManger;
            _configuration = Configuration;
        }

        [HttpPost]
        public async Task<object> Login([FromBody] Logindto userdto)
        {
            var result = await _signInManager
                        .PasswordSignInAsync(userdto.Email, userdto.PassWord, false, false);

            if (result.Succeeded)
            {
                IdentityUser user = _userManger.Users.FirstOrDefault(item => item.Email == userdto.Email);
                return await Task.Run(() => GenerateJwtToken(userdto.Email, user));
            }
            throw new ApplicationException("INVALID_LOGIN_ATTEMPT");
        }
        
        [HttpPost]
        public async Task<object> Regiester([FromBody] Regestrationdto regestdto)
        {
            if (regestdto != null)
            {
                var user = new IdentityUser
                {
                    Email = regestdto.Email,
                    UserName = regestdto.userName
                };
                var userapp = await _userManger.CreateAsync(user, regestdto.Password);
                if (userapp.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    return await Task.Run(() => Ok(GenerateJwtToken(regestdto.Email, user)));
                }
            }
            throw new ApplicationException("UNKNOWN_ERROR");
        }

        
        private async Task<object> GenerateJwtToken(string email, IdentityUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JwtExpireDays"]));

            var token = new JwtSecurityToken(
                _configuration["JwtIssuer"],
                _configuration["JwtIssuer"],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return await Task.Run(() => new JwtSecurityTokenHandler().WriteToken(token));
        }

    }
}