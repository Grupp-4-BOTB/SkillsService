using Microsoft.EntityFrameworkCore;
using SkillsService.Domain.Entities;
using SkillsService.Domain.Repositories;
using SkillsService.Domain.ValueObjects;

namespace SkillsService.Infrastructure.Persistance.Repositories;

public class UserSkillRepository : IUserSkillRepository
{
    private readonly SkillsDbContext _context;
    
    public UserSkillRepository(SkillsDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<UserSkill>> GetByOwnerIdAsync(OwnerId ownerId, CancellationToken ct)
    {
        return await _context.UserSkills
            .Include(s => s.SkillCatalog)
            .Where(s => s.OwnerId == ownerId)
            .ToListAsync(ct);
    }
    public async Task<UserSkill?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        return await _context.UserSkills
            .FirstOrDefaultAsync(s => s.Id == id, ct);
    }

    public async Task AddAsync(UserSkill userSkill, CancellationToken ct)
    {
        await _context.UserSkills.AddAsync(userSkill, ct);
        await _context.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(UserSkill userSkill, CancellationToken ct)
    {
        _context.UserSkills.Remove(userSkill);
        await _context.SaveChangesAsync(ct);
    }

    

    
}
