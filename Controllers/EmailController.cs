using Microsoft.AspNetCore.Mvc;
using Glimpse.Models;
using Glimpse.ViewModels;
using Microsoft.AspNetCore.Identity;
using Glimpse.Services;

namespace Glimpse.Controllers;

public class EmailController : ControllerBase
{
    private readonly IEmailService _emailService;
    public EmailController(IEmailService emailService)
    {
        _emailService = emailService;
    }

    [HttpPost("SendEmail")]
    public IActionResult SendEmail([FromForm] EmailDto request)
    {
        if (_emailService.SendEmail(request))
        {
            return Ok();
        }
        return BadRequest("erro ao enviar email.");
    }
}