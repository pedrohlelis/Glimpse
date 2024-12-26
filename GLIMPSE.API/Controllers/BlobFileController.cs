using GLIMPSE.Application.Dtos;
using GLIMPSE.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GLIMPSE.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class BlobFileController : ControllerBase
{
    private readonly IBlobFileApplicationService blobFileApplicationService;

    public BlobFileController(IBlobFileApplicationService blobFileApplicationService)
    {
        this.blobFileApplicationService = blobFileApplicationService;
    }

    [HttpGet("GetById/{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            return Ok(await this.blobFileApplicationService.GetById(id));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] BlobFileDTO blobFile)
    {
        try
        {
            return Ok(await this.blobFileApplicationService.Add(blobFile));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            return Ok(await this.blobFileApplicationService.Remove(id));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
