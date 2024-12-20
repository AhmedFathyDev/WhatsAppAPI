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
    private static readonly HttpClient Client = new HttpClient();

    public async Task SendAsync(string phone, string body)
    {
        var request = new HttpRequestMessage(HttpMethod.Post,
            $"{metaSettings.Value.Url}/{metaSettings.Value.ApiVersion}/{metaSettings.Value.WhatsAppPhoneNumberId}/messages");

        request.SetBearerToken(metaSettings.Value.WhatsAppAccessToken);

        request.Content = new StringContent(JsonConvert.SerializeObject(new
        {
            messaging_product = "whatsapp",
            to = phone,
            type = "text",
            text = new {
                preview_url = true,
                body
            }
        }), Encoding.UTF8, "application/json");

        using var response = await Client.SendAsync(request);

        if (!response.IsSuccessStatusCode)
            throw new HttpRequestException($"Failed to send message to {phone}");
    }
}
