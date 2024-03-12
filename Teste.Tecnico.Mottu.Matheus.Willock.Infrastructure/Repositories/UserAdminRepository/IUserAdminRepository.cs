using Teste.Tecnico.Mottu.Matheus.Willock.Domain.Models;

namespace Teste.Tecnico.Mottu.Matheus.Willock.Infrastructure.Repositories.UserAdminRepository
{
    public interface IUserAdminRepository
    {
        Task<UserAdmin?> GetByEmailAsync(string email);
        Task<UserAdmin?> GetLoginAsync(string email, string password);
        Task AddAsync(UserAdmin userAdmin);
        Task UpdateAsync(UserAdmin userAdmin);
        Task DeleteAsync(UserAdmin userAdmin);
    }
}
