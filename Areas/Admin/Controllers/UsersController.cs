using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Graph;
using Microsoft.Identity.Client;
using Microsoft.Graph.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JeffPaulin.Areas.Admin.Controllers
{
    public class UsersController : Controller
    {
        public async Task<IActionResult> Index()
        {
            IConfidentialClientApplication confidentialClientApplication = ConfidentialClientApplicationBuilder
            .Create("c252e4e6-a73b-41b3-960e-9a3631795ebf")
            .WithTenantId("01e2a005-cb9f-4851-af17-3fa9abf4f485")
            .WithClientSecret("t84Nqapwj4Zt~.29zR-wK-zZ71Z~I8YT2T") // or .WithCertificate(certificate)
            .Build();

            AuthorizationCodeProvider authProvider = new AuthorizationCodeProvider(confidentialClientApplication);

            GraphServiceClient graphClient = new GraphServiceClient(authProvider);

            var users = await graphClient.Users
                .Request()
                .Select("displayName,id")
                .GetAsync();

            return View(users);
        }
    }
}
