using Microsoft.AspNetCore.Mvc;

namespace Erfa.PruductionManagement.Api.Controllers.V1
{
    public class ApiHeaders
    {
        [FromHeader]
        public string UserName { get; set; }
    }
}
