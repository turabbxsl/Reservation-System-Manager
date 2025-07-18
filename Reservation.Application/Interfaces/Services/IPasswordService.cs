using Microsoft.Extensions.Configuration;

namespace Reservation.Application.Interfaces.Services
{
    public interface IPasswordService
    {
        string Encrypt(string plainText);
        string Decrypt(string cipherText);
        string GenerateJwtToken(string email, string role, IConfiguration _configuration);
    }
}
