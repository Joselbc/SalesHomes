using Microsoft.IdentityModel.Tokens;
using SalesHomes.Entitys;
using SalesHomes.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace SalesHomes.Services
{
    public class JwtAuthManagerService
    {
        private readonly string _key;
        private readonly DBExamenEntities1 _db;

        public JwtAuthManagerService(string key)
        {
            _key = key;
            _db = new DBExamenEntities1();
        }

        public bool ValidacionUsuario(AccesoUsuario acceso)
        {
            _ = acceso == null ? throw new ArgumentNullException(nameof(acceso)) : acceso;
            _ = acceso.Usuario == null ? throw new ArgumentNullException(nameof(acceso)) : acceso;
            _ = acceso.Contraseña == null ? throw new ArgumentNullException(nameof(acceso)) : acceso;

            string adminUser = _db.AdministradorITM.Where(admin => admin.Usuario == acceso.Usuario && admin.Clave == acceso.Contraseña).
                                    Select(admin => admin.Clave).FirstOrDefault();

            return string.IsNullOrEmpty(adminUser) ? false : true;

        }

        public string GenerateToken(string username)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username)
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "SalesHomes",
                audience: "SalesHomes",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public ClaimsPrincipal ValidateToken(string token)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    IssuerSigningKey = securityKey,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = "SalesHomes",
                    ValidAudience = "SalesHomes"
                }, out SecurityToken validatedToken);
                return principal;
            }
            catch
            {
                return null;
            }
        }
    }
}