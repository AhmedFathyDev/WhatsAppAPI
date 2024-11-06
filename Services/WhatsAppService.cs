using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using IdentityModel.Client;
using Newtonsoft.Json;
using WhatsAppAPI.Models;

namespace WhatsAppAPI.Services;

public class WhatsAppService(IOptions<MetaSettings> metaSettings) : IWhatsAppService
{
    public async Task SendAsync(string phone)
    {
        using var request = new HttpRequestMessage(HttpMethod.Post,
            $"{metaSettings.Value.Url}/v21.0/{metaSettings.Value.PhoneNumberId}/messages");

        request.SetBearerToken(metaSettings.Value.AccessToken);
        request.Content = new StringContent(JsonConvert.SerializeObject(new
        {
            messaging_product = "whatsapp", to = phone, type = "template",
            template = new { name = "hello_world", language = new { code = "en_US" } }
        }), Encoding.UTF8, "application/json");

        using var client = new HttpClient();
        using var response = await client.SendAsync(request);

        if (response is { IsSuccessStatusCode: false })
            throw new("Failed to send message...");
    }
}
