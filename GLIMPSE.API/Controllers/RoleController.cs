using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GLIMPSE.Domain.Models;
using GLIMPSE.Application.Interfaces;
using GLIMPSE.Application.Dtos;

namespace GLIMPSE.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class RoleController : ControllerBase
{
    private readonly IRoleApplicationService roleApplicationService;

    public RoleController(IRoleApplicationService roleApplicationService)
    {
        this.roleApplicationService = roleApplicationService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            return Ok(await this.roleApplicationService.GetAll());
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
            return Ok(await roleApplicationService.GetAllIgnoreFilters());
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
            return Ok(await this.roleApplicationService.GetById(id));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] RoleDTO roleDTO)
    {
        try
        {
            if (roleDTO == null)
                return NotFound();

            return Ok(await this.roleApplicationService.Add(roleDTO));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] RoleDTO roleDTO)
    {
        try
        {
            if (roleDTO == null)
                return NotFound();

            return Ok(await this.roleApplicationService.Update(roleDTO));
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
            return Ok(await this.roleApplicationService.Remove(id));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
