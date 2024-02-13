using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using restaurant_server.Data;
using restaurant_server.Entities;

namespace restaurant_server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SettingsController : ControllerBase
{ 
    private readonly IConfiguration _configuration;
    public SettingsController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpGet("auth")]
    public ActionResult<PublicAuth> GetPublicAuth()
    {
        var keyVaultEndpoint = new Uri(_configuration["VaultKey"]);
        var secretClient = new SecretClient(keyVaultEndpoint, new DefaultAzureCredential());

        KeyVaultSecret audience = secretClient.GetSecret("authaudience");
        KeyVaultSecret domain = secretClient.GetSecret("authdomain");
        KeyVaultSecret clientid = secretClient.GetSecret("authidC");
        var publicAuth = new PublicAuth()
        {
            Audience = audience.Value,
            Domain = domain.Value,
            ClientId = clientid.Value
        };
        return Ok(publicAuth);
    }

}
