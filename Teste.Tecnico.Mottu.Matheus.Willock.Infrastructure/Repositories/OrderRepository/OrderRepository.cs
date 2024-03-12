using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teste.Tecnico.Mottu.Matheus.Willock.Domain.Models;

namespace Teste.Tecnico.Mottu.Matheus.Willock.Infrastructure.Repositories.OrderRepository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly MottuDbContext _context;

        public OrderRepository(MottuDbContext context)
        {
            _context = context;
        }
        
        public async Task<Order> Create(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }
        
        public async Task<Order?> Get(Guid id)
        {
            return await _context.Orders.FindAsync(id);
        }
        
        public async Task Update(Order order)
        {
            _context.Entry(order).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        
        public async Task Delete(Guid id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
        }
    }
}
