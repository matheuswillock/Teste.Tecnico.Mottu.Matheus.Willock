using System.Text.RegularExpressions;
using Teste.Tecnico.Mottu.Matheus.Willock.Application.DTOs.UserAdmin;
using Teste.Tecnico.Mottu.Matheus.Willock.Domain.Libs;
using Teste.Tecnico.Mottu.Matheus.Willock.Domain.Models;
using Teste.Tecnico.Mottu.Matheus.Willock.Infrastructure.Repositories.UserAdminRepository;
using Teste.Tecnico.Mottu.Matheus.Willock.Infrastructure.Utils;

namespace Teste.Tecnico.Mottu.Matheus.Willock.Application.Usecases.UserAdminUsecases
{
    public class UserAdminUseCase : IUserAdminUseCase
    {
        private readonly IUserAdminRepository _userAdminRepository;

        public UserAdminUseCase(IUserAdminRepository userAdminRepository)
        {
            _userAdminRepository = userAdminRepository;
        }

        public async Task<Output> DeleteUserAdmin(InputUserAdminDto request, string token)
        {
            var output = new Output();

            try
            {
                var validateUserToken = ValidateUserToken(request.Email, token);

                if (!validateUserToken.IsValid) 
                    return validateUserToken;

                var getuser = await Getuser(request.Email, request.Password);

                if (getuser == null)
                {
                    output.AddErrorMessage("Invalid username or password");
                    return output;
                }

                await _userAdminRepository.DeleteAsync(getuser!);

                var isRemovedUser = await Getuser(request.Email, request.Password);

                if (isRemovedUser != null) 
                {
                    output.AddErrorMessage("An error occurred while deleting user");

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

        public async Task<Output> LoginUser(InputUserAdminDto request)
        {
            var output = new Output();
            try
            {             
                var getuser = await Getuser(request.Email, request.Password);

                if (getuser == null)
                {
                    output.AddErrorMessage("Invalid email or password");
                    return output;
                }

                var outputUserAdminDto = CreateOutputUserAdminDto(getuser!);



                output.AddResult(outputUserAdminDto);
                return output;
            }
            catch (Exception ex)
            {
                output.AddErrorMessage(ex.Message);
                return output;
            }            
        }

        public async Task<Output> RegisterUserAdmin(InputUserAdminToRegisterDto request)
        {
            var output = new Output();
            try
            {
                var reuestIsValid = await ValidateUser(request);

                if (!reuestIsValid)
                {
                    output.AddErrorMessage("Invalid username or password");
                    return output;
                }

                var hashedPassword = MD5Utils.HashMD5Generate(request.Password);

                UserAdmin user = new(request.Name, request.Email, hashedPassword);

                await _userAdminRepository.AddAsync(user);

                var userCreated = await Getuser(user.Email, request.Password);

                if (userCreated == null)
                {
                    output.AddErrorMessage("An error occurred when creating user");
                    return output;
                }

                var outputUserAdminDto = CreateOutputUserAdminDto(userCreated);

                output.AddResult(outputUserAdminDto);

                return output;
            }
            catch (Exception ex)
            {
                output.AddErrorMessage(ex.Message);
                return output;
            }          
        }

        public async Task<Output> UpdateAdmin(InputUserAdminToUpdateDto request, string token)
        {
            var output = new Output();

            try
            {
                var validateUserToken = ValidateUserToken(request.Email, token);

                if (!validateUserToken.IsValid) 
                    return validateUserToken;

                var userExist = await _userAdminRepository.GetByEmailAsync(request.Email);

                if (userExist == null)
                {
                    output.AddErrorMessage("User does not exist");
                    return output;
                }

                userExist.Email = request.Email;
                userExist.Password = MD5Utils.HashMD5Generate(request.Password);
                userExist.Name = request.Name;

                await _userAdminRepository.UpdateAsync(userExist);

                var getuser = await _userAdminRepository.GetByEmailAsync(request.Email);

                if (getuser == null)
                {
                    output.AddErrorMessage("An error occurred while updating user");
                    return output;
                }

                var outputUserAdminDto = CreateOutputUserAdminDto(getuser);

                output.AddResult(outputUserAdminDto);

                return output;
            }
            catch (Exception ex)
            {
                output.AddErrorMessage(ex.Message);
                return output;
            }
        }

        private async Task<UserAdmin?> Getuser(string email, string password)
        {
            var hashedPassword = MD5Utils.HashMD5Generate(password);

            var getUserByEmail = await _userAdminRepository.GetLoginAsync(email, hashedPassword);

            return getUserByEmail;
        }

        private async Task<bool> ValidateUser(InputUserAdminToRegisterDto request)
        {
            string emailRegex = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";

            bool isEmailValid = Regex.IsMatch(request.Email, emailRegex);

            string passwordRegex = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d.*\d)(?=.*[!@#$%^&*()\-_=+{};:,<.>]).{8,}$";

            bool isPasswordValid = Regex.IsMatch(request.Password, passwordRegex);

            if (!isEmailValid || !isPasswordValid || string.IsNullOrEmpty(request.Name)) return false;

            var getUserByEmail = await _userAdminRepository.GetByEmailAsync(request.Email);

            if (getUserByEmail != null) return false;

            return true;
        }

        private static OutputUserAdminDto CreateOutputUserAdminDto(UserAdmin user)
        {
            var userToken = TokenUtils.TokenCreate(user.Id.ToString(), user.Name, user.Email);

            OutputUserAdminDto outputUserAdminDto = new(user.Name, user.Email, userToken);

            return outputUserAdminDto;
        }

        private static Output ValidateUserToken(string email, string token)
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

            var GetTokenResult = requestTokenIsValid.GetResult();

            if (GetTokenResult == null || GetTokenResult.ToString() != email)
            {
                output.AddErrorMessage("Invalid request");
                return output;
            }

            return requestTokenIsValid;
        }
    }
}
