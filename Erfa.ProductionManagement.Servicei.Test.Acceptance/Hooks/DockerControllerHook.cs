using BoDi;
using Ductus.FluentDocker.Builders;
using Ductus.FluentDocker.Services;
using Microsoft.Extensions.Configuration;
using System.Net;
using TechTalk.SpecFlow;

namespace Erfa.ProductionManagement.Servicei.Test.Acceptance.Hooks
{
    [Binding]
    public class DockerControllerHook
    {
        private ICompositeService _compositeService;
        private IObjectContainer _objectContainer;


        [BeforeTestRun] public void DockerComposeUp()
        {
            var config = LoadConfiguration();
            
            var dockerComposeFileName = config["DockerComposeFileName"];
            var dockerComposePath = GetDockerComposeLocation(dockerComposeFileName);

            var confirmationUrl = config["ProductionManagement.Api:BaseAddress"];

            _compositeService = new Builder()
                .UseContainer()
                .UseCompose()
                .FromFile(dockerComposePath)
                .RemoveOrphans()
                .WaitForHttp("webapi",$"{confirmationUrl}/v1/Catalog",
                    continuation: (response, _) => response.Code!= HttpStatusCode.OK ? 2000:0)
                .Build();

        }

        [AfterTestRun] public void DockerComposeDown() 
        {
            _compositeService.Stop();
            _compositeService.Dispose();

        }

        [BeforeScenario]
        public void AddHttpCLient()
        {
            var config = LoadConfiguration();
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri(config["ProductionManagement.Api:BaseAddress"])
            };
            _objectContainer.RegisterInstanceAs(httpClient);
                
        }


        
        

        private static IConfiguration LoadConfiguration()
    {
        return new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
    }
        private static string GetDockerComposeLocation(string dockerComposeFileName)
                {
        var directory = Directory.GetCurrentDirectory();
        while (!Directory.EnumerateFiles(directory, "*.yml").Any(s => s.EndsWith(dockerComposeFileName)) {
            directory = directory.Substring(0,directory.LastIndexOf(Path.DirectorySeparatorChar));
        }
        return Path.Combine(directory, dockerComposeFileName);
    }    }

    
   
}
