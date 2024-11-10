using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WhatsAppAPI.Controllers;
using WhatsAppAPI.Services;
using Xunit;

namespace WhatsAppAPI.Test;

public class MessageControllerTests
{
    [Fact]
    public async Task Send_ReturnsOkResult_WhenMessageIsSentSuccessfully()
    {
        var mockService = new Mock<IWhatsAppService>();
        mockService.Setup(service => service.SendAsync(It.IsAny<string>(), It.IsAny<string>()))
            .Returns(Task.CompletedTask);

        var controller = new MessageController(mockService.Object);

        var result = await controller.Send("1234567890", "Hello");
        var okResult = Assert.IsType<OkObjectResult>(result);

        Assert.Equal("Message sent successfully...", okResult.Value);
    }

    [Fact]
    public async Task Send_ReturnsBadRequest_WhenPhoneIsNull()
    {
        var mockService = new Mock<IWhatsAppService>();

        var controller = new MessageController(mockService.Object);

        var result = await controller.Send(null, "Hello");
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);

        Assert.Equal("The phone number cannot be null or empty.", badRequestResult.Value);
    }

    [Fact]
    public async Task Send_ReturnsBadRequest_WhenExceptionIsThrown()
    {
        var mockService = new Mock<IWhatsAppService>();
        mockService.Setup(service => service.SendAsync(It.IsAny<string>(), It.IsAny<string>()))
            .Throws(new HttpRequestException("Service error"));

        var controller = new MessageController(mockService.Object);

        var result = await controller.Send("1234567890", "Hello");
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);

        Assert.Equal("Service error", badRequestResult.Value);
    }
}
