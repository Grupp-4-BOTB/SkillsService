using Microsoft.EntityFrameworkCore;
using SkillsService.Domain.Entities;

namespace SkillsService.Infrastructure.Persistance.Repositories;

public class SkillCatalogRepository
{
    private readonly SkillsDbContext _context;

    public SkillCatalogRepository(SkillsDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<SkillCatalog>> GetAllAsync(CancellationToken ct)
    {
        return await _context.SkillCatalog
            .ToListAsync(ct);
    }

    public async Task<SkillCatalog?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        return await _context.SkillCatalog
            .FirstOrDefaultAsync(s => s.Id == id, ct);
    } 
}
