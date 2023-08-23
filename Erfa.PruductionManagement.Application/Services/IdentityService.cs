using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Erfa.PruductionManagement.Application.Services
{
    public class IdentityService
    {
        private readonly IConfiguration _configuration;
        public IdentityService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        internal string GenerateRegCode()
        {
            StringBuilder sb = new StringBuilder();
            for (int n = 0; n < 6; n++)
            {
                sb = sb.Append(GenerateChar());
            }
            return sb.ToString();
        }

        internal char GenerateChar()
        {
            string CapitalLetters = "QWERTYUIOPASDFGHJKLZXCVBNM";
            string SmallLetters = "qwertyuiopasdfghjklzxcvbnm";
            string Digits = "0123456789";
            string AllChar = CapitalLetters + SmallLetters + Digits;

            using (var cryptoProvider = new RNGCryptoServiceProvider())
            {
                var byteArray = new byte[1];
                char c;
                do
                {
                    cryptoProvider.GetBytes(byteArray);
                    c = (char)byteArray[0];

                } while (!AllChar.Any(x => x == c));

                return c;
            }
        }
        internal string HashString(string text)
        {
            string salt = _configuration["RegCode:Salt"];
            if (String.IsNullOrEmpty(text))
            {
                throw new ArgumentException("Invalid string");
            }

            // Uses SHA256 to create the hash
            using (var sha = new SHA256Managed())
            {
                // Convert the string to a byte array first, to be processed
                byte[] textBytes = Encoding.UTF8.GetBytes(text + salt);
                byte[] hashBytes = sha.ComputeHash(textBytes);

                // Convert back to a string, removing the '-' that BitConverter adds
                string hash = BitConverter
                    .ToString(hashBytes)
                    .Replace("-", String.Empty);

                return hash;
            }
        }
        internal JwtSecurityToken GetToken(List<Claim> claims)
        {
            var now = DateTime.Now;

            claims.Add(new Claim("iat", now.ToString()));


            var authsigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: now.AddMinutes(30),
                notBefore: now,
                claims: claims,
                signingCredentials: new SigningCredentials(authsigningKey, SecurityAlgorithms.HmacSha256)
                );
            return token;
        }
    }
}
