using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teste.Tecnico.Mottu.Matheus.Willock.Domain.Models;

namespace Teste.Tecnico.Mottu.Matheus.Willock.Infrastructure.Repositories.MotorCycleRepository
{
    public class MotorCycleRepository : IMotorCycleRepository
    {
        private readonly MottuDbContext _context;

        public MotorCycleRepository(MottuDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Motorcycle motorcycle)
        {
            await _context.Motorcycles.AddAsync(motorcycle);
            await _context.SaveChangesAsync();
        }

        public async Task<Motorcycle?> GetByPlateAsync(string plate)
        {
            return await _context.Motorcycles.FirstOrDefaultAsync(m => m.Plate == plate);
        }

        public async Task<List<Motorcycle>> GetAll()
        {
            return await _context.Motorcycles.ToListAsync();
        }

        public async Task<Motorcycle?> UpdateAsync(Motorcycle motorcycle)
        {
            _context.Motorcycles.Update(motorcycle);
            await _context.SaveChangesAsync();

            var updatedMotorcycle = await _context.Motorcycles.FirstOrDefaultAsync(m => m.Id == motorcycle.Id);

            return updatedMotorcycle;
        }

        public async Task<Motorcycle?> GetMotorcycleIsntRented()
        {
            return await _context.Motorcycles.FirstOrDefaultAsync(m => m.IsRented == false);
        }

        public async Task DeleteAsync(Motorcycle motorcycle)
        {
            _context.Motorcycles.Remove(motorcycle);
            await _context.SaveChangesAsync();
        }
    }
}
