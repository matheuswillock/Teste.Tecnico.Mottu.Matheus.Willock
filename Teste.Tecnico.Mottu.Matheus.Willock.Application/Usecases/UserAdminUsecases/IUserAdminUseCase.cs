using Teste.Tecnico.Mottu.Matheus.Willock.Domain.Models;
using Teste.Tecnico.Mottu.Matheus.Willock.Application.DTOs.UserAdmin;
using Teste.Tecnico.Mottu.Matheus.Willock.Domain.Libs;

namespace Teste.Tecnico.Mottu.Matheus.Willock.Application.Usecases.UserAdminUsecases
{
    public interface IUserAdminUseCase
    {
        Task<Output> RegisterUserAdmin(InputUserAdminToRegisterDto request);
        Task<Output> LoginUser(InputUserAdminDto request);
        Task<Output> UpdateAdmin(InputUserAdminToUpdateDto request, string token);
        Task<Output> DeleteUserAdmin(InputUserAdminDto request, string token);
    }
}
