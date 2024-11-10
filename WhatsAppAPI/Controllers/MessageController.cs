using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WhatsAppAPI.Services;

namespace WhatsAppAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MessageController(IWhatsAppService service) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Send([FromQuery] string phone, [FromQuery] string body)
    {
        if (string.IsNullOrEmpty(phone))
            return BadRequest("The phone number cannot be null or empty.");

        try
        {
            await service.SendAsync(phone, body);
            return Ok("Message sent successfully...");
        }
        catch(Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }
}
