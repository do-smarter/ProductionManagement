using System.IdentityModel.Tokens.Jwt;

namespace Erfa.PruductionManagement.Api.Middlewares
{
    public class JwtHeaderMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtHeaderMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var tokenString = context.Request.Cookies["X-Access-Token"].ToString();
            JwtSecurityToken token = new JwtSecurityToken(tokenString);
            var userNameClaim = token.Claims.Where(c => c.Type.Equals("UserName"))
                                .FirstOrDefault().Value;
            context.Request.Headers.Add("UserName", userNameClaim);
            await _next(context);
        }
    }
}
