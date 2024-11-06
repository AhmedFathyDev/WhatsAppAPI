using System.Threading.Tasks;

namespace WhatsAppAPI.Services;

public interface IWhatsAppService
{
    Task SendAsync(string phone);
}
