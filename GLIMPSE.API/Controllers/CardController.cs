using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GLIMPSE.Domain.Models;
using GLIMPSE.Application.Interfaces;
using GLIMPSE.Application.Dtos;

namespace GLIMPSE.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CardController : ControllerBase
{
    private readonly ICardApplicationService cardApplicationService;

    public CardController(ICardApplicationService cardApplicationService)
    {
        this.cardApplicationService = cardApplicationService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            return Ok(await this.cardApplicationService.GetAll());
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
            return Ok(await cardApplicationService.GetAllIgnoreFilters());
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
            return Ok(await this.cardApplicationService.GetById(id));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CardDTO cardDTO)
    {
        try
        {
            if (cardDTO == null)
                return NotFound();

            return Ok(await this.cardApplicationService.Add(cardDTO));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] CardDTO cardDTO)
    {
        try
        {
            if (cardDTO == null)
                return NotFound();

            return Ok(await this.cardApplicationService.Update(cardDTO));
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
            return Ok(await this.cardApplicationService.Remove(id));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}