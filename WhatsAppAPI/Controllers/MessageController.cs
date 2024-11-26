using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WhatsAppAPI.Services;

namespace WhatsAppAPI.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
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

    [HttpPost]
    public async Task<IActionResult> SendToList([FromQuery] List<string> phones, [FromQuery] string body)
    {
        if (phones == null || phones.Count == 0 || phones.Any(string.IsNullOrEmpty))
            return BadRequest("The phone numbers list cannot be null or have any phone number null or empty.");

        var failedNumbers = new List<string>();

        foreach (var phone in phones)
        {
            try
            {
                await service.SendAsync(phone, body);
            }
            catch
            {
                failedNumbers.Add(phone);
            }
        }

        if (failedNumbers.Count > 0)
            return BadRequest($"Failed to send message to the following numbers: {string.Join(", ", failedNumbers)}");

        return Ok("Messages sent successfully...");
    }
}
