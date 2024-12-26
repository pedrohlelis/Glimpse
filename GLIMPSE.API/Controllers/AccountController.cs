using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace GLIMPSE.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class AccountController : ControllerBase
{
    /* private readonly IAccountApplicationService accountApplicationService;

    public AccountController(IAccountApplicationService accountApplicationService)
    {
        this.accountApplicationService = accountApplicationService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            return Ok(await this.accountApplicationService.GetAll());
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
            return Ok(await accountApplicationService.GetAllIgnoreFilters());
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
            return Ok(await this.accountApplicationService.GetById(id));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] AccountDTO accountDTO)
    {
        try
        {
            if (accountDTO == null)
                return NotFound();

            return Ok(await this.accountApplicationService.Add(accountDTO));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] AccountDTO accountDTO)
    {
        try
        {
            if (accountDTO == null)
                return NotFound();

            return Ok(await this.accountApplicationService.Update(accountDTO));
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
            return Ok(await this.accountApplicationService.Remove(id));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    } */
}
