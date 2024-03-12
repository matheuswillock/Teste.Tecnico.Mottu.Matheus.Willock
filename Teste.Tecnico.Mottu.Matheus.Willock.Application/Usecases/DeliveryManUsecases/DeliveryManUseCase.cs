using Azure.Core;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Teste.Tecnico.Mottu.Matheus.Willock.Application.DTOs.DeliveryMan;
using Teste.Tecnico.Mottu.Matheus.Willock.Application.DTOs.UserAdmin;
using Teste.Tecnico.Mottu.Matheus.Willock.Domain.Libs;
using Teste.Tecnico.Mottu.Matheus.Willock.Domain.Models;
using Teste.Tecnico.Mottu.Matheus.Willock.Infrastructure.CosmicInfra;
using Teste.Tecnico.Mottu.Matheus.Willock.Infrastructure.CosmicInfra.Dto;
using Teste.Tecnico.Mottu.Matheus.Willock.Infrastructure.Repositories.DeliveryManRepository;
using Teste.Tecnico.Mottu.Matheus.Willock.Infrastructure.Repositories.DocumentsRepository;
using Teste.Tecnico.Mottu.Matheus.Willock.Infrastructure.Repositories.MotorCycleRepository;
using Teste.Tecnico.Mottu.Matheus.Willock.Infrastructure.Repositories.RentalPlansRepository;
using Teste.Tecnico.Mottu.Matheus.Willock.Infrastructure.Utils;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Teste.Tecnico.Mottu.Matheus.Willock.Application.Usecases.DeliveryManUsecases
{
    public class DeliveryManUseCase : IDeliveryManUseCase
    {
        private readonly ICosmic _cosmic;
        private readonly IDeliveryManRepository _deliveryManRepository;
        private readonly IDocumentsRepository _documentsRepository;
        private readonly IRentalPlansRepository _rentalPlansRepository;
        private readonly IMotorCycleRepository _motorcycleRepository;

        public DeliveryManUseCase(
            ICosmic cosmic, 
            IDeliveryManRepository deliveryManRepository, 
            IDocumentsRepository documentsRepository, 
            IRentalPlansRepository rentalPlansRepository,
            IMotorCycleRepository motorcycleRepository
        )
        {
            _cosmic = cosmic;
            _deliveryManRepository = deliveryManRepository;
            _documentsRepository = documentsRepository;
            _rentalPlansRepository = rentalPlansRepository;
            _motorcycleRepository = motorcycleRepository;
        }

        public async Task<Output> LoginDeliveryMan(InputLoginDeliveryMan request)
        {
            var output = new Output();
            try
            {
                var getDeliveryMan = await GetDeliveryman(request.Email, request.Password);

                if (getDeliveryMan == null)
                {
                    output.AddErrorMessage("Invalid email or password");
                    return output;
                }

                var outputUserAdminDto = CreateOutputDeliveryManDto(getDeliveryMan);

                output.AddResult(outputUserAdminDto);
                return output;
            }
            catch (Exception ex)
            {
                output.AddErrorMessage(ex.Message);
                return output;
            }
        }

        public async Task<Output> ChooseRentalPlan(InputChooseRentalPlan input)
        {
            var output = new Output();
            try
            {
                var deliveryManTokenIsValid = await ValidateUserToken(input.DeliveryManToken);

                if (!deliveryManTokenIsValid.IsValid)
                {
                    output.AddErrorMessage("Invalid request");
                    return output;
                }

                var email = deliveryManTokenIsValid.GetResult()?.ToString();

                if (email == null)
                {
                    output.AddErrorMessage("Error creating rental plan");
                    return output;
                }

                var deliveryMan = await _deliveryManRepository.GetByEmail(email);

                if (deliveryMan == null)
                {
                    output.AddErrorMessage("Error creating rental plan");
                    return output;
                }

                var document = await _documentsRepository.GetById(deliveryMan.Document);

                if (document == null || document.CnhType != CNH.A)
                {
                    output.AddErrorMessage("Error creating rental plan");
                    return output;
                }

                

                var rentalPlan = CreateRentalPlan(input);

                var motorcycle = await _motorcycleRepository.GetMotorcycleIsntRented();

                if (motorcycle == null)
                {
                    output.AddErrorMessage("No motorcycle available");
                    return output;
                }

                rentalPlan.MotorcycleId = motorcycle.Id;

                var createRentalPlan = await _rentalPlansRepository.Add(rentalPlan);

                if (createRentalPlan == null)
                {
                    output.AddErrorMessage("Error creating rental plan");
                    return output;
                }

                motorcycle.IsRented = true;
                await _motorcycleRepository.UpdateAsync(motorcycle);

                deliveryMan.RentalPlan = createRentalPlan.Id;
                await _deliveryManRepository.Update(deliveryMan);

                var outputChooseRentalPlan = CreateOutputChooseRentalPlan(motorcycle, createRentalPlan);

                output.AddResult(outputChooseRentalPlan);

                return output;
            }
            catch (Exception e)
            {
                output.AddErrorMessage(e.Message);
                return output;
            }
        }

        public async Task<Output> RegisterDeliveryMan(InputDeliveryManDto input)
        {
            var output = new Output();

            try
            {
                var userIsValid = await ValidateUser(input);

                if (!userIsValid) 
                {
                     output.AddErrorMessage("Invalid data");
                    return output;
                } 

                var deliveryManImage = await SendImageAsync(input.CnhPhoto, input.Name);

                if (deliveryManImage == null)
                {
                    output.AddErrorMessage("Error sending image");
                    return output;
                }

                DeliveryManDocument document = new (
                    input.CnhNumber, input.CnhType, deliveryManImage.Id, deliveryManImage.Url, deliveryManImage.Imgix_url);

                var createDocument = await _documentsRepository.Create(document);

                if (createDocument == null)
                {
                    output.AddErrorMessage("Error creating document");
                    return output;
                }

                var hashedPassword = MD5Utils.HashMD5Generate(input.Password);

                DateTime utcBirthday = DateTime.SpecifyKind(input.Birthday, DateTimeKind.Utc);

                DeliveryMan deliveryMan = new(
                    input.Name, input.Email, hashedPassword, createDocument.Id, input.Cnpj, utcBirthday);

                DeliveryMan? createDeliveryMan = await _deliveryManRepository.Create(deliveryMan);

                if (createDeliveryMan == null)
                {
                    output.AddErrorMessage("Error creating user");
                    return output;
                }

                var outputDeliveryManDto = CreateOutputDeliveryManDto(createDeliveryMan);

                output.AddResult(outputDeliveryManDto);

                return output;
            }
            catch (Exception e)
            {
                output.AddErrorMessage(e.Message);
                return output;
            }            
        }
        
        private async Task<bool> ValidateUser(InputDeliveryManDto request)
        {
            string emailRegex = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";

            bool isEmailValid = Regex.IsMatch(request.Email, emailRegex);

            string passwordRegex = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d.*\d)(?=.*[!@#$%^&*()\-_=+{};:,<.>]).{8,}$";

            bool isPasswordValid = Regex.IsMatch(request.Password, passwordRegex);

            string cnpjRegex = @"^\d{2}\.\d{3}\.\d{3}\/\d{4}\-\d{2}$";
            bool isCnpjValid = Regex.IsMatch(request.Cnpj, cnpjRegex);

            string cnhNumberRegex = @"^\d{9}$";
            bool isCnhNumberValid = Regex.IsMatch(request.CnhNumber, cnhNumberRegex);

            if (!isEmailValid || !isPasswordValid || string.IsNullOrEmpty(request.Name) || !isCnpjValid || !isCnhNumberValid) 
                return false;

            var getUserByEmail = await _deliveryManRepository.GetByCnpj(request.Cnpj);

            var getDocument = await _documentsRepository.GetByCnhNumber(request.CnhNumber);

            if (getUserByEmail != null && getDocument != null) return false;

            return true;
        }

        private async Task<CosmicMediaDto?> SendImageAsync(IFormFile cnhPhoto, string ImageName)
        {
            var cosmicImage = new CosmicImageDto($"{ImageName} CNH", cnhPhoto);

            var media = await _cosmic.SendImageAsync(cosmicImage);

            return media;
        }

        private static OutputDeliveryManDto CreateOutputDeliveryManDto(DeliveryMan user)
        {
            var deliveryManToken = TokenUtils.TokenCreate(user.Id.ToString(), user.Name, user.Email);

            OutputDeliveryManDto outputUserAdminDto = new(user.Name, user.Email, deliveryManToken);

            return outputUserAdminDto;
        }

        private static RentalPlan CreateRentalPlan(InputChooseRentalPlan input)
        {
            var endDay = DateTime.SpecifyKind(input.EndDay, DateTimeKind.Utc);
            var startDay = DateTime.SpecifyKind(DateTime.UtcNow.Date, DateTimeKind.Utc);

            int totalDays = (int)(endDay - startDay.AddDays(1)).TotalDays;

            decimal rentalCost = CalculateRentalCost(input.RentalDays, totalDays);

            RentalPlan rentalPlan = new (input.RentalDays, startDay, endDay, rentalCost);

            return rentalPlan;
        }

        private static decimal CalculateRentalCost(PlansDay rentalDays, int totalDays)
        {
            decimal rentalCost;
            double fineRate;
            int Plansdays;

            switch (rentalDays)
            {
                case PlansDay.SevenDays:
                    rentalCost = 30.00m;
                    fineRate = 0.20;
                    Plansdays = 7;
                    break;
                case PlansDay.FifteenDays:
                    rentalCost = 28.00m;
                    fineRate = 0.40;
                    Plansdays = 15;
                    break;
                case PlansDay.ThirtyDays:
                    rentalCost = 22.00m;
                    fineRate = 0.60;
                    Plansdays = 30;
                    break;
                default:
                    rentalCost = 30.00m;
                    fineRate = 0.20;
                    Plansdays = 7;
                    break;
            }

            if (totalDays < Plansdays)
            {
                var cost = (Plansdays - totalDays) * fineRate;
                return (decimal)cost + rentalCost;
            }

            return rentalCost;
        }

        private async Task<Output> ValidateUserToken(string token)
        {
            var output = new Output();

            if (string.IsNullOrEmpty(token))
            {
                output.AddErrorMessage("Invalid request");
                return output;
            }

            var requestTokenIsValid = TokenUtils.ValidateToken(token);

            if (!requestTokenIsValid.IsValid)
                return requestTokenIsValid;

            var GetTokenResult = requestTokenIsValid.GetResult()?.ToString();

            if (GetTokenResult == null)
            {
                output.AddErrorMessage("Invalid request");
                return output;
            }

            var deliveryManEmail = await _deliveryManRepository.GetByEmail(GetTokenResult);

            if (GetTokenResult == null || deliveryManEmail == null)
            {
                output.AddErrorMessage("Invalid request");
                return output;
            }

            return requestTokenIsValid;
        }

        private async Task<DeliveryMan?> GetDeliveryman(string email, string password)
        {
            var hashedPassword = MD5Utils.HashMD5Generate(password);

            var getDeliveryMan = await _deliveryManRepository.GetLoginAsync(email, hashedPassword);

            return getDeliveryMan;
        }

        private static OutputChooseRentalPlan CreateOutputChooseRentalPlan(Motorcycle motorcycle, RentalPlan rentalPlan)
        {

            string planDays;

            switch(rentalPlan.Days)
            {
                case PlansDay.SevenDays:
                    planDays = "7 dias";
                    break;
                case PlansDay.FifteenDays:
                    planDays = "15 dias";
                    break;
                case PlansDay.ThirtyDays:
                    planDays = "30 dias";
                    break;
                default:
                    planDays = "7 dias";
                    break;
            }

            var outputChooseRentalPlan = new OutputChooseRentalPlan(
                               motorcycle.Plate, motorcycle.Year, motorcycle.Model, planDays, rentalPlan.StartDay, rentalPlan.EndDay, rentalPlan.RentValue);

            return outputChooseRentalPlan;
        }
    }
}
