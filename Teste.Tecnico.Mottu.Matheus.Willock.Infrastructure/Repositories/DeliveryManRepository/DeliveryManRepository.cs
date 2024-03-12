using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teste.Tecnico.Mottu.Matheus.Willock.Domain.Models;

namespace Teste.Tecnico.Mottu.Matheus.Willock.Infrastructure.Repositories.DeliveryManRepository
{
    public class DeliveryManRepository : IDeliveryManRepository
    {
        private readonly MottuDbContext _context;

        public DeliveryManRepository(MottuDbContext context)
        {
            _context = context;
        }

        public async Task<DeliveryMan> Create(DeliveryMan deliveryMan)
        {
            _context.DeliveryMen.Add(deliveryMan);
            await _context.SaveChangesAsync();
            var getDeliveryMan = await _context.DeliveryMen.FirstOrDefaultAsync(x => x.Cnpj == deliveryMan.Cnpj);
            return getDeliveryMan;
        }

        public async Task<DeliveryMan?> GetLoginAsync(string email, string hashedPassword)
        {
            return await _context.DeliveryMen.FirstOrDefaultAsync(x => x.Email == email && x.Password == hashedPassword);
        }

        public async Task<DeliveryMan?> GetByEmail(string email)
        {
            return await _context.DeliveryMen.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<List<DeliveryMan>> GetAll()
        {
            return await _context.DeliveryMen.ToListAsync();
        }

        public async Task<DeliveryMan> Update(DeliveryMan deliveryMan)
        {
            _context.Entry(deliveryMan).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return deliveryMan;
        }

        public async Task Delete(int id)
        {
            var deliveryMan = await _context.DeliveryMen.FindAsync(id);
            if (deliveryMan != null)
            {
                _context.DeliveryMen.Remove(deliveryMan);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<DeliveryMan?> GetByCnpj(string cnpj)
        {
            return await _context.DeliveryMen.FirstOrDefaultAsync(x => x.Cnpj == cnpj);
        }
    }
}
