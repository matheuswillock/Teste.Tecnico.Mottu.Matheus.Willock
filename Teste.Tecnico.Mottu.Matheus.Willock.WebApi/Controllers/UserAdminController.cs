using Microsoft.AspNetCore.Mvc;
using Teste.Tecnico.Mottu.Matheus.Willock.Application.DTOs.UserAdmin;
using Teste.Tecnico.Mottu.Matheus.Willock.Application.Usecases.OrderUseCases;
using Teste.Tecnico.Mottu.Matheus.Willock.Application.Usecases.UserAdminUsecases;
using Teste.Tecnico.Mottu.Matheus.Willock.Domain.Models;
using Teste.Tecnico.Mottu.Matheus.Willock.Infrastructure.KafkaInfra.Contracts;

namespace Teste.Tecnico.Mottu.Matheus.Willock.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserAdminController : ControllerBase
    {
        private readonly IUserAdminUseCase _userAdminUseCaseRegister;
        private readonly IOrderUseCases _orderUseCases;


        public UserAdminController(IUserAdminUseCase userAdminUseCaseRegister, IOrderUseCases orderUseCases)
        {
            _userAdminUseCaseRegister = userAdminUseCaseRegister;
            _orderUseCases = orderUseCases;
        }

        [HttpPost("/register")]
        public async Task<IActionResult> RegisterUserAdmin([FromBody] InputUserAdminToRegisterDto user)
        {
            try
            {
                var result = await _userAdminUseCaseRegister.RegisterUserAdmin(user);

                if (!result.IsValid)
                    return BadRequest(result);

                var getResults = result.GetResult();

                return StatusCode(201, getResults);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("/login")]
        public async Task<IActionResult> GetLogin([FromBody] InputUserAdminDto request)
        {
            try
            {
                var result = await _userAdminUseCaseRegister.LoginUser(request);

                if (!result.IsValid)
                {
                    return BadRequest(result);
                }

                var getResults = result.GetResult();

                return Ok(getResults);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }            
        }

        [HttpPost("/order")]
        public async Task<IActionResult> CreateOrder([FromBody] InputCreateOrderDto order)
        {
            try
            {
                var createOrder = new Order(order.RaceValue);

                var sendMessage = await _orderUseCases.AddOrderAsync(createOrder);

                if (!sendMessage.IsValid)
                    return BadRequest(sendMessage);

                var getResults = sendMessage.GetResult();

                return Ok(getResults);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("/update")]
        public async Task<IActionResult> UpdateAdmin([FromBody] InputUserAdminToUpdateDto user)
        {
            try
            {
                var userAdminToken = HttpContext.Request.Headers["Authorization"].ToString().Substring("Bearer ".Length).Trim();

                var result = await _userAdminUseCaseRegister.UpdateAdmin(user, userAdminToken);

                if (!result.IsValid)
                    return BadRequest(result);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("/delete")]
        public async Task<IActionResult> DeleteUserAdmin([FromBody] InputUserAdminDto user)
        {
            try
            {
                var userAdminToken = HttpContext.Request.Headers["Authorization"].ToString().Substring("Bearer ".Length).Trim();

                var result = await _userAdminUseCaseRegister.DeleteUserAdmin(user, userAdminToken);

                if (!result.IsValid)
                {
                    return BadRequest(result);
                }

                return StatusCode(204);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
