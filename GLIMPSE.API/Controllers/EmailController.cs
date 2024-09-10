using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Text.Json;
using GLIMPSE.Domain.Models;
using GLIMPSE.Domain.Services;

namespace GLIMPSE.API.Controllers;

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