using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WhatsAppAPI.Services;

namespace WhatsAppAPI.Controllers;

/// <summary>
/// Controller for handling WhatsApp message-related requests.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class MessageController(IWhatsAppService service) : ControllerBase
{
    /// <summary>
    /// Sends a WhatsApp message to the specified phone number.
    /// </summary>
    /// <param name="phone">The phone number to send the message to.</param>
    /// <param name="body">The body of the message to be sent.</param>
    /// <returns>An <see cref="IActionResult"/> indicating the result of the operation.</returns>
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
