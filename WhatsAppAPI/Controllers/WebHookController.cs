using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog;

namespace WhatsAppAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WebHookController : ControllerBase
{
    private const string WhatsAppVerificationToken = "1811";

    [HttpGet]
    public IActionResult WhatsApp([FromQuery(Name = "hub.mode")] string mode,
        [FromQuery(Name = "hub.verify_token")] string verifyToken,
        [FromQuery(Name = "hub.challenge")] string challenge) =>
        mode == "subscribe" && verifyToken == WhatsAppVerificationToken ? Ok(challenge) : BadRequest();

    [HttpPost]
    public async Task<IActionResult> WhatsApp([FromBody] object data)
    {
        var stringData = JsonConvert.SerializeObject(data);

        Log.Warning(stringData);
        return Ok(stringData);
    }
}
