using System.Collections.Generic;
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
    private readonly Mock<IWhatsAppService> _mockService;
    private readonly MessageController _controller;

    public MessageControllerTests()
    {
        _mockService = new();
        _controller = new(_mockService.Object);
    }

    [Fact]
    public async Task Send_ValidPhone_ReturnsOk()
    {
        string phone = "201208731604";
        string body = "Test message.";

        _mockService.Setup(service => service.SendAsync(phone, body)).Returns(Task.CompletedTask);

        var result = await _controller.Send(phone, body);
        var okResult = Assert.IsType<OkObjectResult>(result);

        Assert.Equal("Message sent successfully...", okResult.Value);
    }
    [Fact]
    public async Task Send_NotValidPhone_ReturnsBadRequest()
    {
        string phone = "1234567890";
        string body = "Test message.";

        _mockService.Setup(service => service.SendAsync(phone, body))
            .Throws(new HttpRequestException($"Failed to send message to {phone}"));

        var result = await _controller.Send(phone, body);

        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);

        Assert.Equal($"Failed to send message to {phone}", badRequestResult.Value);
    }

    [Fact]
    public async Task Send_NullPhone_ReturnsBadRequest()
    {
        string phone = null;
        string body = "Test message.";

        var result = await _controller.Send(phone, body);

        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);

        Assert.Equal("The phone number cannot be null or empty.", badRequestResult.Value);
    }
    [Fact]
    public async Task Send_EmptyPhone_ReturnsBadRequest()
    {
        string phone = string.Empty;
        string body = "Test message.";

        var result = await _controller.Send(phone, body);

        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);

        Assert.Equal("The phone number cannot be null or empty.", badRequestResult.Value);
    }

    [Fact]
    public async Task SendToList_ValidPhones_ReturnsOk()
    {
        List<string> phones = ["201208731604", "201208731604"];
        string body = "Test message.";

        _mockService.Setup(service => service.SendAsync(phones[0], body)).Returns(Task.CompletedTask);
        _mockService.Setup(service => service.SendAsync(phones[1], body)).Returns(Task.CompletedTask);

        var result = await _controller.SendToList(phones, body);
        var okResult = Assert.IsType<OkObjectResult>(result);

        Assert.Equal("Messages sent successfully...", okResult.Value);
    }
    [Fact]
    public async Task SendToList_NotValidPhone_ReturnsBadRequest()
    {
        List<string> phones = ["1234567890", "201208731604"];
        string body = "Test message.";

        _mockService.Setup(service => service.SendAsync(phones[0], body))
            .Throws(new HttpRequestException($"Failed to send message to {phones[0]}"));
        _mockService.Setup(service => service.SendAsync(phones[1], body)).Returns(Task.CompletedTask);

        var result = await _controller.SendToList(phones, body);

        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);

        Assert.Equal($"Failed to send message to the following numbers: {phones[0]}", badRequestResult.Value);
    }

    [Fact]
    public async Task SendToList_NullPhones_ReturnsBadRequest()
    {
        List<string> phones = null;
        string body = "Test message.";

        var result = await _controller.SendToList(phones, body);

        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);

        Assert.Equal("The phone numbers list cannot be null or have any phone number null or empty.",
            badRequestResult.Value);
    }
    [Fact]
    public async Task SendToList_NullPhone_ReturnsBadRequest()
    {
        List<string> phones = [null, "201208731604"];
        string body = "Test message.";

        var result = await _controller.SendToList(phones, body);

        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);

        Assert.Equal("The phone numbers list cannot be null or have any phone number null or empty.",
            badRequestResult.Value);
    }
    [Fact]
    public async Task SendToList_EmptyPhone_ReturnsBadRequest()
    {
        List<string> phones = [string.Empty, "201208731604"];
        string body = "Test message.";

        var result = await _controller.SendToList(phones, body);

        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);

        Assert.Equal("The phone numbers list cannot be null or have any phone number null or empty.",
            badRequestResult.Value);
    }
}
