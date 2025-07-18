using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Reservation.Application.Interfaces.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Reservation.Application.Services
{
    public class PasswordService : IPasswordService
    {
        private static readonly string EncryptionKey = "MAKV2SPBNI99212";

        public string Encrypt(string plainText)
        {
            byte[] clearBytes = Encoding.Unicode.GetBytes(plainText);
            using var aes = Aes.Create();
            var pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
            0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64,
            0x76, 0x65, 0x64, 0x65, 0x76 });
            aes.Key = pdb.GetBytes(32);
            aes.IV = pdb.GetBytes(16);
            using var ms = new MemoryStream();
            using var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(clearBytes, 0, clearBytes.Length);
            cs.Close();
            return Convert.ToBase64String(ms.ToArray());
        }

        public string Decrypt(string cipherText)
        {
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using var aes = Aes.Create();
            var pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
            0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64,
            0x76, 0x65, 0x64, 0x65, 0x76 });
            aes.Key = pdb.GetBytes(32);
            aes.IV = pdb.GetBytes(16);
            using var ms = new MemoryStream();
            using var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(cipherBytes, 0, cipherBytes.Length);
            cs.Close();
            return Encoding.Unicode.GetString(ms.ToArray());
        }

        public string GenerateJwtToken(string email, string role, IConfiguration _configuration)
        {
            try
            {
                var claims = new[]
    {
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, role),
            };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddHours(8),
                    signingCredentials: creds
                );

                var to = token.ToString();

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                throw;
            }
    
        }
    }

}
