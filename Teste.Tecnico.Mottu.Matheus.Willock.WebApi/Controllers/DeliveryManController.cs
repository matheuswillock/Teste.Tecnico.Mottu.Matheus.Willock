using Microsoft.AspNetCore.Mvc;
using Teste.Tecnico.Mottu.Matheus.Willock.Application.DTOs.UserAdmin;
using Teste.Tecnico.Mottu.Matheus.Willock.Application.Usecases.UserAdminUsecases;
using static System.Net.Mime.MediaTypeNames;
using Teste.Tecnico.Mottu.Matheus.Willock.Infrastructure.CosmicInfra.Dto;
using Teste.Tecnico.Mottu.Matheus.Willock.Infrastructure.CosmicInfra;
using Teste.Tecnico.Mottu.Matheus.Willock.Application.Usecases.DeliveryManUsecases;
using Teste.Tecnico.Mottu.Matheus.Willock.Application.DTOs.DeliveryMan;

namespace Teste.Tecnico.Mottu.Matheus.Willock.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DeliveryManController(IDeliveryManUseCase deliveryManUseCase) : ControllerBase
    {
        private readonly IDeliveryManUseCase _deliveryManUseCase = deliveryManUseCase;

        [HttpPost("/deliveryman/register")]
        public async Task<IActionResult> RegisterDeliveryMan([FromForm] InputDeliveryManDto inputDeliveryMan)
        {
            try
            {
                var input = await _deliveryManUseCase.RegisterDeliveryMan(inputDeliveryMan);                

                if (!input.IsValid)
                {                    
                    return BadRequest(input);
                }

                var getResult = input.GetResult();

                return Ok(getResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("/deliveryman/login")]
        public async Task<IActionResult> LoginDeliveryMan([FromBody] InputLoginDeliveryMan inputDeliveryMan)
        {
            try
            {
                var input = await _deliveryManUseCase.LoginDeliveryMan(inputDeliveryMan);

                if (!input.IsValid)
                {
                    return BadRequest(input);
                }

                var getResult = input.GetResult();

                return Ok(getResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("/deliveryman/plans/choice")]
        public async Task<IActionResult> ChooseRentalPlan([FromBody] InputChooseRentalPlan inputDeliveryMan)
        {
            try
            {
                var input = await _deliveryManUseCase.ChooseRentalPlan(inputDeliveryMan);

                if (!input.IsValid)
                {
                    return BadRequest(input);
                }

                var getResult = input.GetResult();

                return Ok(getResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
