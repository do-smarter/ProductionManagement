
using Microsoft.Extensions.Primitives;

namespace Erfa.PruductionManagement.Api.Controllers
{
    public static class Utils
    {
        internal static string GetUserName(HttpRequest request)
        {
            string userName = "";

            const string HeaderKeyName = "UserName";
            if (request.Headers.TryGetValue(HeaderKeyName, out StringValues headerValue))
            {
                userName = headerValue.ToString();
            }
            return userName;
        }
    }
}
