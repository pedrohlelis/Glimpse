using Microsoft.AspNetCore.Mvc;
using GLIMPSE.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using GLIMPSE.Application.Interfaces;
using GLIMPSE.Application.Dtos;

namespace GLIMPSE.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class BoardController : ControllerBase
{
    private readonly IBoardApplicationService boardApplicationService;

    public BoardController(IBoardApplicationService boardApplicationService)
    {
        this.boardApplicationService = boardApplicationService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            return Ok(await this.boardApplicationService.GetAll());
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
            return Ok(await boardApplicationService.GetAllIgnoreFilters());
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
            return Ok(await this.boardApplicationService.GetById(id));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] BoardDTO boardDTO)
    {
        try
        {
            if (boardDTO == null)
                return NotFound();

            return Ok(await this.boardApplicationService.Add(boardDTO));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] BoardDTO boardDTO)
    {
        try
        {
            if (boardDTO == null)
                return NotFound();

            return Ok(await this.boardApplicationService.Update(boardDTO));
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
            return Ok(await this.boardApplicationService.Remove(id));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
