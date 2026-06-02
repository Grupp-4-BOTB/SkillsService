using Microsoft.EntityFrameworkCore;
using SkillsService.Domain.Entities;
using SkillsService.Domain.Repositories;

namespace SkillsService.Infrastructure.Persistance.Repositories;

public class SkillCatalogRepository : ISkillCatalogRepository
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

    public async Task AddAsync(SkillCatalog skill, CancellationToken ct)
    {
        await _context.SkillCatalog.AddAsync(skill, ct);
        await _context.SaveChangesAsync(ct);
    }
}
