using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GLIMPSE.Domain.Models;
using GLIMPSE.Application.Interfaces;
using GLIMPSE.Application.Dtos;

namespace GLIMPSE.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class TagController : ControllerBase
{
    private readonly ITagApplicationService tagApplicationService;

    public TagController(ITagApplicationService tagApplicationService)
    {
        this.tagApplicationService = tagApplicationService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            return Ok(await this.tagApplicationService.GetAll());
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("GetIgnoreFilters")]
    public async Task<IActionResult> GetIgnoreFilters()
    {
        try
        {
            return Ok(await tagApplicationService.GetAllIgnoreFilters());
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("GetById/{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            return Ok(await this.tagApplicationService.GetById(id));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] TagDTO tagDTO)
    {
        try
        {
            if (tagDTO == null)
                return NotFound();

            return Ok(await this.tagApplicationService.Add(tagDTO));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] TagDTO tagDTO)
    {
        try
        {
            if (tagDTO == null)
                return NotFound();

            return Ok(await this.tagApplicationService.Update(tagDTO));
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
            return Ok(await this.tagApplicationService.Remove(id));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}