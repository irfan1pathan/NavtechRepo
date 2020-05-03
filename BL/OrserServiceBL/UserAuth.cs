using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OrderManagementSystem.BL.IOrderServiceBL;
using OrderManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.BL.OrserServiceBL
{
    public class UserAuth:IUserAuth
    {
        private readonly navtechContext context;
        private readonly IConfiguration config;

        // private readonly IUserAuth auth;

        public UserAuth(navtechContext context,IConfiguration config)
        {
            this.context = context;
            this.config = config;
        }

        public object Authenticate(string username, string password)
        {
            var users = context.Users.SingleOrDefault(x => x.UserName == username && x.Password == password);
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(config["ApplicationSettings:JWT_Secret"].ToString());
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, users.UserName),
                    new Claim(ClaimTypes.Role, users.Role)
                }),
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            users.Token = tokenHandler.WriteToken(token);
            return users;

        }
    }
}
