using Microsoft.AspNetCore.Mvc;
using Glimpse.Models;
using Glimpse.ViewModels;
using Microsoft.AspNetCore.Identity;
using Glimpse.Services;
using Microsoft.AspNetCore.Authorization;

namespace Glimpse.Controllers;

[Authorize]
public class EmailController : ControllerBase
{
    private readonly IEmailSender _emailSender;
    public EmailController(IEmailSender emailSender)
    {
        _emailSender = emailSender;
    }

    // [HttpPost("SendEmail")]
    // public IActionResult SendEmail([FromForm] EmailDto request)
    // {
    //     _emailSender.SendEmailAsync(request);
    //     if (_emailSender.SendEmailAsync(request))
    //     {
    //         return Ok();
    //     }
    //     return BadRequest("erro ao enviar email.");
    // }
}