using GLIMPSE.Application.Dtos;
using GLIMPSE.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GLIMPSE.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SprintController : ControllerBase
    {
        private readonly ISprintApplicationService SprintApplicationService;

        public SprintController(ISprintApplicationService SprintApplicationService)
        {
            this.SprintApplicationService = SprintApplicationService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await this.SprintApplicationService.GetAll());
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
                return Ok(await SprintApplicationService.GetAllIgnoreFilters());
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
                return Ok(await this.SprintApplicationService.GetById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SprintDTO SprintDTO)
        {
            try
            {
                if (SprintDTO == null)
                    return NotFound();

                return Ok(await this.SprintApplicationService.Add(SprintDTO));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] SprintDTO SprintDTO)
        {
            try
            {
                if (SprintDTO == null)
                    return NotFound();

                return Ok(await this.SprintApplicationService.Update(SprintDTO));
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
                return Ok(await this.SprintApplicationService.Remove(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
