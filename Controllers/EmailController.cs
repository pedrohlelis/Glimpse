using Microsoft.AspNetCore.Mvc;
using Glimpse.Models;
using Glimpse.ViewModels;
using Microsoft.AspNetCore.Identity;
using Glimpse.Services;

namespace Glimpse.Controllers;

public class EmailController : ControllerBase
{
    private readonly IEmailSender _emailSender;
    public EmailController(IEmailSender emailSender)
    {
        _emailSender = emailSender;
    }

    [HttpPost("SendEmail")]
    public IActionResult SendEmail([FromForm] EmailDto request)
    {
        if (_emailSender.SendEmail(request))
        {
            return Ok();
        }
        return BadRequest("erro ao enviar email.");
    }
}