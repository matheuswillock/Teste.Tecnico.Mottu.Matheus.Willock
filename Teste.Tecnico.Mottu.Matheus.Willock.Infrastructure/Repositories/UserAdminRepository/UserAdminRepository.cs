using Microsoft.EntityFrameworkCore;
using Teste.Tecnico.Mottu.Matheus.Willock.Domain.Models;

namespace Teste.Tecnico.Mottu.Matheus.Willock.Infrastructure.Repositories.UserAdminRepository
{
    public class UserAdminRepository : IUserAdminRepository
    {
        private readonly MottuDbContext _context;

        public UserAdminRepository(MottuDbContext context)
        {
            _context = context;
        }

        public async Task<UserAdmin?> GetByEmailAsync(string email)
        {
            return await _context.UsersAdmin.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task AddAsync(UserAdmin userAdmin)
        {
            await _context.UsersAdmin.AddAsync(userAdmin);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(UserAdmin userAdmin)
        {
            _context.UsersAdmin.Update(userAdmin);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(UserAdmin userAdmin)
        {
            _context.UsersAdmin.Remove(userAdmin);
            await _context.SaveChangesAsync();
        }

        public async Task<UserAdmin?> GetLoginAsync(string email, string password)
        {
            var user = await _context.UsersAdmin.FirstOrDefaultAsync(u =>
                u.Email == email && u.Password == password);

            return user;
        }
    }
}
