using Microsoft.AspNetCore.Mvc;

namespace Erfa.PruductionManagement.Api.Controllers.V2
{
    public class ApiHeaders
    {
        [FromHeader]
        public string UserName { get; set; }
    }
}
