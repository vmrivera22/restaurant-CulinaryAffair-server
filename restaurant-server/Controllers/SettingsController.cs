using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
        var publicAuth = new PublicAuth()
        {
            Audience = _configuration.GetValue<string>("ClientAuth:Audience"),
            Domain = _configuration.GetValue<string>("ClientAuth:Domain"),
            ClientId = _configuration.GetValue<string>("ClientAuth:ClientId")
        };
        return Ok(publicAuth);
    }

}
