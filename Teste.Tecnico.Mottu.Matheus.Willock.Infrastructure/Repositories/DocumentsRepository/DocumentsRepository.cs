using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teste.Tecnico.Mottu.Matheus.Willock.Domain.Models;

namespace Teste.Tecnico.Mottu.Matheus.Willock.Infrastructure.Repositories.DocumentsRepository
{
    public class DocumentsRepository : IDocumentsRepository
    {
        private readonly MottuDbContext _context;

        public DocumentsRepository(MottuDbContext context)
        {
            _context = context;
        }
        // Create
        public async Task<DeliveryManDocument?> Create(DeliveryManDocument document)
        {
            _context.Documents.Add(document);
            await _context.SaveChangesAsync();
            var getDocument = await _context.Documents.FirstOrDefaultAsync(x => x.CnhNumber == document.CnhNumber);
            return getDocument;
        }

        public async Task<DeliveryManDocument?> GetById(Guid id)
        {
            return await _context.Documents.FirstOrDefaultAsync(x => x.Id == id);
        }

        // Read
        public async Task<DeliveryManDocument?> GetByCnhNumber(string cnhNumber)
        {
            return await _context.Documents.FirstOrDefaultAsync(x => x.CnhNumber == cnhNumber);
        }

        // Update
        public async Task<DeliveryManDocument> Update(DeliveryManDocument document)
        {
            _context.Entry(document).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return document;
        }

        // Delete
        public async Task Delete(int id)
        {
            var documentToDelete = await _context.Documents.FindAsync(id);
            _context.Documents.Remove(documentToDelete);
            await _context.SaveChangesAsync();
        }

    }
}
