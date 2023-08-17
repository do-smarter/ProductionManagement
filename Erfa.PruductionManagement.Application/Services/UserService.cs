using System.Security.Cryptography;
using System.Text;

namespace Erfa.PruductionManagement.Application.Services
{
    public class UserService
    {

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
    }
}
