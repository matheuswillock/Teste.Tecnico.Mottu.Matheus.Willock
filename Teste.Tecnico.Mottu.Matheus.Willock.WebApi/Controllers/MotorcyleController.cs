using Microsoft.AspNetCore.Mvc;
using Teste.Tecnico.Mottu.Matheus.Willock.Application.DTOs.Motorcycle;
using Teste.Tecnico.Mottu.Matheus.Willock.Application.DTOs.UserAdmin;
using Teste.Tecnico.Mottu.Matheus.Willock.Application.Usecases.MotorcycleUseCases;
using Teste.Tecnico.Mottu.Matheus.Willock.Application.Usecases.UserAdminUsecases;

namespace Teste.Tecnico.Mottu.Matheus.Willock.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MotorcycleController(IMotorCycleUseCases motorCycleUseCases) : ControllerBase
    {
        private readonly IMotorCycleUseCases _motorCycleUseCases = motorCycleUseCases;

        [HttpPost("/motorcycle/register")]
        public async Task<IActionResult> RegisterMotorcycle([FromBody] MotorcycleDto motorcycle)
        {
            try
            {
                var userAdminToken = HttpContext.Request.Headers["Authorization"].ToString().Substring("Bearer ".Length).Trim();

                var input = await _motorCycleUseCases.RegisterMotorcycle(motorcycle, userAdminToken);

                if (!input.IsValid)
                    return BadRequest(input);

                var getResults = input.GetResult();

                return StatusCode(201, getResults);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("/motorcycle/update")]
        public async Task<IActionResult> UpdateMotorcycle([FromBody] MotorcycleDto motorcycle)
        {
            try
            {
                var userAdminToken = HttpContext.Request.Headers["Authorization"].ToString().Substring("Bearer ".Length).Trim();

                var result = await _motorCycleUseCases.UpdateMotorcycle(motorcycle, userAdminToken);

                if (!result.IsValid)
                    return BadRequest(result);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("/motorcycle/get/all")]
        public async Task<IActionResult> GetAllMotorcycles()
        {
            try
            {
                var userAdminToken = HttpContext.Request.Headers["Authorization"].ToString().Substring("Bearer ".Length).Trim();

                var result = await _motorCycleUseCases.GetAllMotorcycles(userAdminToken);

                if (!result.IsValid)
                    return BadRequest(result);

                var getResults = result.GetResult();

                return Ok(getResults);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("/motorcycle/get/{plate}")]
        public async Task<IActionResult> GetMotorcycleByPlate(string plate)
        {
            try
            {
                var userAdminToken = HttpContext.Request.Headers["Authorization"].ToString().Substring("Bearer ".Length).Trim();

                var result = await _motorCycleUseCases.GetMotorcycleByPlate(plate, userAdminToken);

                if (!result.IsValid)
                    return BadRequest(result);

                var getResults = result.GetResult();

                return Ok(getResults);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("/motorcycle/remove/{plate}")]
        public async Task<IActionResult> RemoveMotorcycleByPlate(string plate)
        {
            try
            {
                var userAdminToken = HttpContext.Request.Headers["Authorization"].ToString().Substring("Bearer ".Length).Trim();

                var result = await _motorCycleUseCases.DeleteMotorcycle(plate, userAdminToken);

                if (!result.IsValid)
                    return BadRequest(result);

                return StatusCode(204);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
