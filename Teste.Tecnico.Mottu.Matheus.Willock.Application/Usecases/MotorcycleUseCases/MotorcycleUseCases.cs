using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Teste.Tecnico.Mottu.Matheus.Willock.Application.DTOs.Motorcycle;
using Teste.Tecnico.Mottu.Matheus.Willock.Domain.Libs;
using Teste.Tecnico.Mottu.Matheus.Willock.Domain.Models;
using Teste.Tecnico.Mottu.Matheus.Willock.Infrastructure.Repositories.MotorCycleRepository;
using Teste.Tecnico.Mottu.Matheus.Willock.Infrastructure.Repositories.UserAdminRepository;
using Teste.Tecnico.Mottu.Matheus.Willock.Infrastructure.Utils;

namespace Teste.Tecnico.Mottu.Matheus.Willock.Application.Usecases.MotorcycleUseCases
{
    public class MotorCycleUseCases : IMotorCycleUseCases
    {
        private readonly IMotorCycleRepository _motorcycleRepository;
        private readonly IUserAdminRepository _userAdminRepository;

        public MotorCycleUseCases(IMotorCycleRepository motorcycleRepository, IUserAdminRepository userAdminRepository)
        {
            _motorcycleRepository = motorcycleRepository;
            _userAdminRepository = userAdminRepository;
        }

        public async Task<Output> DeleteMotorcycle(string plate, string token)
        {
            var output = new Output();
            try
            {
                var validateUserAdminToken = await ValidateUserAdminToken(token);

                if (!validateUserAdminToken.IsValid)
                    return validateUserAdminToken;

                var motorcycleExist = await _motorcycleRepository.GetByPlateAsync(plate);

                if (motorcycleExist == null)
                {
                    output.AddErrorMessage("Motorcycle not found");
                    return output;
                }

                await _motorcycleRepository.DeleteAsync(motorcycleExist);

                var motorcycleDeleted = await _motorcycleRepository.GetByPlateAsync(plate);

                if (motorcycleDeleted != null)
                {
                    output.AddErrorMessage("An error occurred while deleting motorcycle");
                    return output;
                }

                return output;
            }
            catch (Exception ex)
            {
                output.AddErrorMessage(ex.Message);
                return output;
            }
        }

        public async Task<Output> GetAllMotorcycles(string token)
        {
            var output = new Output();
            try
            {
                var validateUserAdminToken = await ValidateUserAdminToken(token);

                if (!validateUserAdminToken.IsValid)
                    return validateUserAdminToken;

                var allMotorcycles = await _motorcycleRepository.GetAll();

                List<OutputMotorcycleDto> outputMotorcycleDtoList = new();

                allMotorcycles.ForEach(e =>
                {
                    outputMotorcycleDtoList.Add(new OutputMotorcycleDto(e.Plate, e.Year, e.Model, e.IsRented));
                });

                output.AddResult(outputMotorcycleDtoList);
                return output;
            }
            catch (Exception ex)
            {
                output.AddErrorMessage(ex.Message);
                return output;
            }
        }

        public async Task<Output> GetMotorcycleByPlate(string plate, string token)
        {
            var output = new Output();
            try
            {
                var validateUserAdminToken = await ValidateUserAdminToken(token);

                if (!validateUserAdminToken.IsValid)
                    return validateUserAdminToken;

                var motorcycle = await _motorcycleRepository.GetByPlateAsync(plate);

                if(motorcycle == null)
                {
                    output.AddErrorMessage("Motorcycle not found");
                    return output;
                }

                OutputMotorcycleDto outputMotorcycle = new(motorcycle.Plate, motorcycle.Year, motorcycle.Model, motorcycle.IsRented);

                output.AddResult(outputMotorcycle);
                return output;
            }
            catch (Exception ex)
            {
                output.AddErrorMessage(ex.Message);
                return output;
            }
        }

        public async Task<Output> RegisterMotorcycle(MotorcycleDto motorcycle, string token)
        {
            var output = new Output();

            try
            {
                var validateUserAdminToken = await ValidateUserAdminToken(token);

                if (!validateUserAdminToken.IsValid)
                    return validateUserAdminToken;

                var validateMotorcycle = ValidateMotorcycle(motorcycle);

                if (validateMotorcycle == null)
                {
                    output.AddErrorMessage("Invalid motorcycle");
                    return output;
                }

                var motorcycleExist = await _motorcycleRepository.GetByPlateAsync(validateMotorcycle.Plate);

                if (motorcycleExist != null)
                {
                       output.AddErrorMessage("Motorcycle already registered");
                    return output;
                }
                
                await _motorcycleRepository.AddAsync(validateMotorcycle);

                var motorcycleRegistered = await _motorcycleRepository.GetByPlateAsync(validateMotorcycle.Plate);

                if (motorcycleRegistered == null)
                {
                    output.AddErrorMessage("An error occurred while registering motorcycle");
                    return output;
                }

                MotorcycleDto outputMotorcycle = new(motorcycleRegistered.Plate, motorcycleRegistered.Year, motorcycleRegistered.Model);

                output.AddResult(outputMotorcycle);

                return output;
            }
            catch (Exception ex)
            {
                output.AddErrorMessage(ex.Message);
                return output;
            }    
        }

        public async Task<Output> UpdateMotorcycle(MotorcycleDto motorcycle, string token)
        {
            var output = new Output();
            try
            {
                var validateUserAdminToken = await ValidateUserAdminToken(token);

                if (!validateUserAdminToken.IsValid)
                    return validateUserAdminToken;

                var validateMotorcycle = ValidateMotorcycle(motorcycle);

                if (validateMotorcycle == null)
                {
                    output.AddErrorMessage("Invalid motorcycle");      
                    return output;
                }

                var motorcycleExist = await _motorcycleRepository.GetByPlateAsync(validateMotorcycle.Plate);

                if (motorcycleExist == null)
                {
                    output.AddErrorMessage("Motorcycle not found");
                    return output;
                }

                motorcycleExist.Year = motorcycle.Year;
                motorcycleExist.Model = motorcycle.Model;
                motorcycleExist.Plate = motorcycle.Plate;

                var updatedMotorcycle = await _motorcycleRepository.UpdateAsync(motorcycleExist);

                if (updatedMotorcycle == null)
                {
                    output.AddErrorMessage("An error occurred while updating motorcycle");
                    return output;
                }

                OutputMotorcycleDto outputMotorcycle = new(updatedMotorcycle.Plate, updatedMotorcycle.Model, updatedMotorcycle.Year, updatedMotorcycle.IsRented);

                output.AddResult(updatedMotorcycle);

                return output;
            }
            catch (Exception ex)
            {
                output.AddErrorMessage(ex.Message);
                return output;
            }
        }

        private async Task<Output> ValidateUserAdminToken(string token)
        {
            var output = new Output();

            if (string.IsNullOrEmpty(token))
            {
                output.AddErrorMessage("Invalid request");
                return output;
            }

            var requestTokenIsValid = TokenUtils.ValidateToken(token);

            if(!requestTokenIsValid.IsValid)
                return requestTokenIsValid;

            var GetTokenResult = requestTokenIsValid.GetResult();

            if (GetTokenResult == null)
            {
                output.AddErrorMessage("Invalid request");
                return output;
            }

            var userExist = await _userAdminRepository.GetByEmailAsync(GetTokenResult.ToString()!);

            if (userExist == null)
            {
                output.AddErrorMessage("Invalid request");
                return output;
            }

            return output;
        }

        private static Motorcycle? ValidateMotorcycle(MotorcycleDto motorcycle)
        {
            var output = new Output();
            string platePattern = "^[A-Z]{3}-[0-9A-Za-z]{4}$";
            string yearPattern = "^[0-9]{4}$";

            if (!Regex.IsMatch(motorcycle.Plate.ToUpper(), platePattern) || !Regex.IsMatch(motorcycle.Year, yearPattern) || string.IsNullOrEmpty(motorcycle.Model))
            {
                return null;
            }

            Motorcycle motorcycleEntity = new Motorcycle(motorcycle.Plate.ToUpper(), motorcycle.Model, motorcycle.Year);

            output.AddResult(motorcycleEntity);
            return motorcycleEntity;
        }
    }
}
