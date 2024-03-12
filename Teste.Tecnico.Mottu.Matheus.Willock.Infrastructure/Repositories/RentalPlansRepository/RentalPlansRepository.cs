using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teste.Tecnico.Mottu.Matheus.Willock.Domain.Models;

namespace Teste.Tecnico.Mottu.Matheus.Willock.Infrastructure.Repositories.RentalPlansRepository
{
    public class RentalPlansRepository : IRentalPlansRepository
    {
        private readonly MottuDbContext _context;

        public RentalPlansRepository(MottuDbContext context)
        {
            _context = context;
        }

        // Create
        public async Task<RentalPlan?> Add(RentalPlan rentalPlan)
        {
            _context.RentalPlans.Add(rentalPlan);
            await _context.SaveChangesAsync();
            var getRentalPlan = await _context.RentalPlans.FindAsync(rentalPlan.Id);
            return getRentalPlan;
        }

        // Read
        public async Task<RentalPlan?> Get(int id)
        {
            return await _context.RentalPlans.FindAsync(id);
        }

        // Update
        public async Task<RentalPlan?> Update(RentalPlan rentalPlan)
        {
            _context.Entry(rentalPlan).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            var getRentalPlan = await _context.RentalPlans.FindAsync(rentalPlan.Id);
            return getRentalPlan;
        }

        // Delete
        public async Task Delete(int id)
        {
            var rentalPlan = await _context.RentalPlans.FindAsync(id);

            if (rentalPlan != null)
            {
                _context.RentalPlans.Remove(rentalPlan);
                await _context.SaveChangesAsync();
            }
        }
    }
}
